using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities;
using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Core.EntityFramework.Domain.Interfaces;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v2;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Commands.v2
{
    public class CreateAvisoV2Handler : IRequestHandler<CreateAvisoV2Request, IOperationResult<bool>>
    {
        private readonly IServiceProvider _serviceProvider;

        private IContext _context => _serviceProvider.GetRequiredService<IContext>();
        private IAvisoRepository _repo => _serviceProvider.GetRequiredService<IAvisoRepository>();

        public CreateAvisoV2Handler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<IOperationResult<bool>> Handle(CreateAvisoV2Request request, CancellationToken cancellationToken)
        {
            var entity = new AvisoEntity
            {
                Titulo = request.Titulo,
                Mensagem = request.Mensagem,
                Ativo = true,
                CriadoEm = DateTime.UtcNow,
                AtualizadoEm = DateTime.UtcNow
            };

            await _repo.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return OperationResult<bool>.ReturnOk(true);
        }
    }
}
