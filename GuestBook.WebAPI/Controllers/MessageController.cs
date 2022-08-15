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
        private readonly IValidator<AddMessageDTO> _addValidator;
        private readonly IValidator<UpdateMessageDTO> _updateValidator;

        public MessageController(IMessageBL messageBL,
                                IValidator<AddMessageDTO> addValidator,
                                IValidator<UpdateMessageDTO> UpdateValidator
            )
        {
            _messageBL = messageBL;
            _addValidator = addValidator;
            _updateValidator = UpdateValidator;
        }

        [HttpPost("Write")]
        public async Task<IActionResult> Write(AddMessageDTO newMsg)
        {
            var validationResult = _addValidator.Validate(newMsg);
            if (!validationResult.IsValid) return BadRequest(new { Errors = validationResult.Errors[0].ErrorMessage });
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var IsWritten = await _messageBL.WriteNewMessage(newMsg, userId);
            return Ok(new { addedSuccessfully = IsWritten });
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateMessage(UpdateMessageDTO messageDTO)
        {
            var validationResult = _updateValidator.Validate(messageDTO);
            if (!validationResult.IsValid) return BadRequest(new { Errors = validationResult.Errors[0].ErrorMessage });
            var IsWritten = await _messageBL.UpdateMessage(messageDTO);
            return Ok(new { addedSuccessfully = IsWritten });
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> UpdateMessage(int id)
        {
            var IsWritten = await _messageBL.DeleteMessage(id);
            return Ok(new { addedSuccessfully = IsWritten });
        }


    }
}
