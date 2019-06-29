using Appinion.Api.Helper;
using Appinion.ApplicationService.CommandHandlers;
using Appinion.ApplicationService.Commands;
using Appinion.ApplicationService.Common;
using Appinion.Domain.Config;
using Appinion.Domain.Interface;
using Appinion.Infrastructure.Common;
using Appinion.Infrastructure.Config;
using Appinion.Infrastructure.Jobs;
using Appinion.Infrastructure.Query;
using Appinion.Infrastructure.QueryObjects;
using Appinion.Infrastructure.QueryObjects.Common;
using Appinion.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using NHibernate;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Appinion.Api.Helper.AutomapperHelper;
using contexto = Microsoft.AspNetCore.Http;

namespace Appinion.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        #if RELEASE
                .AddJsonFile("appsettings.Production.json", true)
        #endif
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DataBase");

            // Injentando a factory na aplicação
            services.AddSingleton<ISessionFactory>(factory => { return new NHibernateSessionFactory().CreateFactoryWeb(connectionString); });

            // Injetando a sessão da factory.
            services.AddScoped<NHibernate.ISession>(factory => factory
                    .GetServices<ISessionFactory>().First()
                    .OpenSession());

            services.AddScoped<IServiceContext, ServiceContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<contexto.IHttpContextAccessor, contexto.HttpContextAccessor>();
            services.AddSingleton<IUserHelper, UserHelper>();
            services.Configure<UploadSettingsModel>(Configuration.GetSection("UploadSettings"));

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettingsModel>(appSettingsSection);

            // Injetando os comandos
            // Usuário
            services.AddScoped<ICommandHandler<CadastrarUsuarioCommand>, CadastrarUsuario>();
            services.AddScoped<ICommandHandler<AtualizarDescritivoUsuarioCommand>, AtualizarDescritivoUsuario>();
            services.AddScoped<ICommandHandler<AtualizarUsernameUsuarioCommand>, AtualizarUsernameUsuario>();
            services.AddScoped<ICommandHandler<AtualizarEmailUsuarioCommand>, AtualizarEmailUsuario>();
            services.AddScoped<ICommandHandler<AtualizarFotoUsuarioCommand>, AtualizarFotoUsuario>();
            services.AddScoped<ICommandHandler<AutenticarUsuarioCommand>, AutenticarUsuario>();
            services.AddScoped<ICommandHandler<AtualizarSenhaUsuarioCommand>, AtualizarSenhaUsuario>();
            services.AddScoped<ICommandHandler<AtualizarSenhaUsuarioCommand>, AtualizarSenhaUsuario>();
            services.AddScoped<ICommandHandler<InativarUsuarioCommand>, InativarUsuario>();
            services.AddScoped<ICommandHandler<SeguirUsuarioCommand>, SeguirUsuario>();
            services.AddScoped<ICommandHandler<PararSeguirUsuarioCommand>, PararSeguirUsuario>();

            // Opinião
            services.AddScoped<ICommandHandler<ConcordarNoticiaCommand>, ConcordarNoticia>();
            services.AddScoped<ICommandHandler<DiscordarNoticiaCommand>, DiscordarNoticia>();

            // Publicação
            services.AddScoped<ICommandHandler<CadastrarPublicacaoCommand>, CadastrarPublicacao>();
            services.AddScoped<ICommandHandler<InativarPublicacaoCommand>, InativarPublicacao>();
            services.AddScoped<ICommandHandler<ComentarPublicacaoCommand>, ComentarPublicacao>();
            services.AddScoped<ICommandHandler<RecompartilharPublicacaoCommand>, RecompartilharPublicacao>();
            services.AddScoped<ICommandHandler<UpvotePublicacaoCommand>, UpvotePublicacao>();
            services.AddScoped<ICommandHandler<DownvotePublicacaoCommand>, DownvotePublicacao>();

            // Injetando os QueryObjects
            services.AddScoped<IQueryObject<UsuarioQuery>, UsuarioQuery>();
            services.AddScoped<IQueryObject<NoticiaQuery>, NoticiaQuery>();
            services.AddScoped<IQueryObject<PublicacaoQuery>, PublicacaoQuery>();
            services.AddScoped<IQueryObject<TituloUsuarioQuery>, TituloUsuarioQuery>();
            services.AddScoped<IQueryObject<TituloNoticiaQuery>, TituloNoticiaQuery>();

            // Injetando os repositórios
            services.AddScoped<ILogTransacaoRepository, LogTransacaoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IOpiniaoRepository, OpiniaoRepository>();
            services.AddScoped<IPublicacaoRepository, PublicacaoRepository>();
            services.AddScoped<IPublicacaoUpvoteRepository, PublicacaoUpvoteRepository>();
            services.AddScoped<IPublicacaoDownvoteRepository, PublicacaoDownvoteRepository>();
            services.AddScoped<ITituloUsuarioRepository, TituloUsuarioRepository>();
            services.AddScoped<ISeguidorUsuarioRepository, SeguidorUsuarioRepository>();

            services.AddSingleton<Appinion.Api.Helper.Quartz>();

            services.AddTransient<IJobFactory, JobFactory>(
                (provider) =>
                {
                    return new JobFactory(provider);
                });
            services.AddTransient<TituloUsuarioComMaisPublicacoesJob>();
            services.AddTransient<TituloUsuarioComMaisUpvotesJob>();
            services.AddTransient<TituloUsuarioComMaisDownvotesJob>();

            services.AddTransient<TituloNoticiaMaisConcordada>();
            services.AddTransient<TituloNoticiaMaisDiscordada>();

            // Inicializando a Autorização JWT
            var appSettings = appSettingsSection.Get<AppSettingsModel>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var usuarioRepository = context.HttpContext.RequestServices.GetRequiredService<IUsuarioRepository>();
                        var usuarioId = int.Parse(context.Principal.Identity.Name);
                        var user = usuarioRepository.Find(usuarioId);
                        if(user == null)
                        {
                            context.Fail("Não Autorizado");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // Inicializando o Automapper
            InitializeMapper();

            // Inicializando os validadores dos comandos
            FluentValidationHelper.InitializeValidators(services);

            services.AddMvc(options =>
            {
                options.Filters.Add(new SessionFilter());
                options.Filters.Add(new CustomActionFilter());
            }).AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
                .AddJsonOptions(options => options.SerializerSettings.MaxDepth = 2)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMiddleware(typeof(BadRequestException));
            app.UseMvc();

            app.UseQuartz((quartz) =>
            {
                quartz.AddWeeklyJob<TituloUsuarioComMaisPublicacoesJob>("TituloUsuarioComMaisPublicacoes", "Titulos");
                quartz.AddWeeklyJob<TituloUsuarioComMaisUpvotesJob>("TituloUsuarioComMaisUpvotes", "Titulos");
                quartz.AddWeeklyJob<TituloUsuarioComMaisUpvotesJob>("TituloUsuarioComMaisDownvotes", "Titulos");

                quartz.AddWeeklyJob<TituloNoticiaMaisConcordada>("TituloNoticiaMaisConcordada", "Titulos");
                quartz.AddWeeklyJob<TituloNoticiaMaisDiscordada>("TituloNoticiaMaisDiscordada", "Titulos");

            });
        }
    }
}
