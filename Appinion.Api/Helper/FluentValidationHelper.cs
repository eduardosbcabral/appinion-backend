using Appinion.ApplicationService.Commands;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appinion.Api.Helper
{
    public class FluentValidationHelper
    {
        public static void InitializeValidators(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(new ValidateModelStateFilter());
            })
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CadastrarUsuarioCommandValidator>());

        }
    }
}
