namespace EuPagoAPI.DTOs
{
    public record CompraDTO
    {
        public decimal UsuarioId { get; set; }
        public decimal CartaoId { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
