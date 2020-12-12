using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeDevices.Core.Database.Models
{
    public class DataEntity<T> where T : class
    {
        private readonly DevicesContext _dbContext;

        public DataEntity(DevicesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> Add(T Item)
        {
            var result = await _dbContext.AddAsync(Item);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<T> Remove(T Item)
        {
            await Task.FromResult(_dbContext.Remove<T>(Item));
            await _dbContext.SaveChangesAsync();
            return Item;
        }

        public async Task<T> Remove(Guid Id)
        {
            var item = await _dbContext.FindAsync<T>(Id);
            if(item != null)
            {
                await Task.FromResult(_dbContext.Remove<T>(item));
                await _dbContext.SaveChangesAsync();
            }
            return item;
        }

        public async Task<T> Update(T Item)
        {
            var result = await Task.FromResult(_dbContext.Update<T>(Item));
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<T> Get(Guid Id)
        {
            return await _dbContext.FindAsync<T>(Id);
        }

        public async Task<IEnumerable<T>> Query(Func<T, bool> Predicate)
        {
            return await Task.FromResult(_dbContext.Set<T>().Where(Predicate));
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await Task.FromResult<IEnumerable<T>>(_dbContext.Set<T>().ToList());
        }
    }
}
