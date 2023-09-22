using Core.Interfaces;
using Core.Services;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class ServiceExtensions
    {
      
        public static void AddValidators(this IServiceCollection servicesCollection)
        {
            servicesCollection.AddFluentValidationAutoValidation();
            servicesCollection.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        }

        public static void AddAutoMapper(this IServiceCollection servicesCollection)
        {
            servicesCollection.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public static void AddCustomServices(this IServiceCollection servicesCollection)
        {
            servicesCollection.AddScoped<IMoviesService, MoviesService>();
            servicesCollection.AddScoped<IAccountsService, AccountsService>();
        }
    }
}
