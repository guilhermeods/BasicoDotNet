using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v2;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v2
{
    public class CreateAvisoV2Request : IRequest<IOperationResult<bool>>
    {
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
    }
}
