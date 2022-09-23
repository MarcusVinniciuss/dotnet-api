using EuPagoAPI.Data;
using EuPagoAPI.DTOs;
using EuPagoAPI.Models;
using EuPagoAPI.Utils;

namespace EuPagoAPI.Services
{
    public class UsuarioService : ServiceBase
    {
        public UsuarioService(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<Usuario?> Authenticate(long cpf, string senha)
        {
            //Adicionar verificação de senha hasheada
            var u = await _dataContext.Usuarios
                .Include(u => u.Telefone)
                .Include(u => u.Endereco)
                .FirstOrDefaultAsync(
                    p => p.CPF == cpf && p.Senha == senha
                );
            return u;
        }

        public async Task<Usuario?> Get(long cpf)
        {
            var u = await _dataContext.Usuarios
                .Include(u => u.Telefone)
                .Include(u => u.Endereco)
                .FirstOrDefaultAsync(p => p.CPF == cpf);
            return u;
        }

        public async Task<Usuario?> Get(decimal id)
        {
            var u = await _dataContext.Usuarios
                .Include(u => u.Telefone)
                .Include(u => u.Endereco)
                .FirstOrDefaultAsync(p => p.Id == id);
            return u;
        }

        public async Task Add(UsuarioDTO model)
        {
            try
            {
                Usuario usuario = new()
                {
                    Id = GetId<Usuario>("SEQ_EUPAGO_USUARIO"),
                    Nome = model.Nome,
                    Senha = model.Senha,
                    CPF = model.CPF,
                    DataNascimento = model.DataNascimento,
                    StatusVisao = model.StatusVisao,
                    StatusCadastro = StatusCadastro.Ativo,
                    Email = model.Email,
                    DataCadastro = DateTime.UtcNow
                };

                Endereco endereco = new()
                {
                    Id = GetId<Endereco>("SEQ_EUPAGO_ENDERECO"),
                    UsuarioId = usuario.Id,
                    Cidade = model.Endereco.Cidade,
                    Rua = model.Endereco.Rua,
                    Bairro = model.Endereco.Bairro,
                    Numero = model.Endereco.Numero,
                    Complemento = model.Endereco.Complemento,
                    CEP = model.Endereco.CEP
                };

                Telefone telefone = new()
                {
                    Id = GetId<Telefone>("SEQ_EUPAGO_TELEFONE"),
                    UsuarioId = usuario.Id,
                    Numero = model.Telefone.Numero,
                    DDD = model.Telefone.DDD
                };

                _dataContext.Usuarios.Add(usuario);
                _dataContext.Enderecos.Add(endereco);
                _dataContext.Telefones.Add(telefone);

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

        public async Task Update(Usuario usuarioUpdate, UsuarioDTO model)
        {
            usuarioUpdate.Nome = model.Nome;
            usuarioUpdate.CPF = model.CPF;
            usuarioUpdate.Senha = model.Senha;
            usuarioUpdate.DataNascimento = model.DataNascimento;
            usuarioUpdate.StatusVisao = model.StatusVisao;
            usuarioUpdate.DataAtualizacao = DateTime.UtcNow;

            usuarioUpdate.Telefone.DDD = model.Telefone.DDD;
            usuarioUpdate.Telefone.Numero = model.Telefone.Numero;
            
            usuarioUpdate.Endereco.Rua = model.Endereco.Rua;
            usuarioUpdate.Endereco.CEP = model.Endereco.CEP;
            usuarioUpdate.Endereco.Bairro = model.Endereco.Bairro;
            usuarioUpdate.Endereco.Numero = model.Endereco.Numero;
            usuarioUpdate.Endereco.Cidade = model.Endereco.Cidade;
            usuarioUpdate.Endereco.Complemento = model.Endereco.Complemento;

            _dataContext.Usuarios.Update(usuarioUpdate);
            _dataContext.Enderecos.Update(usuarioUpdate.Endereco);
            _dataContext.Telefones.Update(usuarioUpdate.Telefone);

            await _dataContext.SaveChangesAsync();
        }

        public async Task SoftDelete(Usuario usuario)
        {

            usuario.StatusCadastro = StatusCadastro.Desativado;
            _dataContext.Usuarios.Update(usuario);

            await _dataContext.SaveChangesAsync();

        }

    }
}
