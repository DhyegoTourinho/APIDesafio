namespace APIDesafio.Models
{
    public sealed class ListLogin
    {
        public static ListLogin instance = new ListLogin();
        public List<Login> login = new List<Login>(){
            new Login ("user1", "password123", false),
            new Login ("user2", "password123", false),
            new Login ("user3", "password123", false)
        };
        
        private ListLogin() { }
        public static ListLogin GetInstance()
        {
            if (instance == null) {
                instance = new ListLogin();
            }
            return instance;
        }
        public void Adicionar(Login novoLogin)
        {
            this.login.Add(novoLogin);
        }
    }
}
