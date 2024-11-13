using System.ComponentModel.DataAnnotations.Schema;
using gesDetteWebCS.Models.enums;

namespace gesDetteWebCS.Models
{
    public class Dette : AbstractEntity
    {

        public double Montant { get; set; }

        public double MontantVerser { get; set; }

        public double MontantRestant { get; set; }

        public bool Archiver { get; set; }

        public DateTime Date { get; set; }

        public Client? ClientD { get; set; }

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