using gesdette.core;
using gesdette.entities;

namespace gesdette.repository.implement
{
    public interface IDetailRepository : Repository<Detail>
    {
        Detail selectBy(string name);

        Detail selectById(int id);

        void update(Detail value);
    }
}