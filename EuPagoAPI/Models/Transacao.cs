using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EuPagoAPI.Models
{
    [Table("TB_EUPAGO_TRANSACAO")]
    public class Transacao
    {
        private int _numeroParcelas = 1;
        private bool _isParcelado;

        [Key]
        [Column("ID_TRANSACAO")]
        public decimal Id { get; set; }

        public Compra Compra { get; set; }

        [Column("ID_COMPRA")]
        public decimal CompraId { get; set; }

        [Column("DT_TRANSACAO")]
        public DateTime DataTransacao { get; set; }

        [Column("ST_PARCELADO")]
        public bool IsParcelado { 
            get
            {
                return _isParcelado;
            }
            set
            {
                _isParcelado = Convert.ToBoolean(value);
            }
        }

        [Column("NR_PARCELAS")]
        public int NumeroParcelas
        {
            get => _numeroParcelas;
            set => _numeroParcelas = value; 
        }

        [Column("VL_PARCELAS")]
        public decimal ValorParcelas { get; set; }

        [Column("ST_TRANSACAO")]
        public string StatusTransacao { get; set; }

        [Column("VL_TOTAL_CARTAO")]
        public decimal ValorTotal { get; set; }
    }
}
