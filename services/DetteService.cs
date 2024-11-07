using gesdette.entities;
using gesdette.repository.implement;
using gesdette.services.servicespe;

namespace gesdette.services
{
    public class DetteService : IDetteService
    {

        private IDetteRepository detteRepository;

        public DetteService(IDetteRepository detteRepository)
        {
            this.detteRepository = detteRepository;
        }


        public void create(Dette value)
        {
            value.onPrePersist();
            detteRepository.insert(value);
        }


        public List<Dette> findAll()
        {
            return detteRepository.selectAll();
        }


        public Dette getBy(string name)
        {
            return detteRepository.selectBy(name);
        }


        public int count()
        {
            return detteRepository.count();
        }


        public void modifier(Dette dette)
        {
            dette.onPreUpdated();
            detteRepository.update(dette);
        }


        public Dette getById(int id)
        {
            return detteRepository.selectById(id);
        }


        public List<Dette> detteOfClient(Client client)
        {
            return detteRepository.detteOfClient(client);
        }

    }
}