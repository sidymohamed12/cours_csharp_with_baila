namespace gesDetteWebCS.Models
{
    public class UserConnect
    {
        private static User? userConnecte;

        public static User? UserConnecte
        {
            get => userConnecte;
            set => userConnecte = value;
        }
    }
}