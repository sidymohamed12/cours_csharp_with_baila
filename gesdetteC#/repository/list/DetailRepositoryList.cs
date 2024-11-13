using gesdette.core.database.implement;
using gesdette.entities;
using gesdette.repository.implement;

namespace gesdette.repository.list
{
    public class DetailRepositoryList : RepositoryListeImplement<Detail>, IDetailRepository
    {
        public Detail selectBy(string name)
        {
            throw new NotImplementedException();
        }

        public Detail? selectById(int id)
        {
            return listes.FirstOrDefault(detail => detail.Id == id);
        }

        public void update(Detail value)
        {
            var existingDetail = listes.FirstOrDefault(detail => detail.Id == value.Id);
            if (existingDetail != null)
            {
                existingDetail.QteVendu = value.QteVendu;
                existingDetail.MontantVendu = value.MontantVendu;
                existingDetail.Dette = value.Dette;
                existingDetail.Article = value.Article;
                existingDetail.onPreUpdated();
            }
        }
    }
}