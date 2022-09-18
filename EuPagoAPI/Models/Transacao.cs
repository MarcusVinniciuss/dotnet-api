namespace EuPagoAPI.Models
{
    public class Transacao
    {
        private int _numeroParcelas = 1;
        public int Id { get; set; }
        public Compra Compra { get; set; }
        public DateTime DataTransacao { get; set; }
        public bool IsParcelado { get; set; }
        public int NumeroParcelas
        {
            get => _numeroParcelas;
            set => _numeroParcelas = value; 
        }
        public decimal ValorParcelas { get; set; }
        public string StatusTransacao { get; set; }
        public decimal ValorTotal { get; set; }
        public Cartao Cartao { get; set; }
    }
}
