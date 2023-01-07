using Application.Repositories;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<WriteRepository<T>> _logger;

        public WriteRepository(CapellaDbContext context, ILogger<WriteRepository<T>> logger)
        {
            _context = context;
            _logger = logger;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<IDbContextTransaction> DbTransactional()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task AddAsync(T model)
        {
            try
            {
                EntityEntry<T> entityEntry = Table.Add(model);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) {
                throw new Exception(ex.ToString(), ex);
            }
 
        }

        public async Task RemoveAsync(T model)
        {
            try
            {
                EntityEntry<T> entityEntry = Table.Remove(model);
                await _context.SaveChangesAsync();

            }catch(Exception ex)
            {
                throw new Exception(ex.ToString(), ex);
            }
           
        }
        public async Task UpdateAsync(T newModel, int id)
        {
            try
            {
                T dbModel = Table.FirstOrDefault(data => data.Id == id);

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

                await _context.SaveChangesAsync();

            }catch (Exception ex)
            {
                throw new Exception(ex.ToString(), ex);
            }
        }

    }
}
