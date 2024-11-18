

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gesDetteWebCS.Models

{
    public class Detail : AbstractEntity
    {
        [Required]
        public int QteVendu { get; set; }
        [Required]
        public double MontantVendu { get; set; }
        [Required]
        public Article Article { get; set; }
        [Required]
        public Dette Dette { get; set; }

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