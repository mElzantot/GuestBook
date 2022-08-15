using GuestBook.BAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook.BAL.Interfaces
{
    public interface IMessageBL
    {
        Task<bool> WriteNewMessage(string newMsg, int senderId);
        Task<bool> UpdateMessage(UpdateMessageDTO updateMessageDTO);
        Task<bool> DeleteMessage(int messageId);
        Task<bool> ReplyMessage(AddReplyDTO replyDTO, int senderId);

    }
}
