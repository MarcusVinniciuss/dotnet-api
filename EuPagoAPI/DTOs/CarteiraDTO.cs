namespace EuPagoAPI.DTOs
{
    public record CarteiraDTO
    {
        public IList<CartaoDTO> Cartoes { get; set; }

        public long UsuarioId { get; set; }
    }
}
