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
    public class GetAvisosV2HandlerTests
    {
        private readonly Mock<IAvisoRepository> _repoMock;
        private readonly IServiceProvider _sp;
        private readonly GetAvisosV2Handler _handler;

        public GetAvisosV2HandlerTests()
        {
            _repoMock = new Mock<IAvisoRepository>();

            var services = new ServiceCollection();
            services.AddSingleton(_repoMock.Object);

            _sp = services.BuildServiceProvider();
            _handler = new GetAvisosV2Handler(_sp);
        }

        /// <summary>
        /// Reflection para setar ID
        /// </summary>
        private static void SetId(AvisoEntity entity, int id)
        {
            typeof(AvisoEntity).GetProperty("Id")!.SetValue(entity, id);
        }

        // -------------------------------------------------------------------
        // TESTE 1 — Deve retornar lista de avisos ativos
        // -------------------------------------------------------------------
        [Fact]
        public async Task Deve_Retornar_Lista_De_Avisos_Ativos()
        {
            // Arrange
            var lista = new List<AvisoEntity>
            {
                new AvisoEntity { Titulo = "A1", Mensagem = "M1", Ativo = true },
                new AvisoEntity { Titulo = "A2", Mensagem = "M2", Ativo = true }
            };

            SetId(lista[0], 1);
            SetId(lista[1], 2);

            _repoMock
                .Setup(r => r.ObterTodosAvisosAsync(TrackingBehavior.NoTracking, It.IsAny<CancellationToken>()))
                .ReturnsAsync(lista);

            // Act
            var result = await _handler.Handle(new GetAvisosV2Request(), CancellationToken.None);

            // Assert
            result.StatusCode.Should().Be(CustomHttpStatusCode.Ok);
            result.Data.Should().NotBeNull();
            result.Data!.Count().Should().Be(2);
        }

        // -------------------------------------------------------------------
        // TESTE 2 — Deve retornar NoContent quando não há avisos
        // -------------------------------------------------------------------
        [Fact]
        public async Task Deve_Retornar_NoContent_Quando_Lista_Vazia()
        {
            // Arrange
            _repoMock
                .Setup(r => r.ObterTodosAvisosAsync(TrackingBehavior.NoTracking, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<AvisoEntity>());

            // Act
            var result = await _handler.Handle(new GetAvisosV2Request(), CancellationToken.None);

            // Assert
            result.StatusCode.Should().Be(CustomHttpStatusCode.NoContent);
        }

        // -------------------------------------------------------------------
        // TESTE 3 — Deve ignorar avisos inativos
        // -------------------------------------------------------------------
        [Fact]
        public async Task Deve_Ignorar_Avisos_Inativos()
        {
            // Arrange
            var lista = new List<AvisoEntity>
            {
                new AvisoEntity { Titulo = "Ativo", Mensagem = "OK", Ativo = true },
                new AvisoEntity { Titulo = "Inativo", Mensagem = "X", Ativo = false }
            };

            SetId(lista[0], 1);
            SetId(lista[1], 2);

            _repoMock
                .Setup(r => r.ObterTodosAvisosAsync(TrackingBehavior.NoTracking, It.IsAny<CancellationToken>()))
                .ReturnsAsync(lista);

            // Act
            var result = await _handler.Handle(new GetAvisosV2Request(), CancellationToken.None);

            // Assert
            result.StatusCode.Should().Be(CustomHttpStatusCode.Ok);
            result.Data!.Count().Should().Be(1);
            result.Data!.First().Titulo.Should().Be("Ativo");
        }
    }
}
