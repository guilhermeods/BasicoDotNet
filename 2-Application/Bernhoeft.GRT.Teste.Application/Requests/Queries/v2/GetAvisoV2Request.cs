using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v2;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bernhoeft.GRT.Teste.Application.Requests.Queries.v2
{
    public class GetAvisoV2Request : IRequest<IOperationResult<GetAvisoV2Response>>
    {
        [FromRoute(Name = "id")]
        public int Id { get; set; }
    }
}
