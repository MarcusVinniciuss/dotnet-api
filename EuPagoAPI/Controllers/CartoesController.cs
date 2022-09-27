using EuPagoAPI.DTOs;
using EuPagoAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EuPagoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartoesController : ControllerBase
    {
        private readonly CartaoService _service;

        public CartoesController(CartaoService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<dynamic>> Post([FromBody] CartaoDTO model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var c = await _service.Get(model.UsuarioId, model.Numero);

                if (c != null) return Conflict("Este cartão já está cadastrado.");

                await _service.Add(model);

                return Created(new Uri($@"api/Cartoes/{model.UsuarioId}/{model.Numero}", UriKind.Relative), model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("{usuarioId:decimal}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<dynamic>> Get(decimal usuarioId)
        {
            string headerToken = Request.Headers["Authorization"];
            var jwtToken = new JwtSecurityToken(headerToken.Replace("Bearer ", ""));
            var primarySidCPFClaim = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.PrimarySid).Value;

            var carteiraUsuario = await _service.GetAll(usuarioId);

            var usuario = carteiraUsuario[0].Usuario;

            if (usuario == null) return NotFound("Usuário não encontrado.");

            if (primarySidCPFClaim.Equals(Convert.ToString(usuario.CPF)))
            {
                return Ok(carteiraUsuario);
            }

            return Forbid(@"{""message"": ""Token inválido para esse usuário ou está expirado.""}");
        }

        [HttpPut]
        [Route("{usuarioId:decimal}/{numeroCartao:long}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<dynamic>> Put(
            [FromBody] CartaoDTO model,
            decimal usuarioId,
            long numeroCartao)
        {
            try
            {
                string headerToken = Request.Headers["Authorization"];
                var jwtToken = new JwtSecurityToken(headerToken.Replace("Bearer ", ""));
                var primarySidCPFClaim = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.PrimarySid).Value;

                var cartaoUpdate = await _service.Get(usuarioId, numeroCartao);

                if (cartaoUpdate == null) return NotFound("Cartão não encontrado.");

                var usuario = cartaoUpdate.Usuario;

                if (primarySidCPFClaim.Equals(Convert.ToString(usuario.CPF)))
                {
                    await _service.Update(cartaoUpdate, model);

                    return Ok(model);
                }

                return Forbid(@"{""message"": ""Token inválido para esse usuário ou está expirado.""}");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("{usuarioId:decimal}/{numeroCartao:long}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<dynamic>> Delete(decimal usuarioId, long numeroCartao)
        {
            try
            {
                string headerToken = Request.Headers["Authorization"];
                var jwtToken = new JwtSecurityToken(headerToken.Replace("Bearer ", ""));
                var primarySidCPFClaim = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.PrimarySid).Value;

                var cartaoDelete = await _service.Get(usuarioId, numeroCartao);

                if (cartaoDelete == null) return NotFound("Cartão não encontrado.");

                var usuario = cartaoDelete.Usuario;

                if (primarySidCPFClaim.Equals(Convert.ToString(usuario.CPF)))
                {
                    await _service.SoftDelete(cartaoDelete);

                    return Ok(cartaoDelete);
                }

                return Forbid(@"{""message"": ""Token inválido para esse usuário ou está expirado.""}");
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
