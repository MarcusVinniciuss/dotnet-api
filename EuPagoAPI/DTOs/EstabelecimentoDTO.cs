namespace EuPagoAPI.DTOs
{
    public record EstabelecimentoDTO
    {
        public string Nome { get; set; }
        public long CNPJ { get; set; }
    }
}