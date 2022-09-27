using EuPagoAPI.DTOs;
using EuPagoAPI.Models;
using EuPagoAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EuPagoAPI.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UsuarioService _service;
        public UsuariosController(IConfiguration configuration, UsuarioService service)
        {
            _config = configuration;
            _service = service;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<UsuarioLoginDTO>> Login([FromBody] UsuarioLogin usuarioLogin)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!string.IsNullOrEmpty(usuarioLogin.Senha))
            {
                var usuarioLogado = await _service.Authenticate(usuarioLogin.CPF, usuarioLogin.Senha);
                if (usuarioLogado is null) return NotFound("Usuário não encontrado.");

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.PrimarySid, Convert.ToString(usuarioLogado.CPF))
                };

                foreach (var audience in _config.GetSection("Jwt:Audiences").Get<string[]>())
                {
                    claims.Add(new Claim(JwtRegisteredClaimNames.Aud, audience));
                }

                var token = new JwtSecurityToken
                (
                    issuer: _config.GetSection("Jwt:Issuer").Get<string>(),
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    notBefore: DateTime.UtcNow,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Get<string>())),
                        SecurityAlgorithms.HmacSha256
                        )
                );

                EnderecoDTO endereco = new()
                {
                    Cidade = usuarioLogado.Endereco.Cidade,
                    Rua = usuarioLogado.Endereco.Rua,
                    Bairro = usuarioLogado.Endereco.Bairro,
                    Complemento = usuarioLogado.Endereco.Complemento,
                    CEP = usuarioLogado.Endereco.CEP,
                    Numero = usuarioLogado.Endereco.Numero
                };

                TelefoneDTO telefone = new()
                {
                    DDD = usuarioLogado.Telefone.DDD,
                    Numero = usuarioLogado.Telefone.Numero
                };

                UsuarioDTO usuario = new()
                {
                    Nome = usuarioLogado.Nome,
                    CPF = usuarioLogado.CPF,
                    Email = usuarioLogado.Email,
                    StatusVisao = usuarioLogado.StatusVisao,
                    Senha = usuarioLogado.Senha,
                    DataNascimento = usuarioLogado.DataNascimento,
                    StatusCadastro = usuarioLogado.StatusCadastro,
                    DataCadastro = usuarioLogado.DataCadastro,
                    DataAtualizacao = usuarioLogado.DataAtualizacao,
                    Telefone = telefone,
                    Endereco = endereco
                };

                BearerTokenDTO tokenString = new()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                };

                UsuarioLoginDTO usuarioLoginDTO = new()
                {
                    usuario = usuario,
                    bearer = tokenString
                };

                return Ok(usuarioLoginDTO);
            }
            return BadRequest("Necessário enviar o request com o campo senha.");
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<UsuarioCreateDTO>> Post([FromBody] UsuarioCreateDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var u = await _service.Get(model.usuario.CPF);

                if (u != null)
                {
                    return Conflict("Um usuário com esse CPF já está cadastrado.");
                }

                await _service.Add(model.usuario);
                
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("dados")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UsuarioDTO>> ObterDados([FromBody] UsuarioLogin usuarioLogin)
        {
            try
            {
                string headerToken = Request.Headers["Authorization"];
                var jwtToken = new JwtSecurityToken(headerToken.Replace("Bearer ", ""));
                var primarySidCPFClaim = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.PrimarySid).Value;

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!string.IsNullOrEmpty(usuarioLogin.Senha))
                {
                    var usuario = await _service.Authenticate(usuarioLogin.CPF, usuarioLogin.Senha);

                    if (usuario == null) return NotFound("Usuário não encontrado.");

                    if (primarySidCPFClaim.Equals(Convert.ToString(usuario.CPF)))
                    {
                        return Ok(usuario);
                    }
                    return Unauthorized("Token inválido para esse usuário ou está expirado.");
                }
                return BadRequest("É necessário incluir a senha do usuário.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UsuarioLoginDTO>> Put([FromBody] UsuarioUpdateDTO model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                string headerToken = Request.Headers["Authorization"];
                var jwtToken = new JwtSecurityToken(headerToken.Replace("Bearer ", ""));
                var primarySidCPFClaim = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.PrimarySid).Value;

                var usuarioUpdate = await _service.Get(model.usuario.CPF);

                if (usuarioUpdate == null) return NotFound("Usuário não encontrado.");
                if (!primarySidCPFClaim.Equals(Convert.ToString(usuarioUpdate.CPF)))
                {
                    return Unauthorized(@"{""message"": ""Token inválido para esse usuário ou está expirado.""}");
                }

                await _service.Update(usuarioUpdate, model.usuario);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.PrimarySid, Convert.ToString(model.usuario.CPF))
                };

                foreach (var audience in _config.GetSection("Jwt:Audiences").Get<string[]>())
                {
                    claims.Add(new Claim(JwtRegisteredClaimNames.Aud, audience));
                }

                var token = new JwtSecurityToken
                (
                    issuer: _config.GetSection("Jwt:Issuer").Get<string>(),
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    notBefore: DateTime.UtcNow,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Get<string>())),
                        SecurityAlgorithms.HmacSha256
                        )
                );

                var updatedUsuario = model.usuario;

                EnderecoDTO endereco = new()
                {
                    Cidade = updatedUsuario.Endereco.Cidade,
                    Rua = updatedUsuario.Endereco.Rua,
                    Bairro = updatedUsuario.Endereco.Bairro,
                    Complemento = updatedUsuario.Endereco.Complemento,
                    CEP = updatedUsuario.Endereco.CEP,
                    Numero = updatedUsuario.Endereco.Numero
                };

                TelefoneDTO telefone = new()
                {
                    DDD = updatedUsuario.Telefone.DDD,
                    Numero = updatedUsuario.Telefone.Numero
                };

                UsuarioDTO usuario = new()
                {
                    Nome = updatedUsuario.Nome,
                    CPF = updatedUsuario.CPF,
                    Email = updatedUsuario.Email,
                    StatusVisao = updatedUsuario.StatusVisao,
                    Senha = updatedUsuario.Senha,
                    DataNascimento = updatedUsuario.DataNascimento,
                    StatusCadastro = updatedUsuario.StatusCadastro,
                    DataCadastro = updatedUsuario.DataCadastro,
                    DataAtualizacao = updatedUsuario.DataAtualizacao,
                    Telefone = telefone,
                    Endereco = endereco
                };

                BearerTokenDTO tokenString = new()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                };

                UsuarioLoginDTO usuarioLoginDTO = new()
                {
                    usuario = usuario,
                    bearer = tokenString
                };

                return Ok(usuarioLoginDTO);

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("{cpf:long}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Usuario>> Delete(long cpf)
        {
            string headerToken = Request.Headers["Authorization"];
            var jwtToken = new JwtSecurityToken(headerToken.Replace("Bearer ", ""));
            var primarySidCPFClaim = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.PrimarySid).Value;
            
            var usuarioDesativado = await _service.Get(cpf);
            if (usuarioDesativado == null) return NotFound("Usuário não encontrado");

            if (primarySidCPFClaim.Equals(Convert.ToString(usuarioDesativado.CPF)))
            {
                await _service.SoftDelete(usuarioDesativado);
                return Ok("Usuário deletado.");
            }
            return Unauthorized("Token inválido para esse usuário ou está expirado.");
        }

    }
}
