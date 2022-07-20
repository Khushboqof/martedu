using MartEdu.Domain.Commons;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace MartEdu.Data.IRepositories
{
    public interface IGenericRepository<T> where T : IAuditable
    {
        Task<T> CreateAsync(T entity);
        T Update(T entity);
        Task<bool> DeleteAsync(Expression<Func<T, bool>> expression);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        IQueryable<T> Where(Expression<Func<T, bool>> expression = null);
    }
}
