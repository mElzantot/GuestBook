using FluentValidation;
using GuestBook.BAL.DTO;
using GuestBook.BAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GuestBook.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IValidator<AuthRequestDTO> _validator;

        //private readonly IGuestBL _guestBL;

        public AuthController(IValidator<AuthRequestDTO> validator)
        {
            _validator = validator;
            //_guestBL = guestBL;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(AuthRequestDTO authRequestDTO)
        {
            var validationResult = _validator.Validate(authRequestDTO);
            if (!validationResult.IsValid) return BadRequest(new { Errors = validationResult.Errors[0].ErrorMessage });
            return Ok();
        }
    }
}