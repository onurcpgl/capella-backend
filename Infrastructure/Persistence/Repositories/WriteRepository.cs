using Application.Repositories;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T:BaseEntity,ItemEntity
    {
        private readonly CapellaDbContext _context;
        public WriteRepository(CapellaDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<bool> AddAsync(T model)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(model);
            return await _context.SaveChangesAsync()>-1;
        }

        public async Task<bool> AddRangeAsync(List<T> datas)
        {
            await Table.AddRangeAsync(datas);
            return true;
        }

        public async Task<bool> Remove(T model)
        {
            EntityEntry<T> entityEntry = Table.Remove(model);
            return _context.SaveChanges() > -1;

        }

        public bool RemoveRange(List<T> datas)
        {
            Table.RemoveRange(datas);
            return true;
        }

        public async Task<bool> Update(T model)
        {
            EntityEntry<T> entityEntry = Table.Update(model);
            entityEntry.State = EntityState.Modified;
            return _context.SaveChanges() > -1;
        }

        public async Task<IDbContextTransaction> DbTransactional()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task<bool> UpdateMatchEntity(T newModel, int id)
        {
            T dbModel = await Table.FirstOrDefaultAsync(data => data.Id == id);

            if (dbModel == null)
                throw new ArgumentNullException(nameof(dbModel));

            if (newModel == null)
                throw new ArgumentNullException(nameof(newModel));

            _context.Entry(dbModel).CurrentValues.SetValues(newModel);

            foreach (var property in _context.Entry(dbModel).Properties)
            {
                if (property.CurrentValue == null) 
                {
                    _context.Entry(dbModel).Property(property.Metadata.Name).IsModified = false; 
                }
            }

            return _context.SaveChanges() > -1;

        }
    }
}
