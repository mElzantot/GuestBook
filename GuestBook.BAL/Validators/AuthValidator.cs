using FluentValidation;
using GuestBook.BAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook.BAL.Validators
{
    public class AuthValidator : AbstractValidator<AuthRequestDTO>
    {
        public AuthValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User Name is Required");
            RuleFor(x => x.UserName).MinimumLength(2).MaximumLength(15).WithMessage("User Name is Required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is Required");
            RuleFor(x => x.Password).MinimumLength(6).MaximumLength(20).WithMessage("Password length must be between 6 & 20 characters");
        }
    }

}
