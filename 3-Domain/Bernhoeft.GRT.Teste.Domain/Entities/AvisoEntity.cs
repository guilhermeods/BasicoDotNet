namespace Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities
{
    public partial class AvisoEntity
    {
        public int Id { get; private set; }
        public bool Ativo { get; set; } = true;
        public string Titulo { get; set; }
        public string Mensagem { get; set; }

        //Campos exclusivos da V2 (auditoria + soft delete)
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
        public DateTime? AtualizadoEm { get; set; }
        public bool Excluido { get; set; } = false;
    }
}