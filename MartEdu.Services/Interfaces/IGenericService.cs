using MartEdu.Domain.Commons;
using MartEdu.Domain.Configurations;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MartEdu.Service.Interfaces
{
    public interface IGenericService<TSource, TSourceForCreatingDto>
        where TSource : IAuditable
    {
        Task<BaseResponse<TSource>> CreateAsync(TSourceForCreatingDto model, Expression<Func<TSource, bool>> expression);
        Task<BaseResponse<TSource>> UpdateAsync(Guid id, TSourceForCreatingDto model);
        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<TSource, bool>> expression);
        Task<BaseResponse<TSource>> GetAsync(Expression<Func<TSource, bool>> expression);
        Task<BaseResponse<IEnumerable<TSource>>> GetAllAsync(PaginationParams @params, Expression<Func<TSource, bool>> expression = null);
        Task<BaseResponse<TSource>> RestoreAsync(Expression<Func<TSource, bool>> expression);
    }
}
