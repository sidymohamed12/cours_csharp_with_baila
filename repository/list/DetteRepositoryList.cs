using gesdette.core.database.implement;
using gesdette.entities;
using gesdette.repository.implement;

namespace gesdette.repository.list
{
    public class DetteRepositoryList : RepositoryListeImplement<Dette>, IDetteRepository
    {
        public List<Dette> detteOfClient(Client client)
        {
            return client.Dettes;
        }

        public Dette selectBy(string name)
        {
            throw new NotImplementedException();
        }

        public Dette selectById(int id)
        {
            return listes.FirstOrDefault(dette => dette.Id == id);
        }

        public void update(Dette dette)
        {
            var existingDette = listes.FirstOrDefault(value => value.Id == dette.Id);
            if (existingDette != null)
            {
                existingDette.Montant = dette.Montant;
                existingDette.MontantVerser = dette.MontantVerser;
                existingDette.MontantRestant = dette.MontantRestant;
                existingDette.Details = dette.Details;
                existingDette.Payements = dette.Payements;
                existingDette.Archiver = dette.Archiver;
                existingDette.ClientD = dette.ClientD;
                existingDette.Date = dette.Date;
                existingDette.EtatD = dette.EtatD;
                existingDette.onPreUpdated();
            }
        }
    }
}