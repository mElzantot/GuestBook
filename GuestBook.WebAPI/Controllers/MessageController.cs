using FluentValidation;
using GuestBook.BAL.DTO;
using GuestBook.BAL.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GuestBook.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class MessageController : ControllerBase
    {
        private readonly IMessageBL _messageBL;
        private readonly IValidator<AddReplyDTO> _addValidator;
        private readonly IValidator<UpdateMessageDTO> _updateValidator;

        public MessageController(IMessageBL messageBL,
                                IValidator<AddReplyDTO> addValidator,
                                IValidator<UpdateMessageDTO> UpdateValidator
            )
        {
            _messageBL = messageBL;
            _addValidator = addValidator;
            _updateValidator = UpdateValidator;
        }

        [HttpPost("Write")]
        public async Task<IActionResult> Write(NewMessageDTO newMessage)
        {
            if (newMessage == null || string.IsNullOrWhiteSpace(newMessage.NewMsg)) return BadRequest();
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var IsWritten = await _messageBL.WriteNewMessage(newMessage.NewMsg, userId);
            return Ok(new { addedSuccessfully = IsWritten });
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateMessage(UpdateMessageDTO messageDTO)
        {
            var validationResult = _updateValidator.Validate(messageDTO);
            if (!validationResult.IsValid) return BadRequest(new { Errors = validationResult.Errors[0].ErrorMessage });
            var isUpdated = await _messageBL.UpdateMessage(messageDTO);
            return Ok(new { updated = isUpdated });
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> UpdateMessage(int id)
        {
            var isDeleted = await _messageBL.DeleteMessage(id);
            return Ok(new { deleted = isDeleted });
        }

        [HttpPost("Reply")]
        public async Task<IActionResult> Reply(AddReplyDTO replyMsg)
        {
            var validationResult = _addValidator.Validate(replyMsg);
            if (!validationResult.IsValid) return BadRequest(new { Errors = validationResult.Errors[0].ErrorMessage });
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var IsWritten = await _messageBL.ReplyMessage(replyMsg, userId);
            return Ok(new { addedSuccessfully = IsWritten });

        }


    }
}
