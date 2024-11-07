using gesdette.core;
using gesdette.entities;
using gesdette.enums;

namespace gesdette.views.viewspe
{
    public interface IDetteView : View<Dette>
    {
        Etat saisiEtat();

        void listerDetteNonSolde();

        void listerDetteSolde();

        void filtrerDetteByEtat(Etat etat);

        void createDetteClient(Dette dette);

        Dette getById();

        void traiterDette(Dette dette);

        void ListedetteOfClient(Client client);

        void ListeDemandeDetteClient(Client client);

    }
}