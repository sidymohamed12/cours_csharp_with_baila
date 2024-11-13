using gesdette.core;
using gesdette.entities;

namespace gesdette.services.servicespe
{
    public interface IDetailService : Service<Detail>
    {
        void modifier(Detail detail);
    }
}