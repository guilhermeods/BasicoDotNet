using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v2.Validations
{
    public class CreateAvisoV2Validator : AbstractValidator<CreateAvisoV2Request>
    {
        public CreateAvisoV2Validator()
        {
            RuleFor(x => x.Titulo)
            .NotEmpty().WithMessage("O título é obrigatório.")
            .MaximumLength(50).WithMessage("O título deve ter no máximo 50 caracteres.");

            RuleFor(x => x.Mensagem)
                .NotEmpty().WithMessage("A mensagem é obrigatória.");
        }

    }
                
}
