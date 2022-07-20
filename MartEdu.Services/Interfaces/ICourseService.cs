using MartEdu.Domain.Commons;
using MartEdu.Domain.Entities.Courses;
using MartEdu.Service.DTOs.Courses;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MartEdu.Service.Interfaces
{
    public interface ICourseService : IGenericService<Course, CourseForCreationDto>
    {
        Task<BaseResponse<Course>> RegisterForCourseAsync(Guid userId, Guid courseId);
        Task<BaseResponse<Course>> VoteAsync(int vote, Expression<Func<Course, bool>> expression);
    }
}
