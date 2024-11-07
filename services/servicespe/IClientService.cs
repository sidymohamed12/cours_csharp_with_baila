using gesdette.core;
using gesdette.entities;

namespace gesdette.services.servicespe
{
    public interface IClientService : Service<Client>
    {
        void modifier(Client client);

        Client getClientByUser(User user);
    }
}