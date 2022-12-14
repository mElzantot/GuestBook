using GuestBook.BAL.DTO;
using GuestBook.BAL.Interfaces;
using GuestBook.DAL.Entites;
using GuestBook.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook.BAL.BL
{
    public class MessageBL : IMessageBL
    {
        private readonly IMessageRepository _messageRepository;

        public MessageBL(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public async Task<bool> WriteNewMessage(string newMsg, int senderId)
        {
            var message = await _messageRepository.InsertAsync(new Message
            {
                MessageBody = newMsg,
                CreationDate = DateTime.Now,
                GuestId = senderId,
                ParentMessageId = null
            });
            return message.Id > 0;
        }

        public async Task<bool> ReplyMessage(AddReplyDTO replyDTO, int senderId)
        {
            var message = await _messageRepository.InsertAsync(new Message
            {
                MessageBody = replyDTO.MessageBody,
                CreationDate = DateTime.Now,
                GuestId = senderId,
                ParentMessageId = replyDTO.ParnetMessageId
            });
            return message.Id > 0;
        }

        public async Task<bool> UpdateMessage(UpdateMessageDTO updateMessageDTO)
        {
            return await _messageRepository.UpdateAsync(new Message { Id = updateMessageDTO.Id, MessageBody = updateMessageDTO.MessageBody });

        }

        public async Task<bool> DeleteMessage(int messageId)
        {
            return await _messageRepository.DeleteAsync(messageId);
        }


    }
}
