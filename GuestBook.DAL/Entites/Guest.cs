using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook.DAL.Entites
{
    public class Guest
    {
        public int Id { get; set; }
        public string? GuestName { get; set; }
        public string? PasswordHash { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpirationDate { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
