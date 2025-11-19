using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Validators;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v2.Validations
{
    public class DeleteAvisoV2Validator: AbstractValidator<DeleteAvisoV2Request>
    {
        public DeleteAvisoV2Validator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Id inválido.");
        }
    }
}
