using APIDesafio.Models;
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
        public IActionResult Post([FromBody] Usuario usuario)
        {
            try
            {
                _usuarioService.Adicionar(usuario);
                return Ok("Usuario cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao cadastrar usuario!");
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
                return BadRequest("Não foi possivel retornar os usuarios");
            }
        }

        /*[HttpPut("Atualizar")]
        //Metodo que Atualiza um usuário existente:
        public IActionResult Atualizar([FromBody] Usuario usuario, Usuario usuarioAtualizado)
        {
            try
            {
                _usuarioService.Atualizar(usuario, usuarioAtualizado);
                return Ok("Usuário atualizado com sucesso!");
            }
            catch (Exception ex) 
            {
                return BadRequest("Não foi possível atualizar este usuário.");
            }
        }*/

        [HttpDelete("Remover")]
        //Metodo que remove um usuário existente:
        public IActionResult Remover([FromBody] Usuario usuario)
        {
            try
            {
                _usuarioService.Remover(usuario);
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
