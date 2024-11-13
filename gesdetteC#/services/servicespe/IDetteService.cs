using gesdette.core;
using gesdette.entities;

namespace gesdette.services.servicespe
{
    public interface IDetteService : Service<Dette>
    {
        void modifier(Dette dette);

        Dette getById(int id);

        List<Dette> detteOfClient(Client client);
    }
}