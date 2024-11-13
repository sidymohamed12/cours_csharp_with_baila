using gesdette.core.database.implement;
using gesdette.entities;
using gesdette.repository.implement;

namespace gesdette.repository.list
{
    public class PayementRepositoryList : RepositoryListeImplement<Payement>, IPayementRepository
    {
        public List<Payement>? payementsDette(Dette dette)
        {
            return dette.Payements;
        }

        public Payement selectBy(string name)
        {
            throw new NotImplementedException();
        }

        public Payement? selectById(int id)
        {
            return listes.FirstOrDefault(payement => payement.Id == id);
        }

        public void update(Payement payement)
        {
            var existingPayement = listes.FirstOrDefault(value => value.Id == payement.Id);
            if (existingPayement != null)
            {
                existingPayement.Dette = payement.Dette;
                existingPayement.Date = payement.Date;
                existingPayement.Montant = payement.Montant;
                existingPayement.onPreUpdated();
            }
        }
    }
}