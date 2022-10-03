using EuPagoAPI.Data;
using EuPagoAPI.DTOs;
using EuPagoAPI.Models;
using EuPagoAPI.Services;
using EuPagoAPI.Utils;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

namespace EuPagoAPI
{
    public class CartaoService : ServiceBase
    {
        public CartaoService(DataContext dataContext) : base(dataContext)
        {
        }

        private DateTime DataValidade(int ano, int mes)
        {
            var dataValidade = new DateTime(ano,
                    mes,
                    DateTime.DaysInMonth(
                        ano,
                        mes)
                    );
            return dataValidade;
        }

        public async Task<List<Cartao>> GetAll(decimal usuarioId)
        {
            var carteira = await _dataContext.Cartoes
                .Include(c => c.Usuario)
                .Where(c => c.UsuarioId == usuarioId)
                .ToListAsync();
            return carteira;
        }

        public async Task<Cartao> Get(decimal usuarioId, long numeroCartao)
        {
            var carteira = await GetAll(usuarioId);
            var cartao = carteira
                .Where(c => c.Numero == numeroCartao)
                .FirstOrDefault();
            return cartao;
        }

        public async Task Add(CartaoDTO model)
        {
            try
            {
                var dataValidade = DataValidade(
                    model.AnoValidade,
                    model.MesValidade);
                Cartao cartao = new()
                {
                    Id = GetId<Cartao>("SEQ_EUPAGO_CARTAO"),
                    Numero = model.Numero,
                    NomeImpresso = model.NomeImpresso,
                    DataValidade = dataValidade,
                    CVV = model.CVV,
                    Emissora = model.Emissora,
                    StatusCadastro = StatusCadastro.Ativo,
                    UsuarioId = model.UsuarioId
                };

                await _dataContext.AddAsync(cartao);

                await _dataContext.SaveChangesAsync();
            }
            catch (OracleException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(Cartao cartaoUpdate, CartaoDTO model)
        {

            cartaoUpdate.Numero = model.Numero;
            cartaoUpdate.DataValidade = DataValidade(
                model.AnoValidade,
                model.MesValidade);
            cartaoUpdate.CVV = model.CVV;
            cartaoUpdate.Emissora = model.Emissora;
            cartaoUpdate.UsuarioId = model.UsuarioId;

            _dataContext.Cartoes.Update(cartaoUpdate);

            await _dataContext.SaveChangesAsync();
        }

        public async Task SoftDelete(Cartao cartao)
        {
            cartao.StatusCadastro = StatusCadastro.Desativado;
            _dataContext.Cartoes.Update(cartao);

            await _dataContext.SaveChangesAsync();
        }

    }
}