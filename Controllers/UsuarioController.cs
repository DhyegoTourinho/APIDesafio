using APIDesafio.Models;
using APIDesafio.Models.DTO;
using APIDesafio.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIDesafio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        //Instanciação de UsuarioService:
        private readonly UsuarioService _usuarioService;
        public UsuarioController()
        {
            _usuarioService = new UsuarioService();
        }
        
        //Metodo que cria um novo usuário:
        [HttpPost("criar")]
        public IActionResult Post([FromBody] UsuarioEntrada usuarioEntrada)
        {
            try
            {
                var usuario = new Usuario
                {
                    Password = usuarioEntrada.Password,
                    Permissao = usuarioEntrada.Permissao,
                    UserName = usuarioEntrada.UserName
                };

                _usuarioService.Adicionar(usuario);
                return Ok("Usuário cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao cadastrar usuário!");
            }
        }

        //Metodo que Obtem todos os usuários:
        [HttpGet("obterUsuarios")]
        public IActionResult ObterUsuarios()
        {
            try
            {
                return Ok(_usuarioService.ObterUsuarios());
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possivel retornar os usuários");
            }
        }
        
        //Metodo que Obtem todos os usuários:
        [HttpGet("obterUsuarios/{id}")]
        public IActionResult ObterUsuariosPorID(string id)
        {
            try
            {
                var idInteiro = int.TryParse(id, out int resultado) ? resultado : 0;
                return Ok(_usuarioService.ObterUmUsuarioPorId(idInteiro));
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível retornar os usuários");
            }
        }

        //Metodo que Atualiza um usuário existente:
        [HttpPut("atualizar/{id}")]
        public IActionResult Atualizar(int id, [FromBody] UsuarioEntrada usuario)
        {
            try
            {

                _usuarioService.Atualizar(id , usuario);
                return Ok("Usuário atualizado com sucesso!");
            }
            catch (Exception ex) 
            {
                return BadRequest("Não foi possível atualizar este usuário.");
            }
        }

        //Metodo que remove um usuário existente:
        [HttpDelete("remover/{id}")]
        public IActionResult Remover(string id)
        {
            try
            {
                int idInteiro = int.TryParse(id, out int resultado) ? resultado : 0;
                _usuarioService.Remover(idInteiro);
            return Ok("Usuario removido");
            }
            catch(System.ArgumentOutOfRangeException ex)
            {
                return BadRequest("Usuário não existe.");
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível remover o usuário");
            }

        }

    }
}
