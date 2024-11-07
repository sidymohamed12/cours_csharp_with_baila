namespace gesdette.entities
{
    public class Article : AbstractEntity
    {
        private string? libelle;
        private string? reference;
        private int qteStock;
        private double prix;
        private List<Detail> details = [];

        public string? Libelle
        {
            get => libelle;
            set => libelle = value;
        }

        public string? Reference
        {
            get => reference;
            set => reference = value;
        }

        public int QteStock
        {
            get => qteStock;
            set => qteStock = value;
        }

        public double Prix
        {
            get => prix;
            set => prix = value;
        }

        public List<Detail> Details
        {
            get => details;
            set => details = value;
        }

        public void addDetail(Detail detail)
        {
            details.Add(detail);
        }

        public override bool Equals(object? obj)
        {
            return obj is Article article &&
                   libelle == article.libelle &&
                   reference == article.reference &&
                   qteStock == article.qteStock;
        }

        public override string ToString()
        {
            return "Article [libelle=" + libelle + ", ref=" + reference + ", qteStock=" + qteStock + ", prix=" + prix + "]";
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}