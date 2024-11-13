using gesdette.core;
using gesdette.entities;
using gesdette.enums;
using gesdette.services.servicespe;
using gesdette.views.viewspe;

namespace gesdette.views
{
    public class ClientView : ViewImplement<Client>, IClientView
    {

        private IClientService clientService;
        private IUserService userService;

        public ClientView(IClientService clientService, IUserService userService)
        {

            this.clientService = clientService;
            this.userService = userService;
        }

        public new Client saisie()
        {
            Client client = new();
            Console.WriteLine("entrer le surnom : ");
            client.Surnom = Console.ReadLine();

            Console.WriteLine("entrer son adresse : ");
            client.Adresse = Console.ReadLine();

            Console.WriteLine("entrer son telephone : ");
            client.Telephone = Console.ReadLine();

            return client;
        }



        public new Client getBy()
        {
            clientService.findAll().ForEach(Console.WriteLine);
            Console.WriteLine("Entrez le telephone du client : ");
            string tel = Console.ReadLine()!;
            Client client = clientService.getBy(tel);
            return client;
        }


        public void linkClientUser(Client client)
        {
            Console.WriteLine("Voulez-vous ajouter un compte pour ce client : ");
            if (askToContinue())
            {
                User user = new();

                Console.WriteLine("entrer le login : ");
                user.Login = Console.ReadLine();

                Console.WriteLine("entrer le mdp : ");
                user.Password = Console.ReadLine();

                user.Role = Role.client;
                user.Etat = true;
                user.Client = client;
                userService.create(user);

                client.User = user;
                clientService.modifier(client);
            }

        }


        public void listerClientUser(bool statut)
        {
            clientService.findAll()
                .Where(cl => statut && cl.User != null || !statut && cl.User == null)
                .ToList()
                .ForEach(Console.WriteLine);
        }



        public void searchByTelephone()
        {
            Client client = getBy();
            if (client != null)
            {
                Console.WriteLine("client : " + client);
            }
            else
            {
                Console.WriteLine("client introuvable");
            }
        }



        public void createUserForClient()
        {
            Client client = getBy();
            if (client != null)
            {
                Console.WriteLine("client : " + client);
                if (client.User == null)
                {
                    linkClientUser(client);
                }
                else
                {
                    Console.WriteLine("Le client a déjà un utilisateur associé.");
                }
            }
            else
            {
                Console.WriteLine("Client introuvable.");
            }
        }


    }
}