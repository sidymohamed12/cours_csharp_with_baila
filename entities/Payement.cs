namespace gesdette.entities
{
    public class Payement : AbstractEntity
    {

        private DateTime date;
        private Dette? dette;
        private double montant;

        public DateTime Date
        {
            get => date;
            set => date = value;
        }

        public Dette? Dette
        {
            get => dette;
            set => dette = value;
        }

        public double Montant
        {
            get => montant;
            set => montant = value;
        }

        public override bool Equals(object? obj)
        {
            return obj is Payement payement &&
                   date == payement.date &&
                   EqualityComparer<Dette?>.Default.Equals(dette, payement.dette) &&
                   montant == payement.montant;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(date, dette, montant);
        }

        public override string ToString()
        {
            return "Payement [date=" + date + ", montant=" + montant + "]";
        }
    }
}