using EuPagoAPI.DTOs;
using EuPagoAPI.Services;
using EuPagoAPI.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EuPagoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : ControllerBase
    {
        private readonly CompraService _service;
        private readonly TransacaoService _transacaoService;

        public ComprasController(CompraService service, TransacaoService transacaoService)
        {
            _service = service;
            _transacaoService = transacaoService;
        }

        [HttpGet]
        [Route("{usuarioId:decimal}")]
        public async Task<ActionResult<dynamic>> Get(decimal usuarioId)
        {
            try
            {
                var historicoCompras = await _service.GetAll(usuarioId);
                List<ResumoCompraDTO> listaResumoComprasDTO = new();

                if (historicoCompras.Count == 0) return NotFound("Não há compras realizadas por esse usuário.");

                foreach (var compra in historicoCompras)
                {
                    var ultimaTransacaoCompra = await _transacaoService.GetUltimaTransacao(compra.Id);
                    ResumoCompraDTO resumoCompra = new()
                    {
                        NomeEstabelecimento = compra.Estabelecimento.Nome,
                        CNPJEstabelecimento = compra.Estabelecimento.CNPJ,
                        NumeroCartao = compra.Cartao.Numero,
                        CVVCartao = compra.Cartao.CVV,
                        ValorCompra = compra.ValorTotal,
                        DataCompra = ultimaTransacaoCompra.DataTransacao,
                        NumeroParcelas = ultimaTransacaoCompra.NumeroParcelas,
                        ValorParcelas = ultimaTransacaoCompra.ValorParcelas,
                        StatusTransacao = ultimaTransacaoCompra.StatusTransacao,
                        ValorTotalCartao = ultimaTransacaoCompra.ValorTotal,
                        TransacaoId = ultimaTransacaoCompra.Id
                    };
                    listaResumoComprasDTO.Add(resumoCompra);
                }

                HistoricoComprasDTO historicoComprasDTO = new()
                {
                    UsuarioId = historicoCompras[0].UsuarioId,
                    historicoCompras = listaResumoComprasDTO
                };

                return historicoComprasDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<dynamic>> Post([FromBody] CompraDTO model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var transacao = await _service.Add(model);

                if (transacao.StatusTransacao.Equals(StatusTransacao.Aprovado))
                {
                    return Ok("Transação aprovada! Compra realizada com sucesso.");
                }
                else
                {
                    return BadRequest("Pagamento recusado! Por gentileza, tente mais tarde.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
