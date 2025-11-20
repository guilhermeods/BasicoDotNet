using Bernhoeft.GRT.Teste.Application.Requests.Commands.v2;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v2;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v2;

namespace Bernhoeft.GRT.Teste.Api.Controllers.v2
{
    /// <summary>
    /// Endpoints da versão 2 da API de Avisos,
    /// incluindo criação, consulta, atualização e remoção (soft delete),
    /// além de auditoria com data de criação e atualização.
    /// </summary>
    /// <response code="401">Não Autenticado.</response>
    /// <response code="403">Não Autorizado.</response>
    /// <response code="500">Erro Interno no Servidor.</response>
    [AllowAnonymous]
    [ApiVersion("2.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = null)]
    public class AvisosController : RestApiController
    {
        /// <summary>
        /// Retorna Todos os Avisos Cadastrados (somente ativos) para edição.
        /// </summary>
        /// <response code="200">Sucesso.</response>
        /// <response code="204">Sem Avisos.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDocumentationRestResult<IEnumerable<GetAvisosV2Response>>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<object> GetAvisos(CancellationToken cancellationToken)
            => await Mediator.Send(new GetAvisosV2Request(), cancellationToken);

        /// <summary>
        /// Retorna um Aviso por ID.
        /// </summary>
        /// <response code="200">Sucesso.</response>
        /// <response code="400">Dados Inválidos.</response>
        /// <response code="404">Aviso Não Encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAvisoV2Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<object> GetAviso([FromRoute] GetAvisoV2Request request, CancellationToken cancellationToken)
            => await Mediator.Send(request, cancellationToken);

        /// <summary>
        /// Cria um novo aviso.
        /// </summary>
        /// <response code="201">Criado.</response>
        /// <response code="400">Dados inválidos.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<object> CreateAviso([FromBody] CreateAvisoV2Request request, CancellationToken cancellationToken)
            => await Mediator.Send(request, cancellationToken);

        /// <summary>
        /// Atualiza um aviso existente (somente mensagem).
        /// </summary>
        /// <response code="200">Sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="404">Aviso não encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<object> UpdateAviso([FromModel] UpdateAvisoV2Request request, CancellationToken cancellationToken)
            => await Mediator.Send(request, cancellationToken);

        /// <summary>
        /// Remove um aviso (soft delete).
        /// </summary>
        /// <response code="200">Sucesso.</response>
        /// <response code="404">Aviso não encontrado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<object> DeleteAviso([FromModel] DeleteAvisoV2Request request, CancellationToken cancellationToken)
            => await Mediator.Send(request, cancellationToken);


    }
}