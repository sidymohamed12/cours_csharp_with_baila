using gesdette.core;
using gesdette.entities;

namespace gesdette.services.servicespe
{
    public interface IUserService : Service<User>
    {
        void modifier(User user);

        User connexion(string login, string password);
    }
}