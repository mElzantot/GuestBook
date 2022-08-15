using GuestBook.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook.DAL.Interfaces
{
    public interface IGuestRepository : IBaseRepository<Guest>
    {
        Task<Guest> GetGuestAsync(string GuestName);

    }
}
