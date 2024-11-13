
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gesDetteWebCS.Models
{
    public class Article : AbstractEntity
    {
        [Required]
        public string Libelle { get; set; }
        [Required]
        public string Reference { get; set; }
        [Required]
        public int QteStock { get; set; }
        [Required]
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