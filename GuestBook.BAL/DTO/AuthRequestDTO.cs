using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook.BAL.DTO
{
    public class AuthRequestDTO
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
