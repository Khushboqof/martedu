using AutoMapper;
using MartEdu.Data.Contexts;
using MartEdu.Data.IRepositories;
using MartEdu.Domain.Commons;
using MartEdu.Domain.Entities.Users;
using MartEdu.Service.DTOs.Users;
using MartEdu.Service.Extensions;
using MartEdu.Service.Extensions.Attributes;
using MartEdu.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace MartEdu.Service.Services
{
    public class UserService : GenericService<User, UserForCreationDto>, IUserService
    {
        public UserService
            (
               IUnitOfWork unitOfWork,
               IMapper mapper,
               IWebHostEnvironment env,
               IConfiguration config,
               IGenericRepository<User> repository,
               MartEduDbContext dbContext
           ) : base(unitOfWork, mapper, env, config, repository, dbContext, "Courses")
        {
        }

        public async Task<BaseResponse<User>> LoginAsync(UserForLoginDto loginDto)
        {
            var response = new BaseResponse<User>();

            var encryptedPassword = loginDto.Password.Encrypt();

            var user = await unitOfWork.Users
                                .GetAsync(p => (p.Username == loginDto.EmailOrUsername || p.Email == loginDto.EmailOrUsername)
                                            && p.Password == encryptedPassword);

            if (user is null)
            {
                response.Error = new ErrorResponse(404, "User not found!");
                return response;
            }

            response.Data = user;

            return response;
        }

        public async Task<BaseResponse<User>> SetImageAsync(Expression<Func<User, bool>> expression, [FormFileExtensions(".png", ".jpg"), MaxFileSize(5)] IFormFile image)
        {
            var response = new BaseResponse<User>();

            var user = await unitOfWork.Users.GetAsync(expression);

            if (user is null)
            {
                response.Error = new ErrorResponse(404, "User not found!");
                return response;
            }

            user.Image = await FileStreamExtensions.SaveFileAsync(image.OpenReadStream(), image.FileName, env, config);

            user.Update();
            unitOfWork.Users.Update(user);

            await unitOfWork.SaveChangesAsync();

            response.Data = user;

            return response;

        }
    }
}
