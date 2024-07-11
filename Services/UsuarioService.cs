using APIDesafio.Models;
using APIDesafio.Models.DTO;

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

        public Usuario ObterUmUsuarioPorId(int id)
        {
            return _listaSingletonService.Find(x => x.Id == id);
        }
        public void Remover(int id)
        {
            _listaSingletonService.RemoveAt(_listaSingletonService.FindIndex(x => x.Id == id));
        }

        public void Atualizar(int id, UsuarioEntrada usuarioAtualizado)
        {
            var usuarioSalvo = _listaSingletonService.FirstOrDefault(x => x.Id == id);
            
            usuarioSalvo.UserName = usuarioAtualizado.UserName;
            usuarioSalvo.Permissao = usuarioAtualizado.Permissao;
            usuarioSalvo.Password = usuarioAtualizado.Password;
        }

        public string Login(LoginEntrada usuario)
        {
            var usuarioSalvo = _listaSingletonService.FirstOrDefault(x => x.UserName == usuario.UserName && x.Password == usuario.Password);

            if (usuarioSalvo == null) 
            {
                throw new BadHttpRequestException("Usuario ou senha inválidos!", 401);
            }
            return TokenService.Generate(usuarioSalvo);
        }
    }
}
