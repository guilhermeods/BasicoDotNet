using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Queries.v2.Validations
{
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
