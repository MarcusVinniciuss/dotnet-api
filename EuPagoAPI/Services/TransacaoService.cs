using EuPagoAPI.Data; 
using EuPagoAPI.Models;
using EuPagoAPI.Utils;
using Microsoft.EntityFrameworkCore;

namespace EuPagoAPI.Services
{
    public class TransacaoService : ServiceBase
    {
        public TransacaoService(DataContext dataContext) : base(dataContext)
        {
        }

        private static bool MockIsParcelado()
        {
            var random = new Random();
            // gera um int random de 0 à 1
            return Convert.ToBoolean(random.Next(2));
        }

        private static int MockNumeroParcelas()
        {
            var random = new Random();
            // gera um int random de 2 à 13
            return random.Next(2, 13);
        }

        private static string MockStatusTransacao()
        {
            var random = new Random();
            var status = random.Next(2);
            return status == 1 ? StatusTransacao.Aprovada : StatusTransacao.Recusado;
        }

        public Transacao MockTransacao(Compra compra)
        {
            Transacao transacao = new()
            {
                Id = GetId<Transacao>("SEQ_EUPAGO_TRANSACAO"),
                CompraId = compra.Id,
                DataTransacao = DateTime.UtcNow,
                IsParcelado = MockIsParcelado(),
                StatusTransacao = MockStatusTransacao(),
                ValorTotal = compra.ValorTotal
            };

            if (transacao.IsParcelado)
            {
                transacao.NumeroParcelas = MockNumeroParcelas();
                transacao.ValorParcelas = transacao.ValorTotal / transacao.NumeroParcelas;
            }
            else
            {
                transacao.ValorParcelas = transacao.ValorTotal;
            }

            return transacao;
        }

        public async Task Add(Transacao transacao)
        {
            try
            {
                await _dataContext.AddAsync(transacao);
                
                // Transacao e compra são salvos juntos
                /*
                 SaveChangesAsync é chamado em CompraService
                 */
                //await _dataContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Transacao>> GetAll(decimal compraId)
        {
            try
            {
                var todasTransacoes =  await _dataContext.Transacoes
                    .Where(t => t.CompraId == compraId)
                    .ToListAsync();
                return todasTransacoes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Transacao> GetUltimaTransacao(decimal compraId)
        {
            try
            {
                var transacoes = await GetAll(compraId);
                var ultimaTransacao = transacoes
                    .MaxBy(t => t.Id);
                return ultimaTransacao;
                //return transacoes[0];
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
