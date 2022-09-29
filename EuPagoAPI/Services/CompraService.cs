using EuPagoAPI.Data;
using EuPagoAPI.DTOs;
using EuPagoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EuPagoAPI.Services
{
    public class CompraService : ServiceBase
    {
        private readonly TransacaoService _transacaoService;
        public CompraService(DataContext dataContext, TransacaoService transacaoService) : base(dataContext)
        {
            _transacaoService = transacaoService;
        }

        public async Task<List<Compra>> GetAll(decimal usuarioId)
        {
            return await _dataContext.Compras
                .Include(c => c.Estabelecimento)
                .Include(c => c.Usuario)
                .Include(c => c.Cartao)
                .Where(c => c.UsuarioId == usuarioId)
                .ToListAsync();
        }

        public async Task<Transacao> Add(CompraDTO model)
        {
            try
            {
                Compra compra = new()
                {
                    Id = GetId<Compra>("SEQ_EUPAGO_COMPRA"),
                    //Mock EstabelecimentoId
                    EstabelecimentoId = 1,
                    UsuarioId = model.UsuarioId,
                    CartaoId = model.CartaoId,
                    ValorTotal = model.ValorTotal
                };
                
                var transacao = _transacaoService.MockTransacao(compra);

                await _dataContext.AddAsync(compra);
                await _transacaoService.Add(transacao);

                await _dataContext.SaveChangesAsync();

                return transacao;

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
