using Microsoft.EntityFrameworkCore;
using OngProject.Core.Entities;
using OngProject.Infrastructure.Data;
using OngProject.Infrastructure.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OngProject.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : EntityBase
    {
        #region Objects and Constructor
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _entity;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _entity = context.Set<T>();
        }
        #endregion

        public async Task<IEnumerable<T>> GetAll()
        {
            var response = await _entity.Where(x => x.IsDeleted == false).ToListAsync();
            return response;
        }
        public async Task<T> GetById(int id)
        {
            var response = await _entity.FindAsync(id);
            return response;
        }
        public async Task<T> Insert(T entity)
        {
            entity.CreatedAt = DateTime.Now;
            entity.IsDeleted = false;

            var response = await _entity.AddAsync(entity);
            return response.Entity;
        }
        public async Task Update(T entity)
        {
            entity.CreatedAt = DateTime.Now;

            _entity.Update(entity);
        }
        public async Task Delete(int id)
        {
            var entity = await _entity.FindAsync(id);
            if (entity == null)
            {
                throw new NotImplementedException();
            }

            entity.IsDeleted = true;
            entity.CreatedAt = DateTime.Now;

            _entity.Update(entity);
        }
        #region 
        /// <summary>
        /// Si la entidad existe y no ha sido borrada regresa un bool. 
        /// Si no esta registrado el Id, o ha sido borrada con baja logica, regresa null.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        #endregion
        public bool EntityExists(int id)
        {
            return _entity.Any(x => x.Id==id && x.IsDeleted == false);
        }
        public async Task<T> FindByCondition(Expression<Func<T, bool>> predicate)
        {
            var response = await _entity
                .Where(x=> x.IsDeleted==false)
                .FirstOrDefaultAsync(predicate);

            return response;

        }
    }
}
