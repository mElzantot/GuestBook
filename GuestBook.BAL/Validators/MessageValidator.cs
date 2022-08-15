using FluentValidation;
using GuestBook.BAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook.BAL.Validators
{
    public class AddReplyValidator : AbstractValidator<AddReplyDTO>
    {
        public AddReplyValidator()
        {
            RuleFor(m => m.MessageBody).NotEmpty().WithMessage("Message Can't be empty");
            RuleFor(m => m.ParnetMessageId).NotNull().NotEqual(0);
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
