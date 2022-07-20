using MartEdu.Data.Contexts;
using MartEdu.Data.IRepositories;
using MartEdu.Domain.Commons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace MartEdu.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IAuditable
    {
        internal MartEduDbContext dbContext;
        internal DbSet<T> dbSet;

        public GenericRepository(MartEduDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<T>();
        }

        public T Update(T entity)
            => dbSet.Update(entity).Entity;
        public async Task<T> CreateAsync(T entity)
            => (await dbSet.AddAsync(entity)).Entity;

        public Task<T> GetAsync(Expression<Func<T, bool>> expression)
            => dbSet.FirstOrDefaultAsync(expression);

        public IQueryable<T> Where(Expression<Func<T, bool>> expression = null)
            => expression is null ? dbSet : dbSet.Where(expression);

        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> expression)
        {
            var entity = await dbSet.FirstOrDefaultAsync(expression);

            if (entity is null)
                return false;

            dbSet.Remove(entity);

            return true;
        }

    }
}