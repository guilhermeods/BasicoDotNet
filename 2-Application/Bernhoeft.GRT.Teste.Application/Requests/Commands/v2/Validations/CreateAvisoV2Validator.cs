using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v2.Validations
{
    /// <summary>
    /// Validação do CreateAvisoV2, garantindo título e mensagem obrigatórios.
    /// </summary>
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
