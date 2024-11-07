using gesdette.core.database.implement;
using gesdette.entities;
using gesdette.repository.implement;

namespace gesdette.repository.list
{
    public class ClientRepositoryListe : RepositoryListeImplement<Client>, IClientRepository
    {
        public Client getClientByUser(User user)
        {
            return user.Client;
        }

        public Client selectBy(string name)
        {
            return listes.FirstOrDefault(client => client.Surnom.Equals(name, StringComparison.Ordinal));
        }

        public Client selectById(int id)
        {
            return listes.FirstOrDefault(client => client.Id == id);
        }

        public void update(Client client)
        {
            var existingClient = listes.FirstOrDefault(cl => cl.Telephone.Equals(client.Telephone));
            if (existingClient != null)
            {
                existingClient.Surnom = client.Surnom;
                existingClient.Telephone = client.Telephone;
                existingClient.Adresse = client.Adresse;
                existingClient.User = client.User;
                existingClient.Dettes = client.Dettes;
                existingClient.onPreUpdated();
            }
        }
    }
}