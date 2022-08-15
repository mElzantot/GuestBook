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
    public class EditMessageValidator : AbstractValidator<UpdateMessageDTO>
    {
        public EditMessageValidator()
        {
            RuleFor(m => m.MessageBody).NotEmpty().WithMessage("Message Can't be empty");
        }
    }
}
