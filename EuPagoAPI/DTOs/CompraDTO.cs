namespace EuPagoAPI.DTOs
{
    public record CompraDTO
    {
        public string NomeEstabelecimento { get; set; }
        public long CnpjEstabelecimento { get; set; }
        public string CvvCartao { get; set; }
        public DateTime DataCompra { get; set; }
        public long NumeroCartao { get; set; }
        public int NumeroParcelas { get; set; }
        public string StatusTransacao { get; set; }
        public decimal TransacaoId { get; set; }
        public decimal ValorCompra { get; set; }
        public decimal ValorParcelas { get; set; }
        public decimal ValorTotalCartao { get; set; }
        public decimal UsuarioId { get; set; }
        public decimal CartaoId { get; set; }
    }
}
