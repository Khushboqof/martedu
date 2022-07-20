using AutoMapper;
using MartEdu.Data.Contexts;
using MartEdu.Data.IRepositories;
using MartEdu.Domain.Commons;
using MartEdu.Domain.Configurations;
using MartEdu.Domain.Enums;
using MartEdu.Service.Extensions;
using MartEdu.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MartEdu.Domain.Entities.Authors;
using MartEdu.Service.DTOs.Authors;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace MartEdu.Service.Services
{
    public class GenericService<TSource, TSourceForCreationDto> : IGenericService<TSource, TSourceForCreationDto>
        where TSource : class, IAuditable
        where TSourceForCreationDto : class
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IMapper mapper;
        protected readonly IWebHostEnvironment env;
        protected readonly IConfiguration config;
        protected readonly IGenericRepository<TSource> repository;
        protected readonly MartEduDbContext dbContext;
        protected readonly string includeParams;

        public GenericService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env, IConfiguration config, IGenericRepository<TSource> repository, MartEduDbContext dbContext, string includeParams = null)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.env = env;
            this.config = config;
            this.repository = repository;
            this.dbContext = dbContext;
            this.includeParams = includeParams;
        }

        public virtual async Task<BaseResponse<TSource>> CreateAsync(TSourceForCreationDto model, Expression<Func<TSource, bool>> expression)
        {
            var response = new BaseResponse<TSource>();

            var existTSource = await repository.GetAsync(expression);

            if (existTSource is not null)
            {
                response.Error = new ErrorResponse(400, "This email or username already used!");
                return response;
            }


            var mappedSource = mapper.Map<TSource>(model);

            mappedSource.Create();

            var result = await repository.CreateAsync(mappedSource);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

        public virtual async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<TSource, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            var source = await repository.GetAsync(expression);

            if (source is null || source.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, $"{typeof(TSource).Name} not found");
                return response;
            }

            source.Delete();
            await unitOfWork.SaveChangesAsync();

            response.Data = true;

            return response;
        }

        public virtual async Task<BaseResponse<IEnumerable<TSource>>> GetAllAsync(PaginationParams @params, Expression<Func<TSource, bool>> expression = null)
        {
            var response = new BaseResponse<IEnumerable<TSource>>();


            var sources = repository.Where(expression).Where(p => p.State != ItemState.Deleted);

            if (!string.IsNullOrEmpty(includeParams))
                sources = sources.Include(includeParams);


            response.Data = sources.ToPagedList(@params);

            return response;
        }

        public virtual async Task<BaseResponse<TSource>> GetAsync(Expression<Func<TSource, bool>> expression)
        {
            var response = new BaseResponse<TSource>();

            var source = await repository.GetAsync(expression);

            if (source.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, $"{typeof(TSource).Name} not found");
                return response;
            }

            response.Data = source;

            return response;
        }

        public virtual async Task<BaseResponse<TSource>> RestoreAsync(Expression<Func<TSource, bool>> expression)
        {
            var response = new BaseResponse<TSource>();

            var source = await repository.GetAsync(expression);

            if (source is null)
            {
                response.Error = new ErrorResponse(404, $"{typeof(TSource).Name} not found");
                return response;
            }

            source.Update();

            repository.Update(source);

            await unitOfWork.SaveChangesAsync();

            response.Data = source;

            return response;
        }

        public virtual async Task<BaseResponse<TSource>> UpdateAsync(Guid id, TSourceForCreationDto model)
        {
            var response = new BaseResponse<TSource>();

            // check for exist TSource
            var source = await repository.GetAsync(p => p.Id == id && p.State != ItemState.Deleted);

            if (source is null)
            {
                response.Error = new ErrorResponse(404, $"{typeof(TSource).Name} not found");
                return response;
            }

            source = mapper.Map(model, source);

            source.Update();

            var result = repository.Update(source);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

    }
}
