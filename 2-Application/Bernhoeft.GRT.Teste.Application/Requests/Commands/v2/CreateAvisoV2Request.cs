using Bernhoeft.GRT.Core.Interfaces.Results;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v2
{
    /// <summary>
    /// Request para criação de aviso.
    /// </summary>
    public class CreateAvisoV2Request : IRequest<IOperationResult<int>>
    {
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
    }
}
