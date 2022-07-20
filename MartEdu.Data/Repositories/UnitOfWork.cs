using MartEdu.Data.Contexts;
using MartEdu.Data.IRepositories;
using System;
using System.Threading.Tasks;

namespace MartEdu.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository Users { get; }
        public ICourseRepository Courses { get; }
        public IAuthorRepository Authors { get; }


        public MartEduDbContext dbContext;

        public UnitOfWork(MartEduDbContext dbContext)
        {
            this.dbContext = dbContext;

            Users = new UserRepository(dbContext);
            Courses = new CourseRepository(dbContext);
            Authors = new AuthorRepository(dbContext);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
