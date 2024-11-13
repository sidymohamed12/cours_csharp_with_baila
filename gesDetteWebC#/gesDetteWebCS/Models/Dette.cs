using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using gesDetteWebCS.Models.enums;

namespace gesDetteWebCS.Models
{
    public class Dette : AbstractEntity
    {
        [Required]
        public double Montant { get; set; }
        [Required]
        public double MontantVerser { get; set; }
        [Required]
        public double MontantRestant { get; set; }
        [Required]
        public bool Archiver { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public Client ClientD { get; set; }
        [Required]
        public Etat EtatD { get; set; }
        [NotMapped]
        public List<Detail>? Details { get; set; }
        [NotMapped]
        public List<Payement>? Payements { get; set; }

        public void addDetail(Detail detail) { Details?.Add(detail); }

        public void addPayement(Payement pay)
        {
            Payements?.Add(pay);
            MontantRestant = Montant - MontantVerser;
        }

        public override bool Equals(object? obj)
        {
            return obj is Dette dette &&
                   Date == dette.Date;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Date);
        }
    }
}