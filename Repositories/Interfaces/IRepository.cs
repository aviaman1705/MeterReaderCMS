using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReaderCMS.Repositories.Interfaces
{
   public interface IRepository<T, TKey> : IDisposable
    {
        IQueryable<T> GetAll();

        T Get(int id);

        void Add(T item);

        bool Update(T item);

        bool Delete(int id);
    }
}
