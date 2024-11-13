using gesdette.entities;
using gesdette.repository.implement;
using gesdette.services.servicespe;

namespace gesdette.services
{
    public class UserService : IUserService
    {

        private IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }


        public void create(User user)
        {
            user.Password = userRepository.hashPassword(user.Password!);
            user.onPrePersist();
            userRepository.insert(user);
        }


        public List<User> findAll()
        {
            return userRepository.selectAll();
        }


        public User getBy(string login)
        {
            return userRepository.selectBy(login);
        }


        public int count()
        {
            return userRepository.count();
        }


        public void modifier(User user)
        {
            user.onPreUpdated();
            userRepository.update(user);
        }


        public User connexion(string login, string password)
        {
            return userRepository.authentification(login, password);
        }
    }
}