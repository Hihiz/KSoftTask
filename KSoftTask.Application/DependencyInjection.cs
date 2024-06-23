using FluentValidation;
using KSoftTask.Application.Dto.AuthorBooks;
using KSoftTask.Application.Dto.Authors;
using KSoftTask.Application.Dto.Books;
using KSoftTask.Application.Dto.PublisherBooks;
using KSoftTask.Application.Dto.Publishers;
using KSoftTask.Application.Interfaces;
using KSoftTask.Application.Services;
using KSoftTask.Application.Validations.FluentValidations.AuthorBooks;
using KSoftTask.Application.Validations.FluentValidations.Authors;
using KSoftTask.Application.Validations.FluentValidations.Books;
using KSoftTask.Application.Validations.FluentValidations.PublisherBooks;
using KSoftTask.Application.Validations.FluentValidations.Publishers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace KSoftTask.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });

            services.ServicesInit();
            services.FluentValidatorInit();

            return services;
        }

        private static void ServicesInit(this IServiceCollection services)
        {
            services.AddScoped<IBaseService<AuthorDto, CreateAuthorDto, UpdateAuthorDto>, AuthorService>();
            services.AddScoped<IBaseService<BookDto, CreateBookDto, UpdateBookDto>, BookService>();
            services.AddScoped<IBaseService<PublisherDto, CreatePublisherDto, UpdatePublisherDto>, PublisherService>();
            services.AddScoped<IBaseService<AuthorBookDto, CreateAuthorBookDto, UpdateAuthorBookDto>, AuthorBookService>();
            services.AddScoped<IBaseService<PublisherBookDto, CreatePublisherBookDto, UpdatePublisherBookDto>, PublisherBookService>();
        }

        private static void FluentValidatorInit(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateAuthorDto>, CreateAuthorValidator>();
            services.AddScoped<IValidator<UpdateAuthorDto>, UpdateAuthorValidator>();

            services.AddScoped<IValidator<CreateBookDto>, CreateBookValidator>();
            services.AddScoped<IValidator<UpdateBookDto>, UpdateBookValidator>();

            services.AddScoped<IValidator<CreatePublisherDto>, CreatePublisherValidator>();
            services.AddScoped<IValidator<UpdatePublisherDto>, UpdatePublisherValidator>();

            services.AddScoped<IValidator<CreateAuthorBookDto>, CreateAuthorBookValidator>();
            services.AddScoped<IValidator<UpdateAuthorBookDto>, UpdateAuthorBookValidator>();

            services.AddScoped<IValidator<CreatePublisherBookDto>, CreatePublisherBookValidator>();
            services.AddScoped<IValidator<UpdatePublisherBookDto>, UpdatePublisherBookValidator>();
        }
    }
}
