using APIDesafio.Dados;
using APIDesafio.Models;
using APIDesafio.Models.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace APIDesafio.Services
{
    public class UsuarioService
    {
        private readonly List<Usuario> _listaSingletonService = ListaSingletonService<Usuario>.GetInstance();
        public List<Usuario> Usuarios = new List<Usuario>();
        public UsuarioService()
        {
            if (_listaSingletonService != null && _listaSingletonService.Count == 0)
            {
                _listaSingletonService.AddRange(Usuarios);
            }
        }

        public async Task<string> LoginAsync(AppDbContext context, LoginEntrada usuario)
        {
            var usuarioSalvo = await context.Usuarios.FirstOrDefaultAsync(x => x.UserName.ToLower() == usuario.UserName.ToLower() && x.Password == usuario.Password);

            if (usuarioSalvo == null)
            {
                throw new BadHttpRequestException("Usuario ou senha inválidos!", 401);
            }
            return TokenService.Generate(usuarioSalvo);
        }

        public async Task AdicionarAsync(Usuario usuario, AppDbContext context)
        {
            await context.Usuarios.AddAsync(usuario);
            await context.SaveChangesAsync();
        }

        public async Task<List<UsuarioRetorno>> ObterUsuariosAsync(AppDbContext context)
        {

            var usuarios = await context.Usuarios.AsNoTracking().Select(colunasUsuario => new UsuarioRetorno
            {
                Id = colunasUsuario.Id,
                UserName = colunasUsuario.UserName,
                Password = colunasUsuario.Password,
                Cargo = colunasUsuario.Cargo
            }).ToListAsync();
            return usuarios;
        }

        public async Task<Usuario> ObterUmUsuarioPorIdAsync(int id, AppDbContext context)
        {
            var usuario = await context.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return usuario;
        }

        public async Task<string> AtualizarAsync(AppDbContext context, int id, UsuarioEntrada usuarioAtualizado)
        {
            var usuarioSalvo = await context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
            if (usuarioSalvo == null)
                return null;

            usuarioSalvo.UserName = usuarioAtualizado.UserName;
            usuarioSalvo.Permissao = usuarioAtualizado.Permissao;
            usuarioSalvo.Password = usuarioAtualizado.Password;

            context.SaveChanges();
            return "Ok";
        }

        public async Task<Usuario> RemoverAsync(AppDbContext context, int id)
        {
            var usuarioRemovido = await context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
            context.Usuarios.Remove(usuarioRemovido);
            await context.SaveChangesAsync();
            return usuarioRemovido;
        }

        public async Task RemoverTodosAsync(AppDbContext context)
        {
            await context.Database.EnsureDeletedAsync();
            context.Database.Migrate();
        }
    }
}
