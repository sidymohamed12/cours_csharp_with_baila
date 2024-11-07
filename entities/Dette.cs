using gesdette.enums;

namespace gesdette.entities
{
    public class Dette : AbstractEntity
    {

        private double montant;
        private double montantVerser;
        private double montantRestant;
        private bool archiver;
        private DateTime date;
        private Client? clientD;
        private Etat etatD;
        private List<Detail> details = new();
        private List<Payement> payements = new();

        public double Montant
        {
            get => montant;
            set => montant = value;
        }

        public double MontantVerser
        {
            get => montantVerser;
            set => montantVerser = value;
        }

        public double MontantRestant
        {
            get => montant - montantVerser;
            set => montantRestant = value;
        }

        public bool Archiver
        {
            get => archiver;
            set => archiver = value;
        }

        public DateTime Date
        {
            get => date;
            set => date = value;
        }

        public Client? ClientD
        {
            get => clientD;
            set => clientD = value;
        }

        public Etat EtatD
        {
            get => etatD;
            set => etatD = value;
        }

        public List<Detail> Details
        {
            get => details;
            set => details = value;
        }

        public List<Payement> Payements
        {
            get => payements;
            set => payements = value;
        }
        public void addDetail(Detail detail)
        {
            details.Add(detail);
        }

        public void addPayement(Payement pay)
        {
            payements.Add(pay);
            montantRestant = montant - montantVerser;
        }

        public override bool Equals(object? obj)
        {
            return obj is Dette dette &&
                   montant == dette.montant &&
                   montantVerser == dette.montantVerser &&
                   montantRestant == dette.montantRestant &&
                   archiver == dette.archiver &&
                   date == dette.date &&
                   EqualityComparer<Client?>.Default.Equals(clientD, dette.clientD) &&
                   etatD == dette.etatD;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(montant, montantVerser, montantRestant, archiver, date, clientD, etatD);
        }

        public override string ToString()
        {
            return "Dette [id=" + id + ", montant=" + montant + ", montantVerser=" + montantVerser + ", montantRestant="
                    + montantRestant
                    + ", date=" + date + ", clientD=" + clientD + ", etatD=" + etatD + "]";
        }

    }
}