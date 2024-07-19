using APIDesafio.Dados;
using APIDesafio.Models;
using APIDesafio.Models.DTO;
using APIDesafio.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIDesafio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        public UsuarioController()
        {
            _usuarioService = new UsuarioService();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromServices] AppDbContext context, [FromBody] LoginEntrada loginEntrada)
        {
            try
            {
                var tokenJWT = _usuarioService.LoginAsync(context, loginEntrada);
                return Ok("Token: " + tokenJWT.Result);
            }
            catch (BadHttpRequestException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("criar")]
        public IActionResult Post([FromServices] AppDbContext context, [FromBody] UsuarioEntrada usuarioEntrada)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var usuario = new Usuario
                {
                    Password = usuarioEntrada.Password,
                    Permissao = usuarioEntrada.Permissao,
                    UserName = usuarioEntrada.UserName
                };
                _usuarioService.AdicionarAsync(usuario, context);
                return Ok("Usuário adicionado com sucesso!\n");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao cadastrar usuário!" + "\n\n" + ex.Message);
            }
        }

        [HttpGet("obterUsuarios")]
        public IActionResult ObterUsuarios([FromServices] AppDbContext context)
        {
            try
            {
                return Ok(_usuarioService.ObterUsuariosAsync(context).Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possivel retornar os usuários" + "\n\n" + ex.Message);
            }
        }

        [HttpGet("obterUsuarios/{id}")]
        public IActionResult ObterUsuariosPorID([FromServices] AppDbContext context, string id)
        {
            try
            {
                var idInteiro = int.TryParse(id, out int resultado) ? resultado : 0;
                var usuario = _usuarioService.ObterUmUsuarioPorIdAsync(idInteiro, context).Result;
                return usuario == null ? NotFound("Usuário não existe") : Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível retornar os usuários" + "\n\n" + ex.Message);
            }
        }

        [HttpPut("atualizar/{id}")]
        public IActionResult Atualizar([FromServices] AppDbContext context, int id, [FromBody] UsuarioEntrada usuario)
        {
            try
            {
                var statuscode = _usuarioService.AtualizarAsync(context, id, usuario);
                if (statuscode == null)
                    return NotFound();

                return Ok("Usuário atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível atualizar este usuário." + "\n\n" + ex.Message);
            }
        }

        //[Authorize(Roles = "Administrador")]
        [HttpDelete("remover/{id}")]
        public IActionResult Remover([FromServices] AppDbContext context, string id)
        {
            try
            {
                int idInteiro = int.TryParse(id, out int resultado) ? resultado : 0;
                var usuario = _usuarioService.RemoverAsync(context, idInteiro);
                return Ok("Usuario removido\n" + usuario.Result);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest("Usuário não existe." + "\n\n" + ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível remover o usuário" + "\n\n" + ex.Message);
            }

        }

        //[Authorize(Roles = "Administrador")]
        [HttpDelete("removerTodos")]
        public IActionResult RemoverTodos([FromServices] AppDbContext context)
        {
            try
            {
                _usuarioService.RemoverTodosAsync(context);
                return Ok("Todos os usuários foram removidos com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível remover todos os usuários" + ex.Message);
            }
        }
    }
}
