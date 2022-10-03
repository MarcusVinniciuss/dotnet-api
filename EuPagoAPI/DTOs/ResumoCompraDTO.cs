namespace EuPagoAPI.DTOs
{
    public record ResumoCompraDTO
    {
        public string NomeEstabelecimento { get; set; }
        public long CNPJEstabelecimento { get; set; }
        public long NumeroCartao { get; set; }
        public string CVVCartao { get; set; }
        public decimal ValorCompra { get; set; }
        public DateTime DataCompra { get; set; }
        public int NumeroParcelas { get; set; }
        public decimal ValorParcelas { get; set; }
        public string StatusTransacao { get; set; }
        public decimal ValorTotalCartao { get; set; }
        public decimal TransacaoId { get; set; }
    }
}
