using gesdette.core;
using gesdette.entities;
using gesdette.enums;
using gesdette.services.servicespe;
using gesdette.views.viewspe;

namespace gesdette.controllers
{
    public class BoutiquierController : Controller
    {
        private IClientView clientView;
        private IClientService clientService;
        private IDetteView detteView;
        private IDetteService detteService;
        private IArticleView articleView;
        private IPayementView payementView;
        private IPayementService payementService;

        public BoutiquierController(
                IClientView clientView, IClientService clientService,
                IDetteView detteView, IArticleView articleView,
                IPayementView payementView, IPayementService payementService, IDetteService detteService)
        {
            this.clientView = clientView;
            this.clientService = clientService;
            this.detteView = detteView;
            this.articleView = articleView;
            this.payementView = payementView;
            this.payementService = payementService;
            this.detteService = detteService;
        }



        public int menu()
        {
            Console.WriteLine("1--- Créer un client");
            Console.WriteLine("2--- Lister les clients ayant un compte (avec cumul des montants dus)");
            Console.WriteLine("3--- Lister les clients sans compte");
            Console.WriteLine("4--- Rechercher un client par son téléphone");
            Console.WriteLine("5--- Créer une dette pour un client");
            Console.WriteLine("6--- Enregistrer un paiement pour une dette");
            Console.WriteLine(
                    "7--- Lister les dettes non soldées d'un client avec l'option de voir les articles ou les paiements");
            Console.WriteLine(
                    "8--- Lister les demandes de dette (filtrer par En Cours ou Annuler) , voir les articles d'une dette et enfin valider ou refuser la dette.");
            Console.WriteLine("9- Quitter");
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
                            Client client = clientView.saisie();
                            clientService.create(client);
                            clientView.linkClientUser(client);
                            break;
                        }
                    case 2:
                        {
                            clientView.listerClientUser(true);
                            break;
                        }
                    case 3:
                        {
                            clientView.listerClientUser(false);
                            break;
                        }
                    case 4:
                        {
                            clientView.searchByTelephone();
                            break;
                        }
                    case 5:
                        {
                            Client client = clientView.getBy();
                            Console.WriteLine(client);
                            Dette dette = detteView.saisie();
                            dette.ClientD = client;
                            client.AddDettes(dette);
                            detteService.create(dette);
                            detteView.createDetteClient(dette);
                            break;
                        }
                    case 6:
                        {
                            Payement payement = payementView.saisie();
                            if (payement != null)
                            {
                                payementService.create(payement);
                            }
                            break;
                        }
                    case 7:
                        {
                            Client client = clientView.getBy();
                            if (client == null)
                            {
                                Console.WriteLine("Client non trouvé");
                                break;
                            }
                            else
                            {
                                detteView.ListedetteOfClient(client);
                            }

                            Dette dette = detteView.getById();
                            if (dette != null && dette.ClientD.Id.Equals(client.Id)
                                    && !dette.MontantRestant.Equals(0.0) && dette.EtatD.Equals(Etat.accepter))
                            {
                                Console.WriteLine("-------------- ARTICLES -------------");
                                articleView.listerArticleDette(dette);
                                Console.WriteLine("-------------- PAYEMENTS ------------");
                                payementView.listePayementsDette(dette);
                            }
                            else
                            {
                                Console.WriteLine("Aucune dette non-soldé disponible pour ce client avec ce id.");
                            }
                            break;

                        }
                    case 8:
                        {
                            Etat etat = detteView.saisiEtat();
                            detteView.filtrerDetteByEtat(etat);
                            Console.WriteLine("voir détail d'une dette ?");
                            if (detteView.askToContinue())
                            {
                                Dette dette = detteView.getById();
                                Console.WriteLine("-------------- ARTICLES -------------");
                                articleView.listerArticleDette(dette);
                                Console.WriteLine("-------------- PAYEMENTS ------------");
                                payementView.listePayementsDette(dette);
                                detteView.traiterDette(dette);
                            }
                            break;

                        }
                    case 9:
                        {
                            Console.WriteLine("deconnection done !");
                            break;
                        }
                }
            } while (choix != 9);
        }

    }
}