using gesdette.entities;
using gesdette.repository.implement;

namespace gesdette.repository.entity_framework
{
    public class ClientEF : RepositoryEFImplement<Client>, IClientRepository
    {
        public Client? getClientByUser(User user)
        {
            using var context = new AppDbContext();
            return context.Clients.FirstOrDefault(c => c.User!.Id.Equals(user.Id) );
        }
    }
}