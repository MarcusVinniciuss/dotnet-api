namespace EuPagoAPI.DTOs
{
    public record EstabelecimentoDTO
    {
        public decimal Id { get; set; }
        public string Nome { get; set; }
        public long CNPJ { get; set; }
    }
}