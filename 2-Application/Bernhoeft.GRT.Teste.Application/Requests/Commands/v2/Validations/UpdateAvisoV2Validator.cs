using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v2.Validations
{
    public class UpdateAvisoV2Validator : AbstractValidator<UpdateAvisoV2Request>
    {
        public UpdateAvisoV2Validator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Id inválido.");

            RuleFor(x => x.Mensagem)
                .NotEmpty()
                .WithMessage("A nova mensagem é obrigatória.");
        }
    }
}
