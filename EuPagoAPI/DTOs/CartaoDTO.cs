namespace EuPagoAPI.DTOs
{
    public record CartaoDTO
    {
        public string NomeImpresso { get; set; }
        public long Numero { get; set; }
        public int MesValidade { get; set; }
        public int AnoValidade { get; set; }
        public string CVV { get; set; }
        public string Emissora { get; set; }
        public decimal UsuarioId { get; set; }
    }
}
