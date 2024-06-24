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
        private readonly UsuarioService _usuarioService;
        public UsuarioController()
        {
            _usuarioService = new UsuarioService();
        }
        // GET: api/<LoginController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    if (listLogin.login == null){
        //        return null; 
        //    }
        //        string[] logins = new string[listLogin.login.Count];
                
        //        for(int i = 0; i < listLogin.login.Count; i++)
        //    {
        //        logins[i] = listLogin.login[i].ToString();
        //    }
        //        return logins;
        //}

        // GET api/<LoginController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    Login usuario = listLogin.login.Find(x => x.Id == id);
            
        //    if (usuario == null)
        //    {
        //        return "Empty";
        //    }
        //    return usuario.ToString();
        //}

        // POST api/<LoginController>
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

        [HttpGet("ObterUsuarios")]
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

        // PUT api/<LoginController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LoginController>/5
        //[HttpDelete("deletar")]
        //public void Delete(string userName)
        //{
        //    Login usuarioRemove = listLogin.login.Find(x => x.UserName == userName);
        //    if (usuarioRemove != null) {
        //        listLogin.login.Remove(usuarioRemove);
        //    }
        //}
    }
}
