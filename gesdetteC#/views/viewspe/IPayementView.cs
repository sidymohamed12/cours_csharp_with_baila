using gesdette.core;
using gesdette.entities;

namespace gesdette.views.viewspe
{
    public interface IPayementView : View<Payement>
    {
        void listePayementsDette(Dette dette);
    }
}