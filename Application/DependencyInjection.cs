
using System.Reflection;
using Application.AutoMapper.Profiles;
using Application.Persons.Commands.CreatePerson;

using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            #region AutoMapper Configurations

            services
                .AddAutoMapper(typeof(PersonProfile));
            services
                .AddMediatR(Assembly.GetExecutingAssembly());

            #endregion
            //services.AddValidatorsFromAssemblyContaining<CreatePersonCommandValidator>();
            //services.AddScoped<IValidator<CreatePersonCommand>, CreatePersonCommandValidator>();

            return services;
        }
    }
}
