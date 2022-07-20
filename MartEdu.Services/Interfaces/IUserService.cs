using MartEdu.Domain.Commons;
using MartEdu.Domain.Entities.Users;
using MartEdu.Service.DTOs.Users;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MartEdu.Service.Interfaces
{
    public interface IUserService : IGenericService<User, UserForCreationDto>
    {
        Task<BaseResponse<User>> LoginAsync(UserForLoginDto loginDto);
        Task<BaseResponse<User>> SetImageAsync(Expression<Func<User, bool>> expression, IFormFile image);
    }
}
