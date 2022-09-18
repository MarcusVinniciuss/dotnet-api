namespace EuPagoAPI.DTOs
{
    public class UsuarioLoginDTO
    {
        public UsuarioDTO usuario { get; set; }
        public BearerTokenDTO bearer { get; set; }
    }
}
