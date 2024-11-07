using gesdette.core;
using gesdette.entities;
using gesdette.enums;
using gesdette.services.servicespe;
using gesdette.views.viewspe;

namespace gesdette.views
{
    public class DetteView : ViewImplement<Dette>, IDetteView
    {

        private IArticleService articleService;
        private IDetailService detailService;
        private IDetteService detteService;

        public DetteView(IArticleService articleService, IDetailService detailService,
                IDetteService detteService)
        {

            this.articleService = articleService;
            this.detailService = detailService;
            this.detteService = detteService;
        }


        public Dette saisie()
        {
            Dette dette = new()
            {
                Date = DateTime.Now,
                Montant = 0.0,
                MontantVerser = 0.0,
                MontantRestant = 0.0,
                Archiver = false
            };
            if (UserConnect.UserConnecte.Role.Equals(Role.boutiquier))
            {
                dette.EtatD = Etat.accepter;
            }
            else
            {
                dette.EtatD = Etat.encours;
            }

            return dette;

        }



        public void createDetteClient(Dette dette)//-+
        {
            do
            {
                articleService.findAll()
                        .Where(art => art.QteStock != 0)
                        .ToList()
                        .ForEach(Console.WriteLine);

                Console.WriteLine("Choisissez l'article par son ref : ");
                Article article = articleService.getBy(Console.ReadLine());
                Console.WriteLine(article);

                if (article == null || article.QteStock == 0.0)
                {
                    Console.WriteLine("Article inexistant ou en rupture de stock. Veuillez recommencer.");
                    return;
                }
                else
                {
                    int qte;
                    do
                    {
                        Console.WriteLine("Choisissez la quantité de l'article");
                        qte = int.Parse(Console.ReadLine());
                    } while (qte > article.QteStock || qte <= 0);

                    double montantArticle = qte * article.Prix;
                    article.QteStock = article.QteStock - qte;
                    articleService.modifier(article);
                    Detail existingDetail = dette.Details
                            .Where(detail => detail.Article.Equals(article))
                            .ToList()
                            .First();

                    if (existingDetail != null)
                    {
                        existingDetail.QteVendu += qte;
                        existingDetail.MontantVendu += montantArticle;
                        detailService.modifier(existingDetail);
                    }
                    else
                    {

                        Detail detail = new()
                        {
                            QteVendu = qte,
                            MontantVendu = montantArticle,
                            Article = article,
                            Dette = dette
                        };
                        detailService.create(detail);
                        dette.addDetail(detail);

                    }

                    dette.Montant = dette.Montant + montantArticle;

                    Console.WriteLine("Voulez-vous continuer ? ");
                }
            } while (askToContinue());

            detteService.modifier(dette);
        }



        public Dette? getBy()
        {
            // detteService.findAll().forEach(System.out::println);
            // Console.WriteLine("Entrez le telephone du client : ");
            // String tel = Console.ReadLine();
            // Dette dette = detteService.getBy(tel);
            // return dette;
            return null;
        }


        public Etat saisiEtat()
        {
            int choix;
            do
            {
                Console.WriteLine("veuillez selectionner l'etat");
                foreach (Etat etat in Enum.GetValues(typeof(Etat)))
                {
                    Console.WriteLine((int)etat + 1 + "-" + etat.ToString());
                }
                choix = int.Parse(Console.ReadLine());
            } while (choix <= 0 || choix > Enum.GetValues(typeof(Etat)).Length);

            return (Etat)(choix - 1);
        }


        public void listerDetteNonSolde()
        {
            detteService.findAll()
                .Where(dette => dette.Montant != dette.MontantVerser)
                .ToList()
                .ForEach(Console.WriteLine);
        }



        public void listerDetteSolde()
        {
            detteService.findAll()
                .Where(dette => dette.Montant == dette.MontantVerser)
                .ToList()
                .ForEach(Console.WriteLine);
        }



        public void filtrerDetteByEtat(Etat etat)
        {
            detteService.findAll()
                .Where(dette => dette.EtatD.Equals(etat))
                .ToList()
                .ForEach(Console.WriteLine);
        }



        public Dette? getById()
        {
            Console.WriteLine("Entrez l'id de la dette : ");
            int id = int.Parse(Console.ReadLine());
            Dette dette = detteService.getById(id);
            if (dette != null)
            {
                return dette;
            }
            else
            {
                Console.WriteLine("No dette found");
                return null;
            }
        }



        public void traiterDette(Dette dette)
        {
            if (dette.EtatD.Equals(Etat.encours))
            {
                int choix;
                do
                {
                    Console.WriteLine("1- Accepter");
                    Console.WriteLine("2- Annuler");
                    choix = int.Parse(Console.ReadLine());
                } while (choix <= 0 || choix > 2);

                switch (choix)
                {
                    case 1:
                        dette.EtatD = Etat.accepter;
                        detteService.modifier(dette);
                        break;
                    case 2:
                        dette.EtatD = Etat.annuler;
                        detteService.modifier(dette);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Console.WriteLine("cette dette est déjà acceptée ou annulée");
                return;
            }

        }


        public void ListedetteOfClient(Client client)
        {
            detteService.detteOfClient(client)
                .Where(dette => dette.ClientD.Id == client.Id && dette.EtatD.Equals(Etat.accepter) && dette.MontantRestant != 0.0)
                .ToList()
                .ForEach(Console.WriteLine);
        }



        public void ListeDemandeDetteClient(Client client)
        {
            detteService.detteOfClient(client)
                .Where(dette => dette.ClientD.Equals(client) && !dette.EtatD.Equals(Etat.accepter))
                .ToList()
                .ForEach(Console.WriteLine);
            int choix;
            do
            {
                Console.WriteLine("1- encours");
                Console.WriteLine("2- annuler");
                choix = int.Parse(Console.ReadLine());
            } while (choix <= 0 || choix > 2);

            Etat etat = choix switch
            {
                1 => Etat.encours,
                2 => Etat.annuler,
                _ => throw new NotImplementedException(),
            };
            detteService.detteOfClient(client)
                .Where(dette => dette.ClientD.Equals(client) && dette.EtatD.Equals(etat))
                .ToList()
                .ForEach(Console.WriteLine);
        }


    }
}