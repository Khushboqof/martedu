using System;
using System.Threading.Tasks;

namespace MartEdu.Data.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ICourseRepository Courses { get; }
        IAuthorRepository Authors { get; }
        Task SaveChangesAsync();
    }
}
