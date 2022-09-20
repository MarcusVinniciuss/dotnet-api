namespace EuPagoAPI.DTOs
{
    public record UsuarioDTO
    {
        public string Nome { get; set; }
        public long CPF { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }
        public string StatusVisao { get; set; }
        public string Email { get; set; }
        public string StatusCadastro { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public TelefoneDTO Telefone { get; set; }
        public EnderecoDTO Endereco { get; set; }
    }
}
