using gesdette.controllers;
using gesdette.core;
using gesdette.core.factory.Impl;
using gesdette.entities;
using gesdette.repository.dapper;
using gesdette.repository.entity_framework;
using gesdette.repository.implement;
using gesdette.services;
using gesdette.services.servicespe;
using gesdette.views;
using gesdette.views.viewspe;

namespace gesdette
{
    public class Program
    {

        public static void Main(string[] args)
        {

            // ----------------------------- FACTORIES -----------------------------

            // factory repository
            // FactoryRepo<Client> clientRepoFactory = new(typeof(Client));
            // FactoryRepo<User> userRepoFactory = new(typeof(User));
            // FactoryRepo<Article> articleRepoFactory = new(typeof(Article));
            // FactoryRepo<Dette> detteRepoFactory = new(typeof(Dette));
            // FactoryRepo<Detail> detailRepoFactory = new(typeof(Detail));
            // FactoryRepo<Payement> payementRepoFactory = new(typeof(Payement));

            // initialisation des SERVICE pour chaque entité
            IClientRepository clientRepository = new ClientDapper();
            IUserRepository userRepository = new UserDapper();
            // IArticleRepository articleRepository = (ArticleRepositoryList)articleRepoFactory.createRepository();
            // IDetteRepository detteRepository = (DetteRepositoryList)detteRepoFactory.createRepository();
            // IDetailRepository detailRepository = (DetailRepositoryList)detailRepoFactory.createRepository();
            // IPayementRepository payementRepository = (PayementRepositoryList)payementRepoFactory.createRepository();

            // factory service
            FactoryService<Client> clientServiceFactory = new(typeof(Client), clientRepository);
            FactoryService<User> userServiceFactory = new(typeof(User), userRepository);
            // FactoryService<Article> articleServiceFactory = new(typeof(Article), articleRepository);
            // FactoryService<Dette> detteServiceFactory = new(typeof(Dette), detteRepository);
            // FactoryService<Detail> detailServiceFactory = new(typeof(Detail), detailRepository);
            // FactoryService<Payement> payementServiceFactory = new(typeof(Payement), payementRepository);

            // initialisation des SERVICE pour chaque entité
            IClientService clientService = (ClientService)clientServiceFactory.createService();
            IUserService userService = (UserService)userServiceFactory.createService();
            // IArticleService articleService = (ArticleService)articleServiceFactory.createService();
            // IDetteService detteService = (DetteService)detteServiceFactory.createService();
            // IDetailService detailService = (DetailService)detailServiceFactory.createService();
            // IPayementService payementService = (PayementService)payementServiceFactory.createService();

            // factory view
            FactoryView<User> userViewFactory = new(typeof(User), userService, null, null, null, null, null);
            FactoryView<Client> clientViewFactory = new(typeof(Client), userService, clientService, null, null, null, null);
            // FactoryView<Article> articleViewFactory = new(typeof(Article), null, null, articleService, null, null, null);
            // FactoryView<Dette> detteViewFactory = new(typeof(Dette), null, null, articleService, detteService, detailService, null);
            // FactoryView<Payement> payementViewFactory = new(typeof(Payement), null, null, null, detteService, null, payementService);

            // initialisation des VIEW pour chaque entité
            IUserView userView = (UserView)userViewFactory.createView();
            IClientView clientView = (ClientView)clientViewFactory.createView();
            // IArticleView articleView = (ArticleView)articleViewFactory.createView();
            // IDetteView detteView = (DetteView)detteViewFactory.createView();
            // IPayementView payementView = (PayementView)payementViewFactory.createView();

            // -----------------------------RECUPERATION USER CONNECT --------------------

            // Console.WriteLine("Création d'un user");
            // userService.create(userView.saisie());
            // User? userConnected = userView.authentification();
            // UserConnect.UserConnecte = userConnected;

            // ------------- DEROULEMENT POUR CHHAQUE ROLE si le user exist -----------

            //     if (userConnected != null)
            //     {
            //         switch (userConnected.Role)
            //         {
            //             case Role.admin:
            //                 {
            //                     Console.WriteLine("Connecting to admin, WELCOME...");
            //                     Controller adminController = new AdminController(clientView, userView, userService,
            //                             articleService, articleView);
            //                     adminController.execute();
            //                     break;
            //                 }
            //             case Role.boutiquier:
            //                 {
            //                     Console.WriteLine("Connecting to boutiquier...");
            //                     Controller bouController = new BoutiquierController(clientView, clientService, detteView,
            //                             articleView, payementView, payementService, detteService);
            //                     bouController.execute();
            //                     break;
            //                 }
            //             case Role.client:
            //                 {
            //                     Console.WriteLine("Connecting to Client...");
            //                     Controller clientController = new ClientController(detteView, detteService, articleView,
            //                             payementView, clientService);
            //                     clientController.execute();
            //                     break;
            //                 }
            //             default:
            //                 {
            //                     Console.WriteLine("role not found");
            //                     break;
            //                 }
            //         }
            // }

            // Console.WriteLine("ajout de client : ");
            // Client client = clientView.saisie();
            // clientService.create(client);
            // clientView.linkClientUser(client);
            // Console.WriteLine("----------------- listes Clients------------------ ");
            // clientView.afficher(clientService.findAll());
            // Console.WriteLine("----------------- listes Users-------------------- ");
            // userView.afficher(userService.findAll());


        }



    }
}