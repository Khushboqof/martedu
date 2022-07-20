using AutoMapper;
using MartEdu.Data.Contexts;
using MartEdu.Data.IRepositories;
using MartEdu.Domain.Commons;
using MartEdu.Domain.Configurations;
using MartEdu.Domain.Entities.Authors;
using MartEdu.Domain.Enums;
using MartEdu.Service.DTOs.Authors;
using MartEdu.Service.Extensions;
using MartEdu.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MartEdu.Service.Services
{
    public class AuthorService : GenericService<Author, AuthorForCreationDto>, IAuthorService
    {
        public AuthorService
             (
                IUnitOfWork unitOfWork,
                IMapper mapper,
                IWebHostEnvironment env,
                IConfiguration config,
                IGenericRepository<Author> repository,
                MartEduDbContext dbContext
            ) : base(unitOfWork, mapper, env, config, repository, dbContext, "Courses")
        {
        }

        public async Task<BaseResponse<Author>> AddCourseAsync(Guid authorId, Guid courseId)
        {
            var response = new BaseResponse<Author>();

            var author = await unitOfWork.Authors.GetAsync(p => p.Id == authorId);

            if (author is null)
            {
                response.Error = new ErrorResponse(404, "Author not found!");
                return response;
            }

            var course = await unitOfWork.Courses.GetAsync(p => p.Id == courseId);

            if (course is null)
            {
                response.Error = new ErrorResponse(404, "Course not found!");
                return response;
            }

            author.Courses.Add(course);
            author.Update();

            unitOfWork.Authors.Update(author);

            response.Data = author;

            return response;
        }

        public async Task<BaseResponse<Author>> DeleteBackgroundImageAsync(Expression<Func<Author, bool>> expression)
        {
            var response = new BaseResponse<Author>();

            var author = await unitOfWork.Authors.GetAsync(expression);

            if (author is null)
            {
                response.Error = new ErrorResponse(404, "Author not found!");
                return response;
            }

            author.BackgroundImage = null;
            author.Update();

            unitOfWork.Authors.Update(author);

            response.Data = author;

            return response;
        }

        public async Task<BaseResponse<Author>> DeleteProfileImageAsync(Expression<Func<Author, bool>> expression)
        {
            var response = new BaseResponse<Author>();

            var author = await unitOfWork.Authors.GetAsync(expression);

            if (author is null)
            {
                response.Error = new ErrorResponse(404, "Author not found!");
                return response;
            }

            author.ProfileImage = null;

            author.Update();
            unitOfWork.Authors.Update(author);

            await unitOfWork.SaveChangesAsync();

            response.Data = author;

            return response;
        }

        public async Task<BaseResponse<Author>> LoginAsync(AuthorForLoginDto loginDto)
        {
            var response = new BaseResponse<Author>();

            var encryptedPassword = loginDto.Password.Encrypt();

            var author = await unitOfWork.Authors
                                .GetAsync(p => p.Email == loginDto.Email && p.Password == encryptedPassword);

            if (author is null)
            {
                response.Error = new ErrorResponse(404, "AUthor not found!");
                return response;
            }

            response.Data = author;

            return response;
        }

        public async Task<BaseResponse<Author>> SetBackgroundImageAsync(Expression<Func<Author, bool>> expression, IFormFile image)
        {
            var response = new BaseResponse<Author>();

            var author = await unitOfWork.Authors.GetAsync(expression);

            if (author is null)
            {
                response.Error = new ErrorResponse(404, "Author not found!");
                return response;
            }

            author.BackgroundImage = await FileStreamExtensions.SaveFileAsync(image.OpenReadStream(), image.FileName, env, config);

            author.Update();
            unitOfWork.Authors.Update(author);

            await unitOfWork.SaveChangesAsync();

            response.Data = author;

            return response;
        }

        public async Task<BaseResponse<Author>> SetProfileImageAsync(Expression<Func<Author, bool>> expression, IFormFile image)
        {
            var response = new BaseResponse<Author>();

            var author = await unitOfWork.Authors.GetAsync(expression);

            if (author is null)
            {
                response.Error = new ErrorResponse(404, "Author not found!");
                return response;
            }

            author.ProfileImage = await FileStreamExtensions.SaveFileAsync(image.OpenReadStream(), image.FileName, env, config);

            author.Update();
            unitOfWork.Authors.Update(author);

            await unitOfWork.SaveChangesAsync();

            response.Data = author;

            return response;
        }

        public async Task<BaseResponse<Author>> VoteAsync(int vote, Expression<Func<Author, bool>> expression)
        {
            var response = new BaseResponse<Author>();

            if (vote < 1 || vote > 5)
            {
                response.Error = new ErrorResponse(400, "The score cannot be less than 1 and more than 5!");
                return response;
            }

            var author = await unitOfWork.Authors.GetAsync(expression);

            if (author is null)
            {
                response.Error = new ErrorResponse(404, "Author not found!");
                return response;
            }

            author.Score += vote;
            author.CountOfVotes++;


            author.Update();
            unitOfWork.Authors.Update(author);

            await unitOfWork.SaveChangesAsync();

            response.Data = author;

            return response;
        }
    }
}
