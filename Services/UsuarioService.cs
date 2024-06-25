using APIDesafio.Models;

namespace APIDesafio.Services
{
    public class UsuarioService
    {
        private readonly List<Usuario> _listaSingletonService = ListaSingletonService<Usuario>.GetInstance();
        public List<Usuario> Usuarios = new List<Usuario>()
        {
            new Usuario {
                Password = "password",
                Permissao = false,
                UserName = "username"
            },
            new Usuario {
                UserName = "user2",
                Password = "password123",
                Permissao = false
            },
            new Usuario
            {
                UserName = "user3",
                Password = "password123",
                Permissao = false
            }
        };

        public UsuarioService() 
        {
            if (_listaSingletonService != null && _listaSingletonService.Count == 0)
            {
                _listaSingletonService.AddRange(Usuarios);
            }
        }

        public void Adicionar(Usuario usuario)
        {
            _listaSingletonService.Add(usuario);
        }

        public List<Usuario> ObterUsuarios()
        {
            return _listaSingletonService;
        }

        public Usuario ObterUmUsuario(Usuario usuario)
        {
            return _listaSingletonService.Find(x => x == usuario);
        }
        public void Remover(Usuario usuario)
        {
            _listaSingletonService.RemoveAt(_listaSingletonService.FindIndex(x => x == usuario));
        }

        public void Atualizar(int id, Usuario usuarioAtualizado)
        {
            _listaSingletonService[_listaSingletonService.FindIndex(x => x.getId() == id)] = usuarioAtualizado;
        }
    }
}
