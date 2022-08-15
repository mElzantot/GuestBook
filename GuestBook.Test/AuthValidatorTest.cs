using FluentValidation.TestHelper;
using GuestBook.BAL.DTO;
using GuestBook.BAL.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook.Test
{

    public class AuthValidatorTest
    {
        private readonly AuthValidator _validator = new AuthValidator();


        [Fact]
        public void GiveAnEmptyName_ShouldHaveValidationError()
        {
            AuthRequestDTO authRequestDTO = new AuthRequestDTO
            {
                UserName = String.Empty,
                Password = "Mohamed"
            };
            var result = _validator.TestValidate(authRequestDTO);
            result.ShouldHaveValidationErrorFor(user => user.UserName);
        }
        [Fact]
        public void GiveAnOneCharName_ShouldHaveValidationError()
        {
            AuthRequestDTO authRequestDTO = new AuthRequestDTO
            {
                UserName = "S",
                Password = "Mohamed"
            };
            var result = _validator.TestValidate(authRequestDTO);
            result.ShouldHaveValidationErrorFor(user => user.UserName);
        }

        [Fact]
        public void GiveAnEmptyPassword_ShouldHaveValidationError()
        {
            AuthRequestDTO authRequestDTO = new AuthRequestDTO
            {
                UserName = "Mohamed",
                Password = ""
            };
            var result = _validator.TestValidate(authRequestDTO);
            result.ShouldHaveValidationErrorFor(user => user.Password);
        }

        [Fact]
        public void GiveValidUSerNameAndPassword_ShouldNotHaveValidationErrors()
        {
            AuthRequestDTO authRequestDTO = new AuthRequestDTO
            {
                UserName = "Mohamed",
                Password = "shaker"
            };
            var result = _validator.TestValidate(authRequestDTO);
            result.ShouldNotHaveAnyValidationErrors();
        }

    }
}
