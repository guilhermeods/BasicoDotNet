using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v2;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bernhoeft.GRT.Teste.Application.Requests.Queries.v2
{
    /// <summary>
    /// Request para retornar a aviso apartir do id.
    /// </summary>
    public class GetAvisoV2Request : IRequest<IOperationResult<GetAvisoV2Response>>
    {
        [FromRoute(Name = "id")]
        public int Id { get; set; }
    }
}
