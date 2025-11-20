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
    /// <summary>
    /// Handler responsável por retornar a lista de avisos apenas os ativos.
    /// </summary>
    public class GetAvisosV2Handler : IRequestHandler<GetAvisosV2Request, IOperationResult<IEnumerable<GetAvisosV2Response>>>
    {
        private readonly IServiceProvider _serviceProvider;

        private IContext _context => _serviceProvider.GetRequiredService<IContext>();
        private IAvisoRepository _avisoRepository => _serviceProvider.GetRequiredService<IAvisoRepository>();

        public GetAvisosV2Handler(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task<IOperationResult<IEnumerable<GetAvisosV2Response>>> Handle(GetAvisosV2Request request, CancellationToken cancellationToken)
        {
            var entity = await _avisoRepository.ObterTodosAvisosAsync(TrackingBehavior.NoTracking);
            if (!entity.Any())
                return OperationResult<IEnumerable<GetAvisosV2Response>>.ReturnNoContent();


            var ativos = entity.Where(x => x.Ativo).ToList();
            if (!ativos.Any())
                return OperationResult<IEnumerable<GetAvisosV2Response>>.ReturnNotFound();

            return OperationResult<IEnumerable<GetAvisosV2Response>>.ReturnOk(
                ativos.Select(x => (GetAvisosV2Response)x));
        }
    }
}