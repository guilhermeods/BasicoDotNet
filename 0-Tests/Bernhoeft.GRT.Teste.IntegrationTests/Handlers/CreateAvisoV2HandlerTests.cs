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
    public class CreateAvisoV2HandlerTests
    {
        private readonly Mock<IAvisoRepository> _repoMock;
        private readonly Mock<IContext> _contextMock;
        private readonly IServiceProvider _sp;
        private readonly CreateAvisoV2Handler _handler;

        private static void SetId(AvisoEntity e, int id)
        {
            typeof(AvisoEntity).GetProperty("Id")!.SetValue(e, id);
        }
        public CreateAvisoV2HandlerTests()
        {
            _repoMock = new Mock<IAvisoRepository>();
            _contextMock = new Mock<IContext>();

            var services = new ServiceCollection();
            services.AddSingleton(_repoMock.Object);
            services.AddSingleton(_contextMock.Object);

            _sp = services.BuildServiceProvider();
            _handler = new CreateAvisoV2Handler(_sp);
        }

        [Fact]
        public async Task Deve_Criar_Aviso_Com_Sucesso()
        {
            // Arrange
            var request = new CreateAvisoV2Request
            {
                Titulo = "Novo",
                Mensagem = "Teste"
            };

            var avisoCriado = new AvisoEntity
            {
                Titulo = request.Titulo,
                Mensagem = request.Mensagem,
                Ativo = true
            };
            _repoMock
                .Setup(r => r.AddAsync(It.IsAny<AvisoEntity>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((AvisoEntity entity, CancellationToken _) =>
                {
                    SetId(entity, 10);
                    return entity;
                });


            _contextMock
                .Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.StatusCode.Should().Be(CustomHttpStatusCode.Ok);
            result.Data.Should().BeGreaterThan(0);
            (result.Messages ?? Enumerable.Empty<string>())
                .Should()
                .BeEmpty();


            _repoMock.Verify(r => r.AddAsync(It.IsAny<AvisoEntity>(), It.IsAny<CancellationToken>()), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
