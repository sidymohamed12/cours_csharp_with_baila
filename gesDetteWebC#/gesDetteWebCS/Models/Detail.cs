

namespace gesDetteWebCS.Models

{
    public class Detail : AbstractEntity
    {

        public int QteVendu { get; set; }

        public double MontantVendu { get; set; }

        public Article? Article { get; set; }

        public Dette? Dette { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Detail detail &&
                   QteVendu == detail.QteVendu;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(QteVendu);
        }
    }
}