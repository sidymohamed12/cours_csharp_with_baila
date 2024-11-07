using gesdette.core;
using gesdette.entities;

namespace gesdette.views.viewspe
{
    public interface IClientView : View<Client>
    {
        void linkClientUser(Client client);
        void listerClientUser(bool statut);
        void searchByTelephone();
        void createUserForClient();
    }
}