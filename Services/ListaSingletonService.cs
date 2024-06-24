using APIDesafio.Models;

namespace APIDesafio.Services
{
    public sealed class ListaSingletonService<T>
    {
        public static List<T> instance = new List<T>();

        private ListaSingletonService() { }
        public static List<T> GetInstance()
        {
            if (instance == null)
            {
                instance = new List<T>();
            }
            return instance;
        }
    }
}
