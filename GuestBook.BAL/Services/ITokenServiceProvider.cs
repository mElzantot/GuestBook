using GuestBook.BAL.DTO;
using GuestBook.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook.BAL.Services
{
    public interface ITokenServiceProvider
    {
        AuthResponseDTO GenerateAccessToken(Guest guest);

    }
}
