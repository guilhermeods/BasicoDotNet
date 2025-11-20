using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v2;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Requests.Queries.v2
{
    /// <summary>
    /// Request para retornar a lista de avisos ativos.
    /// </summary>
    public class GetAvisosV2Request : IRequest<IOperationResult<IEnumerable<GetAvisosV2Response>>>
    {
    }
}