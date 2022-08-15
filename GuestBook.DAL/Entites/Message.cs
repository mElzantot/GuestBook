using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook.DAL.Entites
{
    public class Message
    {
        public int Id { get; set; }
        public string? MessageBody { get; set; }
        public int GuestId { get; set; }
        public DateTime CreationDate { get; set; }
        public int? ParentMessageId { get; set; }
        public bool IsDeleted { get; set; }

    }
}
