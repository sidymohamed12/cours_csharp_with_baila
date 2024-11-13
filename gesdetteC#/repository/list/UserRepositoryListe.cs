using gesdette.core.database.implement;
using gesdette.repository.implement;

using gesdette.entities;
namespace gesdette.repository.list
{
    using BCrypt.Net;
    public class UserRepositoryListe : RepositoryListeImplement<User>, IUserRepository
    {

        public string hashPassword(string plainPassword)
        {
            return BCrypt.HashPassword(plainPassword, BCrypt.GenerateSalt());
        }


        public bool verifyPassword(string plainPassword, string hashedPassword)
        {
            return BCrypt.Verify(plainPassword, hashedPassword);
        }


        public User? authentification(string login, string password)
        {
            User user = selectBy(login);
            if (user != null && verifyPassword(password, user.Password!))
            {
                return user;
            }
            else
            {
                return null;
            }
        }




        public User selectBy(string name)
        {
            return listes.FirstOrDefault(user => user?.Login?.Equals(name, StringComparison.Ordinal) == true)!;
        }

        public User selectById(int id)
        {
            return listes.FirstOrDefault(user => user.Id == id)!;
        }

        public void update(User user)
        {
            var existingUser = listes.FirstOrDefault(value => value?.Login?.Equals(user.Login, StringComparison.Ordinal) == true);
            if (existingUser != null)
            {
                existingUser.Login = user.Login;
                existingUser.Password = user.Password;
                existingUser.Role = user.Role;
                existingUser.Etat = user.Etat;
                existingUser.Client = user.Client;
            }
        }

    }
}