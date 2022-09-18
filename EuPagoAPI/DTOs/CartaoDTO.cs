namespace EuPagoAPI.DTOs
{
    public class CartaoDTO
    {
        public string NomeImpresso { get; set; }
        public string? CPF { get; set; }
        public string? CNPJ { get; set; }
        public string MesValidade { get; set; }
        public string AnoValidade { get; set; }
        public string CVV { get; set; }
    }
}
