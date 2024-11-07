using gesdette.core;
using gesdette.entities;

namespace gesdette.repository.implement
{
    public interface IUserRepository : Repository<User>
    {
        User selectBy(string name);

        User selectById(int id);

        void update(User user);

        string hashPassword(string plainPassword);

        bool verifyPassword(string plainPassword, string hashedPassword);

        User authentification(string login, string password);

    }
}