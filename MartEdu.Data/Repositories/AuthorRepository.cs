using MartEdu.Data.Contexts;
using MartEdu.Data.IRepositories;
using MartEdu.Domain.Entities.Authors;

namespace MartEdu.Data.Repositories
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(MartEduDbContext dbContext) : base(dbContext)
        {
        }
    }
}
