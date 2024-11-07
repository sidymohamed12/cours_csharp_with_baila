using gesdette.core;
using gesdette.entities;
using gesdette.services.servicespe;
using gesdette.views.viewspe;

namespace gesdette.controllers
{
    public class AdminController : Controller
    {
        private IClientView clientView;
        private IUserView userView;
        private IUserService userService;
        private IArticleService articleService;
        private IArticleView articleView;

        public AdminController(
                IClientView clientView,
                IUserView userView, IUserService userService,
                IArticleService articleService, IArticleView articleView)
        {
            this.clientView = clientView;
            this.userService = userService;
            this.userView = userView;
            this.articleService = articleService;
            this.articleView = articleView;
        }


        public int menu()
        {
            Console.WriteLine("1- Créer un compte utilisateur à un client n'ayant pas de compte");
            Console.WriteLine("2- Créer un compte utilisateur avec un rôle Boutiquier ou  Admin");
            Console.WriteLine("3- Activer un compte utilisateur");
            Console.WriteLine("4- Désactiver un compte utilisateur");
            Console.WriteLine("5- Afficher les comptes utilisateurs actifs");
            Console.WriteLine("6- Afficher les comptes utilisateurs  par rôle");
            Console.WriteLine("7- Ajouter un article");
            Console.WriteLine("8- Lister tous les articles");
            Console.WriteLine("9- Lister article disponible");
            Console.WriteLine("10- Mettre à jour la quantité d'un article");
            Console.WriteLine("12- Deconnexion");
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
                            clientView.createUserForClient();
                            break;
                        }
                    case 2:
                        {
                            userService.create(userView.saisie());
                            break;
                        }
                    case 3:
                        {
                            userView.setEtatUser(true);
                            break;
                        }
                    case 4:
                        {
                            userView.setEtatUser(false);
                            break;
                        }
                    case 5:
                        {
                            userView.listerUserActif();
                            break;
                        }
                    case 6:
                        {
                            userView.listerUserbyRole();
                            break;
                        }
                    case 7:
                        {
                            articleService.create(articleView.saisie());
                            break;
                        }
                    case 8:
                        {
                            articleService.findAll().ForEach(Console.WriteLine);
                            break;
                        }
                    case 9:
                        {
                            articleView.listerArticleDispo();
                            break;
                        }
                    case 10:
                        {
                            articleView.updateQteArticle();
                            break;
                        }
                    case 12:
                        {
                            Console.WriteLine("Deconnexion fait ");
                            break;
                        }
                }
            } while (choix != 12);
        }

    }
}