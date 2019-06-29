using Appinion.Domain.Entity;
using Appinion.Infrastructure.QueryModels;
using Appinion.Infrastructure.QueryObjects.UsuarioQueryObjects.QueryModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appinion.Api.Helper
{
    public class AutomapperHelper
    {
        public static void InitializeMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Usuario, UsuarioQueryModel>();
                cfg.CreateMap<SeguidorUsuario, SeguidorUsuarioQueryModel>();
                cfg.CreateMap<Noticia, NoticiaQueryModel>();
                cfg.CreateMap<Opiniao, ListarNoticiasMaisConcordadasDaSemanaQueryModel>();
                cfg.CreateMap<Opiniao, ListarUsuariosMenosParecidosComUsuarioLogadoQueryModel>();
                cfg.CreateMap<Opiniao, ListarUsuariosMaisParecidosComUsuarioLogadoQueryModel>();
                cfg.CreateMap<Publicacao, PublicacaoQueryModelSearch>();
                cfg.CreateMap<Publicacao, DetalharPublicacaoQueryModel>();
                cfg.CreateMap<ArquivoQueryModel, ArquivoQueryModel>();
                cfg.CreateMap<ArquivoQueryModel, ArquivoQueryModel>();
                
                cfg.CreateMap<Usuario, DetalharUsuarioQueryModel>()
                    .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                    .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                    .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao))
                    .ForMember(dest => dest.QuantidadeSeguindo, opt => opt.MapFrom(src => src.Seguindo.Count))
                    .ForMember(dest => dest.QuantidadeSeguidores, opt => opt.MapFrom(src => src.Seguidores.Count));

                // Mapeamento para mostrar o usuário sem lista mas com a quantidade de cada lista
                cfg.CreateMap<Usuario, UsuarioQueryModelWithoutList>()
                    .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                    .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                    .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao))
                    .ForMember(dest => dest.QuantidadeSeguindo, opt => opt.MapFrom(src => src.Seguindo.Count))
                    .ForMember(dest => dest.QuantidadeSeguidores, opt => opt.MapFrom(src => src.Seguidores.Count));

                // Mapeando para a detalhar somente o necessário ao listar usuários seguidores
                cfg.CreateMap<SeguidorUsuario, SeguidorUsuarioQueryModelUsuarioSeguidor>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UsuarioSeguidor.Id))
                    .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UsuarioSeguidor.Username))
                    .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.UsuarioSeguidor.Nome))
                    .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.UsuarioSeguidor.Descricao))
                    .ForMember(dest => dest.Foto, opt => opt.MapFrom(src => src.UsuarioSeguidor.Foto));

                // Mapeando para a detalhar somente o necessário ao listar usuários seguindo
                cfg.CreateMap<SeguidorUsuario, SeguidorUsuarioQueryModelUsuarioSeguido>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UsuarioSeguido.Id))
                    .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UsuarioSeguido.Username))
                    .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.UsuarioSeguido.Nome))
                    .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.UsuarioSeguido.Descricao))
                    .ForMember(dest => dest.Foto, opt => opt.MapFrom(src => src.UsuarioSeguido.Foto));

            });
        }
    }
}
