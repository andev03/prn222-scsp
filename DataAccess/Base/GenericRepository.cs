using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Base
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected QuitSmokingAppDBContext _context;

        public GenericRepository()
        {
            _context ??= new QuitSmokingAppDBContext();
        }

        public GenericRepository(QuitSmokingAppDBContext context)
        {
            _context = context;
        }

      
        public async Task<List<T>> GetAllAsync()
        {
            var item = await _context.Set<T>().ToListAsync();
            return item;
        }



        public async Task<int> AddAsync(T entity) {
            try
            {
                _context.Set<T>().Add(entity);
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> UpdateAsync(T entity)
        {
            try {
                _context.ChangeTracker.Clear();
                var tracker = _context.Attach(entity);
                tracker.State = EntityState.Modified;
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }

        }


        public async Task<bool> RemoveAsync(T entity)
        {
            try
            {
                _context.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }


        public async Task<T> GetByIdAsync(string code)
        {
            return await _context.Set<T>().FindAsync(code);
        }

        public T GetById(Guid code)
        {
            return _context.Set<T>().Find(code);
        }

        public async Task<T> GetByIdAsync(Guid code)
        {
            return await _context.Set<T>().FindAsync(code);
        }

        #region Separating asigned entity and save operators        

    

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        #endregion Separating asign entity and save operators
    }
}
