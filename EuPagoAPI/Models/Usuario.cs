using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EuPagoAPI.Models
{
    [Table("TB_EUPAGO_USUARIO")]
    public class Usuario
    {
        [Key]
        [Column("ID_USUARIO")]
        public decimal Id { get; set; }

        [Column("DS_NOME_COMPLETO")]
        [StringLength(250)]
        [Required(ErrorMessage = "Nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Telefone é obrigatório.")]
        public Telefone Telefone { get; set; }

        [Required(ErrorMessage = "Endereço é obrigatório.")]
        public Endereco Endereco { get; set; }

        [Column("NR_CPF")]
        [Required(ErrorMessage = "CPF é obrigatório.")]
        public long CPF { get; set; }

        [Column("DS_SENHA")]
        [Required]
        [MinLength(10, ErrorMessage = "Senha deve possuir o mínimo de 10 caracteres.")]
        [MaxLength(20, ErrorMessage = "Senha deve possuir no máximo 20 caracteres.")]
        public string Senha { get; set; }

        [Column("DT_NASCIMENTO")]
        [Required(ErrorMessage = "Data de nascimento é obrigatório.")]
        public DateTime DataNascimento { get; set; }

        [Column("ST_VISAO")]
        [Required(ErrorMessage = "É obrigatório informar o nível de visibilidade do usuário.")]
        public string StatusVisao { get; set; }

        [Column("DS_EMAIL")]
        [Required(ErrorMessage = "Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Email { get; set; }

        [Column("ST_CADASTRO")]
        public string StatusCadastro { get; set; }

        [Column("DT_CADASTRO")]
        public DateTime DataCadastro { get; set; }

        [Column("DT_ATUALIZACAO")]
        public DateTime DataAtualizacao { get; set; }
    }
}
