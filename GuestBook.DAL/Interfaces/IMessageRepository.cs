using GuestBook.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook.DAL.Interfaces
{
    public interface IMessageRepository : IBaseRepository<Message>
    {
        Task<bool> UpdateAsync(Message message);
        Task<bool> DeleteAsync(int id);

    }
}
