using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook.BAL.DTO
{
    public class AddMessageDTO
    {
        public string? MessageBody { get; set; }
        public int? ParnetMessageId { get; set; }
    }
}
