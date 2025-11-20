using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities;
using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Core.EntityFramework.Domain.Interfaces;
using Bernhoeft.GRT.Core.Enums;
using Bernhoeft.GRT.Teste.Application.Handlers.Commands.v2;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v2;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Bernhoeft.GRT.Teste.IntegrationTests.Handlers
{
    public class UpdateAvisoV2HandlerTests
    {
        private readonly Mock<IAvisoRepository> _repoMock;
        private readonly Mock<IContext> _contextMock;
        private readonly IServiceProvider _sp;
        private readonly UpdateAvisoV2Handler _handler;

        public UpdateAvisoV2HandlerTests()
        {
            _repoMock = new Mock<IAvisoRepository>();
            _contextMock = new Mock<IContext>();

            var services = new ServiceCollection();
            services.AddSingleton(_repoMock.Object);
            services.AddSingleton(_contextMock.Object);

            _sp = services.BuildServiceProvider();
            _handler = new UpdateAvisoV2Handler(_sp);
        }

        /// <summary>
        /// Reflection para setar ID privado
        /// </summary>
        private static void SetId(AvisoEntity e, int id)
        {
            typeof(AvisoEntity).GetProperty("Id")!.SetValue(e, id);
        }

        // ---------------------------------------------------------------------------------------------
        // TESTE 1 — Atualização com sucesso
        // ---------------------------------------------------------------------------------------------
        [Fact]
        public async Task Deve_Atualizar_Aviso_Com_Sucesso()
        {
            // Arrange
            var request = new UpdateAvisoV2Request
            {
                Id = 10,
                Mensagem = "Mensagem atualizada"
            };

            var avisoExistente = new AvisoEntity
            {
                Titulo = "Titulo Original",
                Mensagem = "Mensagem antiga",
                Ativo = true,
                CriadoEm = DateTime.UtcNow.AddDays(-2),
                AtualizadoEm = DateTime.UtcNow.AddDays(-1)
            };

            SetId(avisoExistente, 10);

            _repoMock
                .Setup(r => r.GetByIdAsync(10, It.IsAny<CancellationToken>()))
                .ReturnsAsync(avisoExistente);

            _contextMock
                .Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.StatusCode.Should().Be(CustomHttpStatusCode.Ok);
            avisoExistente.Mensagem.Should().Be("Mensagem atualizada");

            _repoMock.Verify(r => r.Update(It.IsAny<AvisoEntity>()), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        // ---------------------------------------------------------------------------------------------
        // TESTE 2 — NotFound quando não existe
        // ---------------------------------------------------------------------------------------------
        [Fact]
        public async Task Deve_Retornar_NotFound_Quando_Aviso_Nao_Encontrado()
        {
            // Arrange
            var request = new UpdateAvisoV2Request
            {
                Id = 999,
                Mensagem = "Nova mensagem"
            };

            _repoMock
                .Setup(r => r.GetByIdAsync(999, It.IsAny<CancellationToken>()))
                .ReturnsAsync((AvisoEntity)null);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.StatusCode.Should().Be(CustomHttpStatusCode.NotFound);
        }

        // ---------------------------------------------------------------------------------------------
        // TESTE 3 — NotFound quando aviso está inativo
        // ---------------------------------------------------------------------------------------------
        [Fact]
        public async Task Deve_Retornar_NotFound_Quando_Aviso_Inativo()
        {
            // Arrange
            var request = new UpdateAvisoV2Request
            {
                Id = 10,
                Mensagem = "Teste"
            };

            var avisoInativo = new AvisoEntity
            {
                Titulo = "Teste",
                Mensagem = "Mensagem",
                Ativo = false
            };

            SetId(avisoInativo, 10);

            _repoMock
                .Setup(r => r.GetByIdAsync(10, It.IsAny<CancellationToken>()))
                .ReturnsAsync(avisoInativo);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.StatusCode.Should().Be(CustomHttpStatusCode.NotFound);
        }
    }
}
