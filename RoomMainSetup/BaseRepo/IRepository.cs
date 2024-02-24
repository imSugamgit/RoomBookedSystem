using RoomMainSetup.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomMainSetup.BaseRepo
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        IQueryable<TEntity> GetAllAsync();
        Task<TEntity> AddAsync(TEntity entity);
        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> DeleteAsync(TEntity entity);
        Task<IEnumerable<TEntity>> DeleteRangeAsync(IEnumerable<TEntity> entities);

        Task<TEntity> GetByIdAsync(long id);
    }

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        public readonly ApplicationDbContext _applicationDbContext;
        public Repository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            if (entities.Count() == 0)
            {
                throw new ArgumentException($"{ nameof(AddAsync)} entity must be null");
            }
            try
            {
                await _applicationDbContext.AddRangeAsync(entities);
                await _applicationDbContext.SaveChangesAsync();
                return entities;
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {

                throw new ArgumentException($"{ nameof(AddAsync)} entity must be null");
            }
            try
            {
                await _applicationDbContext.AddAsync(entity);
                await _applicationDbContext.SaveChangesAsync();
                return entity;
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {

            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(DeleteAsync)} entity must not be null");
            }

            try
            {
                _applicationDbContext.Remove(entity);
                await _applicationDbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be deleted: {ex.Message}");
            }
        }

        public async Task<IEnumerable<TEntity>> DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            if (entities.Count() == 0)
            {
                throw new ArgumentNullException($"{nameof(DeleteAsync)} entity must not be null");
            }

            try
            {
                _applicationDbContext.RemoveRange(entities);
                await _applicationDbContext.SaveChangesAsync();

                return entities;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entities)} could not be deleted: {ex.Message}");
            }
        }

        public IQueryable<TEntity> GetAllAsync()
        {
            try
            {
                return _applicationDbContext.Set<TEntity>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public async Task<TEntity> GetByIdAsync(long Id)
        {
            if (Id == 0)
            {
                throw new ArgumentNullException($"{nameof(GetByIdAsync)} entity must not be null");
            }
            try
            {
                return await _applicationDbContext.Set<TEntity>().FindAsync(Id);
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(Id)} could not be find: {ex.Message}");
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(UpdateAsync)} entity must not be null");
            }

            try
            {
                _applicationDbContext.Update(entity);
                await _applicationDbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
            }
        }
    }
}