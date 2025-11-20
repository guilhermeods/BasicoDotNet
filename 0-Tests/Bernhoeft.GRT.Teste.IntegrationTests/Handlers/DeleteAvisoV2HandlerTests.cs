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
    public class DeleteAvisoV2HandlerTests
    {
        private readonly Mock<IAvisoRepository> _repoMock;
        private readonly Mock<IContext> _contextMock;
        private readonly IServiceProvider _serviceProvider;
        private readonly DeleteAvisoV2Handler _handler;

        public DeleteAvisoV2HandlerTests()
        {
            _repoMock = new Mock<IAvisoRepository>();
            _contextMock = new Mock<IContext>();

            var services = new ServiceCollection();
            services.AddSingleton(_repoMock.Object);
            services.AddSingleton(_contextMock.Object);

            _serviceProvider = services.BuildServiceProvider();
            _handler = new DeleteAvisoV2Handler(_serviceProvider);
        }

        /// <summary>
        /// Utilitário para setar ID privado da entidade via reflection
        /// </summary>
        private static void SetId(AvisoEntity entity, int id)
        {
            typeof(AvisoEntity)
                .GetProperty("Id")!
                .SetValue(entity, id);
        }

        // ---------------------------------------------------------------------------------------------
        // TESTE 1 — DELETAR AVISO EXISTENTE E ATIVO
        // ---------------------------------------------------------------------------------------------
        [Fact]
        public async Task Deve_Deletar_Quando_Aviso_Existe_E_Esta_Ativo()
        {
            // Arrange
            var request = new DeleteAvisoV2Request { Id = 10 };

            var aviso = new AvisoEntity
            {
                Titulo = "Teste",
                Mensagem = "Mensagem",
                Ativo = true,
                CriadoEm = DateTime.UtcNow,
                AtualizadoEm = DateTime.UtcNow
            };

            SetId(aviso, 10);

            _repoMock
                .Setup(r => r.GetByIdAsync(10, It.IsAny<CancellationToken>()))
                .ReturnsAsync(aviso);

            _contextMock
                .Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(CustomHttpStatusCode.Ok);
            aviso.Ativo.Should().BeFalse();

            _repoMock.Verify(r => r.Update(It.IsAny<AvisoEntity>()), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        // ---------------------------------------------------------------------------------------------
        // TESTE 2 — NOT FOUND SE NÃO EXISTE
        // ---------------------------------------------------------------------------------------------
        [Fact]
        public async Task Deve_Retornar_NotFound_Quando_Aviso_Nao_Existe()
        {
            // Arrange
            var request = new DeleteAvisoV2Request { Id = 999 };

            _repoMock
                .Setup(r => r.GetByIdAsync(999, It.IsAny<CancellationToken>()))
                .ReturnsAsync((AvisoEntity)null);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.StatusCode.Should().Be(CustomHttpStatusCode.NotFound);
        }

        // ---------------------------------------------------------------------------------------------
        // TESTE 3 — NOT FOUND SE JÁ ESTÁ INATIVO
        // ---------------------------------------------------------------------------------------------
        [Fact]
        public async Task Deve_Retornar_NotFound_Quando_Aviso_Ja_Inativo()
        {
            // Arrange
            var request = new DeleteAvisoV2Request { Id = 10 };

            var aviso = new AvisoEntity
            {
                Titulo = "Teste",
                Mensagem = "Mensagem",
                Ativo = false
            };

            SetId(aviso, 10);

            _repoMock
                .Setup(r => r.GetByIdAsync(10, It.IsAny<CancellationToken>()))
                .ReturnsAsync(aviso);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.StatusCode.Should().Be(CustomHttpStatusCode.NotFound);
        }
    }
}
