using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EuPagoAPI.Models
{
    [Table("TB_EUPAGO_COMPRA")]
    public class Compra
    {
        [Key]
        [Column("ID_COMPRA")]
        public decimal Id { get; set; }

        public Estabelecimento Estabelecimento { get; set; }

        [Column("ID_ESTABELECIMENTO")]
        [ForeignKey("Estabelecimento")]
        public decimal EstabelecimentoId { get; set; }

        public Usuario Usuario { get; set; }

        [Column("ID_USUARIO")]
        [ForeignKey("Usuario")]
        public decimal UsuarioId { get; set; }

        public Cartao Cartao { get; set; }

        [Column("ID_CARTAO")]
        [ForeignKey("Cartao")]
        public decimal CartaoId { get; set; }

        [Column("VL_TOTAL_COMPRA")]
        public decimal ValorTotal { get; set; }
    }
}