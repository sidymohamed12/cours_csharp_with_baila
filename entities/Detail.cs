namespace gesdette.entities
{
    public class Detail : AbstractEntity
    {
        private int qteVendu;
        private double montantVendu;

        private Article? article;

        private Dette? dette;

        public int QteVendu
        {
            get => qteVendu;
            set => qteVendu = value;
        }

        public double MontantVendu
        {
            get => montantVendu;
            set => montantVendu = value;
        }

        public Article? Article
        {
            get => article;
            set => article = value;
        }

        public Dette? Dette
        {
            get => dette;
            set => dette = value;
        }
    }
}