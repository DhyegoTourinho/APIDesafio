using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Xml.Linq;

namespace APIDesafio.Models
{
    public class Login
    {
        public static int IdCont = 0;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Permissao { get; set; }
        public Login(string name, string password, Boolean permissao)
        {
            this.Id = IdCont;
            this.Name = name;
            this.Password = password;
            if (permissao == true) {
                this.Permissao = "Administrador";
            } else
            {
                this.Permissao = "Usuario";
            }
            IdCont++;
        }

        public override string ToString()
        {
            return "ID: " + Id +
                   " Nome: " + Name +
                   " Senha: " + Password +
                   " Permissão: " + Permissao;
        }
    }
}
