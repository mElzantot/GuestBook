using GuestBook.BAL.DTO;
using GuestBook.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook.BAL.Interfaces
{
    public interface IAuthBL
    {
        Task<AuthResponseDTO> Register(AuthRequestDTO newGuest);
        Task<AuthResponseDTO> Login(AuthRequestDTO guest);
        Task<bool> CheckIfUserNameExist(string userName);

    }
}
