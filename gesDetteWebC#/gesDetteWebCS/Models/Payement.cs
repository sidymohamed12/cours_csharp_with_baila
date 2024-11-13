using System.ComponentModel.DataAnnotations;

namespace gesDetteWebCS.Models

{
    public class Payement : AbstractEntity
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public Dette? Dette { get; set; }
        [Required]
        public double Montant { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Payement payement &&
                   Date == payement.Date;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Date);
        }
    }
}