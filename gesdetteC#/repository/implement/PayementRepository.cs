using gesdette.core;
using gesdette.entities;

namespace gesdette.repository.implement
{
    public interface IPayementRepository : Repository<Payement>
    {
        Payement? selectBy(string name);

        Payement? selectById(int id);

        void update(Payement payement);

        List<Payement>? payementsDette(Dette dette);

    }
}