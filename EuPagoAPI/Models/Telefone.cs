namespace EuPagoAPI.Models
{
    [Table("TB_EUPAGO_TELEFONE")]
    public class Telefone
    {
        [Column("ID_TELEFONE")]
        [Key]
        public decimal Id { get; set; }

        [Column("NR_DDD")]
        [Required(ErrorMessage = "DDD do telefone é obrigatório.")]
        [Range(11, 99, ErrorMessage = "DDD inválido.")]
        public int DDD { get; set; }

        [Column("NR_TELEFONE")]
        [Required(ErrorMessage = "Número de telefone é obrigatório.")]
        [Range(10000000, 999999999, ErrorMessage = "Número de telefone deve ter entre 8-9 números.")]
        public long Numero { get; set; }

        Usuario Usuario { get; set; }

        [Column("ID_USUARIO")]
        [ForeignKey("Usuario")]
        public decimal UsuarioId { get; set; }
    }
}
