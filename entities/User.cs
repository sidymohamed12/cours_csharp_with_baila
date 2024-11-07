using gesdette.enums;

namespace gesdette.entities
{
    public class User : AbstractEntity
    {

        private string? login;
        private string? password;
        private Role role;
        private bool etat;
        private Client? client;

        public string? Login
        {
            get => login;
            set => login = value;
        }

        public string? Password
        {
            get => password;
            set => password = value;
        }

        public Role Role
        {
            get => role;
            set => role = value;
        }

        public bool Etat
        {
            get => etat;
            set => etat = value;
        }

        public Client? Client
        {
            get => client;
            set => client = value;
        }

        public override bool Equals(object? obj)
        {
            return obj is User user &&
                   login == user.login &&
                   password == user.password &&
                   role == user.role &&
                   etat == user.etat &&
                   EqualityComparer<Client?>.Default.Equals(client, user.client);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(login, password, role, etat, client);
        }

        public override string ToString()
        {
            return "User [id=" + id + ", login=" + login + ", password=" + password + ", role=" + role + ", etat="
                    + etat
                    + "]";
        }

    }
}