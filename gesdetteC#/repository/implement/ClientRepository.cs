using gesdette.core;
using gesdette.entities;

namespace gesdette.repository.implement
{
    public interface IClientRepository : Repository<Client>
    {
        Client? selectBy(string name);

        Client? selectById(int id);

        void update(Client client);

        Client? getClientByUser(User user);
    }
}