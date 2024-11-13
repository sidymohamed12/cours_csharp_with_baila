using gesdette.entities;
using gesdette.repository.implement;
using gesdette.services.servicespe;

namespace gesdette.services
{
    public class DetailService : IDetailService
    {

        private IDetailRepository detailRepository;

        public DetailService(IDetailRepository detailRepository)
        {
            this.detailRepository = detailRepository;
        }


        public void create(Detail value)
        {
            value.onPrePersist();
            detailRepository.insert(value);
        }


        public List<Detail> findAll()
        {
            return detailRepository.selectAll();
        }


        public Detail getBy(string name)
        {
            return detailRepository.selectBy(name);
        }


        public int count()
        {
            return detailRepository.count();
        }


        public void modifier(Detail detail)
        {
            detail.onPreUpdated();
            detailRepository.update(detail);
        }

    }
}