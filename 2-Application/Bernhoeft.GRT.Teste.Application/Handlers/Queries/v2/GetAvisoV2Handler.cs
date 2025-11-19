using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities;
using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Core.EntityFramework.Domain.Interfaces;
using Bernhoeft.GRT.Core.Enums;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v2;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v2;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Queries.v2
{
    public class GetAvisoV2Handler : IRequestHandler<GetAvisoV2Request, IOperationResult<GetAvisoV2Response>>
    {
        private readonly IServiceProvider _serviceProvider;

        private IContext _context => _serviceProvider.GetRequiredService<IContext>();
        private IAvisoRepository _repo => _serviceProvider.GetRequiredService<IAvisoRepository>();

        public GetAvisoV2Handler(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public async Task<IOperationResult<GetAvisoV2Response>> Handle(GetAvisoV2Request request, CancellationToken cancellationToken)
        {
            
            var entity = await _repo.GetByIdAsync(request.Id, cancellationToken);

            
            if (entity == null)
                return OperationResult<GetAvisoV2Response>.ReturnNotFound();

            
            if (!entity.Ativo)
                return OperationResult<GetAvisoV2Response>.ReturnNotFound();

            var response = (GetAvisoV2Response)entity;

            return OperationResult<GetAvisoV2Response>.ReturnOk(response);
        }
    }
}
