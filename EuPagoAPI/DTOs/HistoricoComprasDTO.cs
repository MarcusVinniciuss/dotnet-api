namespace EuPagoAPI.DTOs
{
    public record HistoricoComprasDTO
    {
        public decimal UsuarioId { get; set; }
        public List<ResumoCompraDTO> historicoCompras { get; set; }
    }
}
