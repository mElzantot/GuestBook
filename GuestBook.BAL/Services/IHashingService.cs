using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook.BAL.Services
{
    public interface IHashingService
    {
        string Hash(string text);
        bool HashCheck(string hashed, string text);

    }
}
