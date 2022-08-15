using FluentValidation;
using GuestBook.BAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook.BAL.Validators
{
    public class AddMessageValidator : AbstractValidator<AddMessageDTO>
    {
        public AddMessageValidator()
        {
            RuleFor(m => m.MessageBody).NotEmpty().WithMessage("Message Can't be empty");
        }
    }
    public class UpdateMessageValidator : AbstractValidator<UpdateMessageDTO>
    {
        public UpdateMessageValidator()
        {
            RuleFor(m => m.Id).NotNull().NotEqual(0);
            RuleFor(m => m.MessageBody).NotEmpty().WithMessage("Message Can't be empty");
        }
    }
}
