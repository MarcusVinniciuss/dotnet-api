using EuPagoAPI.DTOs;
using EuPagoAPI.Models;
using EuPagoAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EuPagoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstabelecimentosController : ControllerBase
    {
        private readonly EstabelecimentoService _service;

        public EstabelecimentosController(EstabelecimentoService service)
        {
            _service = service;
        }

        // GET: api/<EstabelecimentosController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            throw new NotImplementedException(); 
        }

        // GET api/<EstabelecimentosController>/5
        [HttpGet("{cnpj:long}")]
        public async Task<ActionResult<dynamic>> Get(long cnpj)
        {
            var estabelecimento = await _service.Get(cnpj);

            if (estabelecimento == null) return NotFound("Estabelecimento não encontrado.");

            return Ok(estabelecimento);
        }

        // POST api/<EstabelecimentosController>
        [HttpPost]
        public async Task<ActionResult<dynamic>> Post([FromBody] Estabelecimento model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var e = await _service.Get(model.CNPJ);

                if (e != null) return Conflict("Este estabelecimento já existe");

                return Created(new Uri($@"api/Estabelecimentos/{model.CNPJ}"), UriKind.Relative);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // PUT api/<EstabelecimentosController>/5
        [HttpPut("{cnpj:long}")]
        public async Task<ActionResult<dynamic>> Put(long cnpj,
            [FromBody] EstabelecimentoDTO model)
        {
            try
            {
                var estabelecimentoUpdate = await _service.Get(cnpj);

                if (estabelecimentoUpdate == null) return NotFound("Estabelecimento não encontrado.");

                await _service.Update(estabelecimentoUpdate, model);

                return Ok(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // DELETE api/<EstabelecimentosController>/5
        [HttpDelete("{cnpj:long}")]
        public async Task<ActionResult<dynamic>> Delete(long cnpj)
        {
            try
            {
                var estabelecimentoDelete = await _service.Get(cnpj);

                if (estabelecimentoDelete == null) return NotFound("Estabelecimento não encontrado.");

                await _service.SoftDelete(estabelecimentoDelete);

                return Ok(estabelecimentoDelete);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
