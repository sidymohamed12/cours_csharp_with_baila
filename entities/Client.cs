namespace gesdette.entities
{
    public class Client : AbstractEntity
    {
        private string? surnom;
        private string? telephone;
        private string? adresse;
        private User? user;
        private List<Dette> dettes = [];

        public string? Surnom
        {
            get { return surnom; }
            set { surnom = value; }
        }


        public string? Telephone
        {
            get { return telephone; }
            set { telephone = value; }
        }

        public string? Adresse
        {
            get { return adresse; }
            set { adresse = value; }
        }

        public User? User
        {
            get { return user; }
            set { user = value; }
        }

        public List<Dette> Dettes
        {
            get { return dettes; }
            set { dettes = value; }
        }

        public void AddDettes(Dette dette)
        {
            dettes.Add(dette);
        }

        public override string ToString()
        {
            return "Client [id=" + id + ", surnom=" + surnom + ", telephone=" + telephone + ", adresse=" + adresse
                    + ", user=" + user + "]";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(id, Surnom, Telephone, Adresse, User);
        }

        public override bool Equals(object? obj)
        {
            return obj is Client client &&
                   surnom == client.surnom &&
                   telephone == client.telephone &&
                   adresse == client.adresse &&
                   EqualityComparer<User?>.Default.Equals(user, client.user);
        }
    }
}