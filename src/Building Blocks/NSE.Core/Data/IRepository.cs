using NSE.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NSE.Core.Data
{
    public interface IRepository<T> : IDisposable
        where T : IAggregateRoot
    {
        void Insert(T model);
        void Update(T model);
        void Delete(T model);
        void Delete(Guid id);
        Task<IEnumerable<T>> SelectAsync();
        Task<T> SelectAsync(Guid id);
    }
}
