using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Queries.v2.Validations
{
    /// <summary>
    /// Validação do GetAvisoV2, garantindo id valido.
    /// </summary>
    public class GetAvisoV2Validator : AbstractValidator<GetAvisoV2Request>
    {
        public GetAvisoV2Validator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("O Id deve ser maior que zero.");
        }
    }
}
