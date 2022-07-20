using AutoMapper;
using MartEdu.Data.Contexts;
using MartEdu.Data.IRepositories;
using MartEdu.Domain.Commons;
using MartEdu.Domain.Entities.Courses;
using MartEdu.Domain.Enums;
using MartEdu.Service.DTOs.Courses;
using MartEdu.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MartEdu.Service.Services
{
    public class CourseService : GenericService<Course, CourseForCreationDto>, ICourseService
    {
        public CourseService
            (
                IUnitOfWork unitOfWork,
                IMapper mapper,
                IWebHostEnvironment env,
                IConfiguration config,
                IGenericRepository<Course> repository,
                MartEduDbContext dbContext
            ) : base(unitOfWork, mapper, env, config, repository, dbContext, "Participants")
        {
        }

        public async Task<BaseResponse<Course>> RegisterForCourseAsync(Guid userId, Guid courseId)
        {
            var response = new BaseResponse<Course>();

            var user = await unitOfWork.Users.GetAsync(p => p.Id == userId && p.State != ItemState.Deleted);
            if (user is null)
            {
                response.Error = new ErrorResponse(404, "User not found");
                return response;
            }

            var course = await unitOfWork.Courses.GetAsync(p => p.Id == courseId && p.State != ItemState.Deleted);
            if (course is null)
            {
                response.Error = new ErrorResponse(404, "Course not found!");
                return response;
            }

            user.Courses.Add(course);
            user.Update();
            unitOfWork.Users.Update(user);

            course.Participants.Add(user);
            course.Update();
            unitOfWork.Courses.Update(course);

            await unitOfWork.SaveChangesAsync();

            response.Data = course;
            return response;
        }

        public async Task<BaseResponse<Course>> VoteAsync(int vote, Expression<Func<Course, bool>> expression)
        {
            var response = new BaseResponse<Course>();

            if (vote < 1 || vote > 5)
            {
                response.Error = new ErrorResponse(400, "The score cannot be less than 0 and more than 5!");
                return response;
            }

            var course = await unitOfWork.Courses.GetAsync(expression);

            if (course is null)
            {
                response.Error = new ErrorResponse(404, "Course not found!");
                return response;
            }

            course.Score += vote;
            course.CountOfVotes++;


            course.Update();
            unitOfWork.Courses.Update(course);

            await unitOfWork.SaveChangesAsync();

            response.Data = course;

            return response;
        }
    }
}
