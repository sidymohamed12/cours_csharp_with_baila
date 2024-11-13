using gesdette.core.database.implement;
using gesdette.entities;
using gesdette.repository.implement;

namespace gesdette.repository.dapper
{
    using BCrypt.Net;
    public class UserDapper : RepositoryBDImplDapper<User>, IUserRepository
    {
        public UserDapper()
        {
            clazz = typeof(User);
            tableName = "Users";
            colomnSelectBy = "Login";
            colones = ["Login", "Password", "Role", "Etat", "CreatedAt", "UpdatedAt"];
        }

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