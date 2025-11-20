using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v2.Validations
{
    /// <summary>
    /// Validação do DeleteAvisoV2, garantindo id valido.
    /// </summary>
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
