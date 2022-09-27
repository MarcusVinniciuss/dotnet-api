using EuPagoAPI.Data;
using EuPagoAPI.DTOs;
using EuPagoAPI.Models;
using EuPagoAPI.Utils;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

namespace EuPagoAPI.Services
{
    public class EstabelecimentoService : ServiceBase
    {
        public EstabelecimentoService(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<Estabelecimento?> Get(long cnpj)
        {
            var e = await _dataContext.Estabelecimentos
                .FirstOrDefaultAsync(e => e.CNPJ == cnpj);

            return e;
        } 

        public async Task Add(EstabelecimentoDTO model)
        {
            try
            {
                Estabelecimento estabelecimento = new()
                {
                    Id = GetId<Estabelecimento>("SEQ_EUPAGO_ESTABELECIMENTO"),
                    Nome = model.Nome,
                    CNPJ = model.CNPJ,
                    StatusCadastro = StatusCadastro.Ativo
                };

                _dataContext.Estabelecimentos.Add(estabelecimento);

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

        public async Task Update(Estabelecimento estabelecimentoUpdate, EstabelecimentoDTO model)
        {
            estabelecimentoUpdate.Nome = model.Nome;
            estabelecimentoUpdate.CNPJ = model.CNPJ;

            _dataContext.Update(estabelecimentoUpdate);

            await _dataContext.SaveChangesAsync();
        }

        public async Task SoftDelete(Estabelecimento estabelecimento)
        {
            estabelecimento.StatusCadastro = StatusCadastro.Desativado;
            _dataContext.Estabelecimentos.Update(estabelecimento);

            await _dataContext.SaveChangesAsync();
        }

    }
}
