namespace gesDetteWebCS.Models

{
    public class Payement : AbstractEntity
    {

        public DateTime Date { get; set; }

        public Dette? Dette { get; set; }

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