namespace APIDesafio.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Permissao { get; set; }
        public string Cargo => !Permissao ? "Usuario" : "Administrador";
        
        public override string ToString()
        {
            return " Id: " + Id +
                   "\n Nome: " + UserName +
                   "\n Senha: " + Password +
                   "\n Permissão: " + Permissao +
                   "\n Cargo: " + Cargo;
        }
    }
}
