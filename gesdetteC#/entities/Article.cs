
using System.ComponentModel.DataAnnotations.Schema;

namespace gesdette.entities
{
    public class Article : AbstractEntity
    {

        public string? Libelle { get; set; }

        public string? Reference { get; set; }

        public int QteStock { get; set; }

        public double Prix { get; set; }
        [NotMapped]
        public List<Detail>? Details { get; set; }

        public void AddDettes(Detail detail)
        {
            Details?.Add(detail);
        }

        public override bool Equals(object? obj)
        {
            return obj is Article article &&
                   Libelle == article.Libelle;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Libelle);
        }
    }
}