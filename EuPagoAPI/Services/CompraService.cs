using EuPagoAPI.Data;
using EuPagoAPI.DTOs;
using EuPagoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EuPagoAPI.Services
{
    public class CompraService : ServiceBase
    {
        private readonly TransacaoService _transacaoService;
        private readonly EstabelecimentoService _estabelecimentoService;
        public CompraService(DataContext dataContext,
            TransacaoService transacaoService,
            EstabelecimentoService estabelecimentoService) : base(dataContext)
        {
            _transacaoService = transacaoService;
            _estabelecimentoService = estabelecimentoService;
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
                var estabelecimento = await _estabelecimentoService.Get(model.CnpjEstabelecimento);

                if (estabelecimento == null)
                {
                    var newEstabelecimento = new EstabelecimentoDTO()
                    {
                        Nome = model.NomeEstabelecimento,
                        CNPJ = model.CnpjEstabelecimento
                    };
                    await _estabelecimentoService.Add(newEstabelecimento);
                    estabelecimento = await _estabelecimentoService.Get(model.CnpjEstabelecimento);
                }

                Compra compra = new()
                {
                    Id = GetId<Compra>("SEQ_EUPAGO_COMPRA"),
                    EstabelecimentoId = estabelecimento.Id,
                    UsuarioId = model.UsuarioId,
                    CartaoId = model.CartaoId,
                    ValorTotal = model.ValorTotal
                };

                Transacao transacao = new()
                {
                    Id = GetId<Transacao>("SEQ_EUPAGO_TRANSACAO"),
                    CompraId = compra.Id,
                    DataTransacao = model.DataCompra,
                    IsParcelado = model.NumeroParcelas > 1,
                    NumeroParcelas = model.NumeroParcelas,
                    ValorParcelas = model.ValorParcelas,
                    StatusTransacao = model.StatusTransacao,
                    ValorTotal = model.ValorTotalCartao
                };
                
                //var transacao = _transacaoService.MockTransacao(compra);

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
