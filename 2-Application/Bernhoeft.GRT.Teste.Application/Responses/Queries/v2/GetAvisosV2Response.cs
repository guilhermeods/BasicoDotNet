using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities;

namespace Bernhoeft.GRT.Teste.Application.Responses.Queries.v2
{
    public class GetAvisosV2Response
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
        public DateTime CriadoEm { get; set; } 
        public DateTime? AtualizadoEm { get; set; }
        public bool Excluido { get; set; }

        public static implicit operator GetAvisosV2Response(AvisoEntity entity) => new()
        {
            Id = entity.Id,
            Ativo = entity.Ativo,
            Titulo = entity.Titulo,
            Mensagem = entity.Mensagem,
            CriadoEm = entity.CriadoEm,
            AtualizadoEm = entity.AtualizadoEm,
            Excluido = entity.Excluido
        };
    }
}