using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v2;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Requests.Queries.v2
{
    public class GetAvisosV2Request : IRequest<IOperationResult<IEnumerable<GetAvisosV2Response>>>
    {
    }
}