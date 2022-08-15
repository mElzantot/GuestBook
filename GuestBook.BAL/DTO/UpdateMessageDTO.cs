using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook.BAL.DTO
{
    public class UpdateMessageDTO
    {
        public int Id { get; set; }
        public string? MessageBody { get; set; }
    }
}
