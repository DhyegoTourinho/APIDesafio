using APIDesafio.Models;

namespace APIDesafio.Services
{
    public class UsuarioService
    {
        private readonly List<Usuario> _listaSingletonService = ListaSingletonService<Usuario>.GetInstance();
        public List<Usuario> Usuarios = new List<Usuario>()
        {
            new Usuario {
                UserName = "user1",
                Password = "password123",
                Permissao = false
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
    }
}
