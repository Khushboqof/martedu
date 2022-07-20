using MartEdu.Data.IRepositories;
using MartEdu.Data.Repositories;
using MartEdu.Domain.Entities.Authors;
using MartEdu.Domain.Entities.Courses;
using MartEdu.Domain.Entities.Users;
using MartEdu.Service.DTOs.Authors;
using MartEdu.Service.DTOs.Courses;
using MartEdu.Service.DTOs.Users;
using MartEdu.Service.Interfaces;
using MartEdu.Service.Services;
using Microsoft.Extensions.DependencyInjection;


namespace MartEdu.Api.Extensions
{
    internal static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
            services.AddScoped<IGenericService<User, UserForCreationDto>, GenericService<User, UserForCreationDto>>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IGenericRepository<Author>, GenericRepository<Author>>();
            services.AddScoped<IGenericService<Author, AuthorForCreationDto>, GenericService<Author, AuthorForCreationDto>>();
            services.AddScoped<IAuthorService, AuthorService>();

            services.AddScoped<IGenericRepository<Course>, GenericRepository<Course>>();
            services.AddScoped<IGenericService<Course, CourseForCreationDto>, GenericService<Course, CourseForCreationDto>>();
            services.AddScoped<ICourseService, CourseService>();

        }
    }
}
