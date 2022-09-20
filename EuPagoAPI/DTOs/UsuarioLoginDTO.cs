namespace EuPagoAPI.DTOs
{
    public record UsuarioLoginDTO
    {
        public UsuarioDTO usuario { get; set; }
        public BearerTokenDTO bearer { get; set; }
    }
}
