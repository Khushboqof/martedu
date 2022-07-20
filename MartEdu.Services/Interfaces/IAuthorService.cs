using MartEdu.Domain.Commons;
using MartEdu.Domain.Entities.Authors;
using MartEdu.Service.DTOs.Authors;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MartEdu.Service.Interfaces
{
    public interface IAuthorService : IGenericService<Author, AuthorForCreationDto>
    {
        Task<BaseResponse<Author>> LoginAsync(AuthorForLoginDto loginDto);
        Task<BaseResponse<Author>> SetBackgroundImageAsync(Expression<Func<Author, bool>> expression, IFormFile image);
        Task<BaseResponse<Author>> SetProfileImageAsync(Expression<Func<Author, bool>> expression, IFormFile image);
        Task<BaseResponse<Author>> DeleteBackgroundImageAsync(Expression<Func<Author, bool>> expression);
        Task<BaseResponse<Author>> DeleteProfileImageAsync(Expression<Func<Author, bool>> expression);
        Task<BaseResponse<Author>> VoteAsync(int vote, Expression<Func<Author, bool>> expression);
        Task<BaseResponse<Author>> AddCourseAsync(Guid authorId, Guid courseId);
    }
}
