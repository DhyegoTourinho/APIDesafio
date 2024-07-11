using APIDesafio.Models;
using APIDesafio.Models.DTO;
using APIDesafio.Services;
using Microsoft.AspNetCore.Authorization;
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
                return BadRequest("Erro ao cadastrar usuário!" + "\n\n" + ex.Message);
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
                return BadRequest("Não foi possivel retornar os usuários" + "\n\n" + ex.Message);
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
                return BadRequest("Não foi possível retornar os usuários" + "\n\n" + ex.Message);
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
                return BadRequest("Não foi possível atualizar este usuário." + "\n\n" + ex.Message);
            }
        }

        //Metodo que remove um usuário existente:
        [Authorize]
        [HttpDelete("remover/{id}")]
        public IActionResult Remover(string id)
        {
            try
            {
                int idInteiro = int.TryParse(id, out int resultado) ? resultado : 0;
                _usuarioService.Remover(idInteiro);
            return Ok("Usuario removido");
            }
            catch(ArgumentOutOfRangeException ex)
            {
                return BadRequest("Usuário não existe." + "\n\n" + ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível remover o usuário" + "\n\n" + ex.Message);
            }

        }

        //Metodo que remove um usuário existente:
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginEntrada loginEntrada)
        {
            try
            {
                var tokenJWT = _usuarioService.Login(loginEntrada);
                return Ok(tokenJWT);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest("Usuário não existe." + "\n\n" + ex.Message);
            }
            catch (BadHttpRequestException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível remover o usuário" + "\n\n" + ex.Message);
            }

        }
    }
}
