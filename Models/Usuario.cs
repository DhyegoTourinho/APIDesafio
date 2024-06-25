namespace APIDesafio.Models
{
    public class Usuario
    {
        private static int _idCont = 0;
        private int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Permissao { get; set; }

        public Usuario() 
        {
            this.Id = _idCont;
            _idCont++;
        }
        public Usuario(string userName, string password, bool permissao) 
        {
            this.Id = _idCont;
            this.UserName = userName;
            this.Password = password;
            this.Permissao = permissao;
            _idCont++;
        }

        public int getId()
        {
            return Id;
        }

        public override string ToString()
        {
            return " Id: " + getId() +
                   "\n Nome: " + UserName +
                   "\n Senha: " + Password +
                   "\n Permissão: " + Permissao;
        }
    }
}
