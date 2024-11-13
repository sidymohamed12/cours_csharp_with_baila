using gesdette.core.database.implement;
using gesdette.entities;
using gesdette.repository.implement;

namespace gesdette.repository.dapper
{
    public class ClientDapper : RepositoryBDImplDapper<Client>, IClientRepository
    {
        public ClientDapper()
        {
            clazz = typeof(Client);
            tableName = "Clients";
            colomnSelectBy = "Telephone";
            colones = ["Surnom", "Telephone", "Adresse", "UserId", "CreatedAt", "UpdatedAt"];
        }

        public Client? getClientByUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}