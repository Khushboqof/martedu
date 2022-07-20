using MartEdu.Domain.Commons;
using MartEdu.Domain.Entities.Courses;
using MartEdu.Service.DTOs.Courses;
using MartEdu.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MartEdu.Api.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CoursesController : GenericController<ICourseService, Course, CourseForCreationDto>
    {
        public CoursesController(ICourseService service) : base(service)
        {
        }

        [HttpPost()]
        public async override Task<ActionResult<BaseResponse<Course>>> Create(CourseForCreationDto creationDto)
        {
            var result = await service.CreateAsync(creationDto, p => p.Name == creationDto.Name);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpPost("{id}/register/{userId}")]
        public async Task<ActionResult<BaseResponse<Course>>> Register(Guid id, Guid userId)
        {
            var result = await service.RegisterForCourseAsync(userId, id);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpPost("{id}/vote/")]
        public async Task<ActionResult<BaseResponse<Course>>> Vote(Guid id, [Required] int vote)
        {
            var result = await service.VoteAsync(vote, p => p.Id == id);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

    }
}
