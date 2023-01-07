using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IWriteRepository<T>: IRepository<T> where T :BaseEntity,ItemEntity
    {
        
        Task AddAsync(T model);
        Task RemoveAsync(T model);
        Task UpdateAsync(T model, int id);
        Task<IDbContextTransaction> DbTransactional();


    }
}
