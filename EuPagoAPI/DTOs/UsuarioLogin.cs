namespace EuPagoAPI.DTOs
{
    public record UsuarioLogin
    {
        [Required]
        public long CPF { get; set; }

        [Required]
        public string Senha { get; set; }
    }
}
