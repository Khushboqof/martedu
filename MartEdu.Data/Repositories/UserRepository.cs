using MartEdu.Data.Contexts;
using MartEdu.Data.IRepositories;
using MartEdu.Domain.Entities.Users;

namespace MartEdu.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(MartEduDbContext dbContext) : base(dbContext)
        {
        }
    }
}
