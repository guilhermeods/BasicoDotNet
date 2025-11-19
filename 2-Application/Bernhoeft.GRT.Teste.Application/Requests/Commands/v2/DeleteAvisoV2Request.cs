using Bernhoeft.GRT.Core.Interfaces.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v2
{
    public class DeleteAvisoV2Request : IRequest<IOperationResult<bool>>
    {
        [FromRoute(Name = "id")]
        public int Id { get; set; }
    }
}
