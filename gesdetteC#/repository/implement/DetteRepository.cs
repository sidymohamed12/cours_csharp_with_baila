using gesdette.core;
using gesdette.entities;

namespace gesdette.repository.implement
{
    public interface IDetteRepository : Repository<Dette>
    {
        Dette? selectBy(string name);

        Dette? selectById(int id);

        void update(Dette dette);

        List<Dette>? detteOfClient(Client client);
    }
}