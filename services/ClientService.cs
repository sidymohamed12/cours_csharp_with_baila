using gesdette.entities;
using gesdette.repository.implement;
using gesdette.services.servicespe;

namespace gesdette.services
{
    public class ClientService : IClientService
    {
        private IClientRepository clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }


        public void create(Client cl)
        {
            cl.onPrePersist();
            clientRepository.insert(cl);
        }


        public List<Client> findAll()
        {
            return clientRepository.selectAll();
        }


        public Client getBy(string name)
        {
            return clientRepository.selectBy(name);
        }


        public int count()
        {
            return clientRepository.count();
        }


        public void modifier(Client client)
        {
            client.onPreUpdated();
            clientRepository.update(client);
        }


        public Client getClientByUser(User user)
        {
            return clientRepository.getClientByUser(user);
        }
    }
}