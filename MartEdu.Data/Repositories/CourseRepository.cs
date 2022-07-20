using MartEdu.Data.Contexts;
using MartEdu.Data.IRepositories;
using MartEdu.Domain.Entities.Courses;

namespace MartEdu.Data.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(MartEduDbContext dbContext) : base(dbContext)
        {
        }
    }
}
