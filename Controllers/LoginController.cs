using APIDesafio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIDesafio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        ListLogin listLogin = ListLogin.GetInstance();

        //FUNCIONANDOOOOOOOOOOO!!!!!!!!!!!!!!!
        // GET: api/<LoginController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            if (listLogin.login == null){
                return null; 
            }
                string[] logins = new string[listLogin.login.Count];
                
                for(int i = 0; i < listLogin.login.Count; i++)
            {
                logins[i] = listLogin.login[i].ToString();
            }
                return logins;
        }

        //FUNCIONNADOOOOOO!
        // GET api/<LoginController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            Login usuario = listLogin.login.Find(x => x.Id == id);
            
            if (usuario == null)
            {
                return "Empty";
            }
            return usuario.ToString();
        }

        // POST api/<LoginController>
        [HttpPost]
        public void Post([FromBody] int id, string name, string password, Boolean permissao)
        {
            listLogin.Adicionar(new Login(name, password, permissao));
        }

        // PUT api/<LoginController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LoginController>/5
        [HttpDelete]
        public void Delete(string Name)
        {
            Login usuarioRemove = listLogin.login.Find(x => x.Name == Name);
            if (usuarioRemove != null) {
                listLogin.login.Remove(usuarioRemove);
            }
        }
    }
}
