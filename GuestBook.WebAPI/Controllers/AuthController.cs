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
        private readonly IAuthBL _authBL;


        public AuthController(IValidator<AuthRequestDTO> validator, IAuthBL authBL)
        {
            _validator = validator;
            _authBL = authBL;
        }



        [HttpPost("Register")]
        public async Task<IActionResult> Register(AuthRequestDTO authRequestDTO)
        {
            var validationResult = _validator.Validate(authRequestDTO);
            if (!validationResult.IsValid) return BadRequest(new { Errors = validationResult.Errors[0].ErrorMessage });
            if (await _authBL.CheckIfUserNameExist(authRequestDTO.UserName))
                return BadRequest(new { Errors = "User Name is already exist , Please try another one" });
            var userToken = await _authBL.Register(authRequestDTO);
            return Ok(userToken);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(AuthRequestDTO authRequestDTO)
        {
            var validationResult = _validator.Validate(authRequestDTO);
            if (!validationResult.IsValid) return BadRequest(new { Errors = validationResult.Errors[0].ErrorMessage });
            var tokens = await _authBL.Login(authRequestDTO);
            return tokens == null ? BadRequest("User Not Found") : Ok(tokens);
        }
    }
}