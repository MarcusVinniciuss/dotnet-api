using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EuPagoAPI.Models
{
    [Table("TB_EUPAGO_ESTABELECIMENTO")]
    public class Estabelecimento
    {
        [Key]
        [Column("ID_ESTABELECIMENTO")]
        public decimal Id { get; set; }

        [Column("NM_ESTABELECIMENTO")]
        public string Nome { get; set; }

        [Column("NR_CNPJ")]
        public long CNPJ { get; set; }

        [Column("ST_CADASTRO")]
        public string StatusCadastro { get; set; }
    }
}
