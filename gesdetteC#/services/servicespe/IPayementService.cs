using gesdette.core;
using gesdette.entities;

namespace gesdette.services.servicespe
{
    public interface IPayementService : Service<Payement>
    {
        void modifier(Payement payement);
        Payement getById(int id);
        List<Payement> getPayementsDette(Dette dette);

    }
}