namespace APIDesafio.Models.DTO
{
    public class UsuarioRetorno
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Permissao { get; set; }
    }
}
