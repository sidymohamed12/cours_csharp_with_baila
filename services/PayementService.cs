using gesdette.entities;
using gesdette.repository.implement;
using gesdette.services.servicespe;

namespace gesdette.services
{
    public class PayementService : IPayementService
    {

        private IPayementRepository payementRepository;

        public PayementService(IPayementRepository payementRepository)
        {
            this.payementRepository = payementRepository;
        }


        public void create(Payement value)
        {
            value.onPrePersist();
            payementRepository.insert(value);
        }


        public List<Payement> findAll()
        {
            return payementRepository.selectAll();
        }


        public Payement getBy(string name)
        {
            return payementRepository.selectBy(name);
        }


        public int count()
        {
            return payementRepository.count();
        }


        public void modifier(Payement payement)
        {
            payement.onPreUpdated();
            payementRepository.update(payement);
        }


        public Payement getById(int id)
        {
            return payementRepository.selectById(id);
        }


        public List<Payement> getPayementsDette(Dette dette)
        {
            return payementRepository.payementsDette(dette);
        }

    }
}