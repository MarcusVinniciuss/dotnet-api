﻿namespace EuPagoAPI.Models
{
    public class Estabelecimento
    {
        public decimal Id { get; set; }
        public string Nome { get; set; }
        public long CNPJ { get; set; }
        public string StatusCadastro { get; set; }
    }
}
