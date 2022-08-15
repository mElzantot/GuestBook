using GuestBook.BAL.DTO;
using GuestBook.BAL.Interfaces;
using GuestBook.BAL.Services;
using GuestBook.DAL.Entites;
using GuestBook.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook.BAL.BL
{
    public class AuthBL : IAuthBL
    {
        private readonly IGuestRepository _guestRepository;
        private readonly IHashingService _hashingService;
        private readonly ITokenServiceProvider _tokenServiceProvider;

        public AuthBL(IGuestRepository guestRepository, IHashingService hashingService, ITokenServiceProvider tokenServiceProvider)
        {
            _guestRepository = guestRepository;
            _hashingService = hashingService;
            _tokenServiceProvider = tokenServiceProvider;
        }

        public async Task<int> GetAllUsersAsync()
        {
            var result = await _guestRepository.GetAllAsync();
            return result.Count();
        }

        public async Task<AuthResponseDTO> Register(AuthRequestDTO newGuest)
        {
            var guest = await _guestRepository.InsertAsync(new Guest
            {
                GuestName = newGuest.UserName,
                PasswordHash = _hashingService.Hash(newGuest.Password),
                CreationDate = DateTime.Now
            });
            if (guest.Id == 0) return null;
            return _tokenServiceProvider.GenerateAccessToken(guest);

        }

        public async Task<AuthResponseDTO> Login(AuthRequestDTO guest)
        {
            var user = await _guestRepository.GetGuestAsync(guest.UserName);
            if (user == null) return null;
            bool isAuthorized = _hashingService.HashCheck(user.PasswordHash, guest.Password);
            if (!isAuthorized) return null;
            return _tokenServiceProvider.GenerateAccessToken(user);
        }


        public async Task<bool> CheckIfUserNameExist(string userName)
        {
            var user = await _guestRepository.GetGuestAsync(userName);
            return user != null;
        }

    }
}
