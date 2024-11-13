using gesdette.entities;
using gesdette.repository.implement;

namespace gesdette.repository.entity_framework
{
    using BCrypt.Net;
    public class UserEF : RepositoryEFImplement<User>, IUserRepository
    {
        public User? authentification(string login, string password)
        {
            throw new NotImplementedException();
        }

        public string hashPassword(string plainPassword)
        {
            return BCrypt.HashPassword(plainPassword, BCrypt.GenerateSalt());
        }


        public bool verifyPassword(string plainPassword, string hashedPassword)
        {
            return BCrypt.Verify(plainPassword, hashedPassword);
        }
    }
}