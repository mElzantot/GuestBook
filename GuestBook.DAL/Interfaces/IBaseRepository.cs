using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook.DAL.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> InsertAsync(T item);
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(object id);

    }
}
