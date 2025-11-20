using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities;
using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Core.Enums;
using Bernhoeft.GRT.Teste.Application.Handlers.Queries.v2;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v2;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Bernhoeft.GRT.Teste.IntegrationTests.Handlers
{
    public class GetAvisoV2HandlerTests
    {
        private readonly Mock<IAvisoRepository> _repoMock;
        private readonly IServiceProvider _sp;
        private readonly GetAvisoV2Handler _handler;

        public GetAvisoV2HandlerTests()
        {
            _repoMock = new Mock<IAvisoRepository>();

            var services = new ServiceCollection();
            services.AddSingleton(_repoMock.Object);

            _sp = services.BuildServiceProvider();
            _handler = new GetAvisoV2Handler(_sp);
        }

        /// <summary>
        /// Reflection para setar ID privado
        /// </summary>
        private static void SetId(AvisoEntity entity, int id)
        {
            typeof(AvisoEntity)
                .GetProperty("Id")!
                .SetValue(entity, id);
        }

        // -------------------------------------------------------------------
        // TESTE 1 — Retornar aviso com sucesso
        // -------------------------------------------------------------------
        [Fact]
        public async Task Deve_Retornar_Aviso_Quando_Existe_Ativo()
        {
            // Arrange
            var request = new GetAvisoV2Request { Id = 5 };

            var aviso = new AvisoEntity
            {
                Titulo = "Título Teste",
                Mensagem = "Mensagem Teste",
                Ativo = true,
                CriadoEm = DateTime.UtcNow.AddDays(-2),
                AtualizadoEm = DateTime.UtcNow.AddDays(-1)
            };

            SetId(aviso, 5);

            _repoMock
                .Setup(r => r.GetByIdAsync(5, It.IsAny<CancellationToken>()))
                .ReturnsAsync(aviso);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.StatusCode.Should().Be(CustomHttpStatusCode.Ok);
            result.Data.Should().NotBeNull();
            result.Data!.Id.Should().Be(5);
            result.Data.Titulo.Should().Be("Título Teste");
        }

        // -------------------------------------------------------------------
        // TESTE 2 — NotFound quando não existe
        // -------------------------------------------------------------------
        [Fact]
        public async Task Deve_Retornar_NotFound_Quando_Aviso_Nao_Existe()
        {
            // Arrange
            var request = new GetAvisoV2Request { Id = 999 };

            _repoMock
                .Setup(r => r.GetByIdAsync(999, It.IsAny<CancellationToken>()))
                .ReturnsAsync((AvisoEntity)null);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.StatusCode.Should().Be(CustomHttpStatusCode.NotFound);
        }

        // -------------------------------------------------------------------
        // TESTE 3 — NotFound quando está inativo
        // -------------------------------------------------------------------
        [Fact]
        public async Task Deve_Retornar_NotFound_Quando_Aviso_Inativo()
        {
            // Arrange
            var request = new GetAvisoV2Request { Id = 7 };

            var aviso = new AvisoEntity
            {
                Titulo = "Teste",
                Mensagem = "Msg",
                Ativo = false
            };

            SetId(aviso, 7);

            _repoMock
                .Setup(r => r.GetByIdAsync(7, It.IsAny<CancellationToken>()))
                .ReturnsAsync(aviso);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.StatusCode.Should().Be(CustomHttpStatusCode.NotFound);
        }
    }
}
