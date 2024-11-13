using gesdette.core;
using gesdette.entities;
using gesdette.enums;
using gesdette.services.servicespe;
using gesdette.views.viewspe;

namespace gesdette.controllers
{
    public class ClientController : Controller
    {
        private IDetteView detteView;
        private IDetteService detteService;
        private IArticleView articleView;
        private IPayementView payementView;
        private IClientService clientService;
        private Client client;

        public ClientController(IDetteView detteView,
                IDetteService detteService, IArticleView articleView,
                IPayementView payementView, IClientService clientService)
        {
            this.detteService = detteService;
            this.detteView = detteView;
            this.articleView = articleView;
            this.payementView = payementView;
            this.clientService = clientService;
            client = this.clientService.getClientByUser(UserConnect.UserConnecte);
        }


        public int menu()
        {
            Console.WriteLine("1-  Lister ses dettes non soldées avec l'option de voir les articles ou les paiements");
            Console.WriteLine("2-  Faire une demande de Dette");
            Console.WriteLine("3-  Filtrer demandes de dette par état(En Cours, ou Annuler)");
            Console.WriteLine("4-  Envoyer une relance pour une demande de dette annuler");
            Console.WriteLine("5- Quitter");
            return int.Parse(Console.ReadLine());
        }


        public void execute()
        {
            int choix;
            do
            {
                switch (choix = menu())
                {
                    case 1:
                        {
                            detteView.ListedetteOfClient(client);
                            Console.WriteLine("voir détail d'une dette ?");
                            if (detteView.askToContinue())
                            {
                                Dette dette = detteView.getById();
                                if (dette != null && dette.ClientD.Id.Equals(client.Id)
                                        && dette.MontantRestant != 0.0 &&
                                        dette.EtatD.Equals(Etat.accepter))
                                {
                                    Console.WriteLine("-------------- ARTICLES -------------");
                                    articleView.listerArticleDette(dette);
                                    Console.WriteLine("-------------- PAYEMENTS ------------");
                                    payementView.listePayementsDette(dette);
                                }
                                else
                                {
                                    Console.WriteLine("Aucune dette non-soldé disponible pour vous avec ce id.");
                                }
                            }
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine(client);
                            Dette dette = detteView.saisie();
                            dette.ClientD = client;
                            client.AddDettes(dette);
                            detteService.create(dette);
                            detteView.createDetteClient(dette);
                        }
                        break;

                    case 3:
                        { detteView.ListeDemandeDetteClient(client); }
                        break;
                    case 4:

                    case 5:
                        Console.WriteLine("deconnexion done !");
                        break;
                }
            } while (choix != 5);

        }

    }
}