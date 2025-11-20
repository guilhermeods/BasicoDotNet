using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Core.EntityFramework.Domain.Interfaces;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v2;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Commands.v2
{
    /// <summary>
    /// Handler responsável por atualizar a mensagem do aviso.
    /// </summary>
    public class UpdateAvisoV2Handler : IRequestHandler<UpdateAvisoV2Request, IOperationResult<bool>>
    {
        private readonly IServiceProvider _serviceProvider;

        private IAvisoRepository _repo => _serviceProvider.GetRequiredService<IAvisoRepository>();
        private IContext _context => _serviceProvider.GetRequiredService<IContext>();

        public UpdateAvisoV2Handler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<IOperationResult<bool>> Handle(UpdateAvisoV2Request request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id, cancellationToken);

            if (entity == null)
                return OperationResult<bool>.ReturnNotFound();

            if (!entity.Ativo)
                return OperationResult<bool>.ReturnNotFound();

            entity.Mensagem = request.Mensagem;
            entity.AtualizadoEm = DateTime.UtcNow;

            _repo.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return OperationResult<bool>.ReturnOk(true);
        }
    }
}
