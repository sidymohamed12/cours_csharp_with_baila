using gesdette.core;
using gesdette.entities;
using gesdette.services.servicespe;
using gesdette.views.viewspe;

namespace gesdette.views
{
    public class ArticleView : ViewImplement<Article>, IArticleView
    {

        private IArticleService articleService;

        public ArticleView(IArticleService articleService)
        {

            this.articleService = articleService;
        }


        public Article saisie()
        {
            Article article = new();

            Console.WriteLine("Entrez le libelle de l'article : ");
            article.Libelle = Console.ReadLine();

            Console.WriteLine("Entrez la quantité en stock de l'article : ");
            article.QteStock = int.Parse(Console.ReadLine());

            Console.WriteLine("Entrez le prix unitaire de l'article : ");
            article.Prix = double.Parse(Console.ReadLine());

            int id = articleService.count() + 1;
            int size = id.ToString().Length;
            article.Reference = "ART" + new string('0', Math.Max(0, 4 - size)) + id;

            return article;
        }



        public Article getBy()
        {
            articleService.findAll().ForEach(Console.WriteLine);

            Console.WriteLine("Entrez la reference de l'article à mise à jour : ");
            string reference = Console.ReadLine();

            Article article = articleService.getBy(reference);

            return article;
        }



        public void listerArticleDispo()
        {
            articleService.findAll()
                .Where(art => art.QteStock != 0)
                .ToList()
                .ForEach(Console.WriteLine);
        }



        public void updateQteArticle()
        {
            Article article = getBy();
            int newQte;
            if (article != null)
            {
                do
                {
                    Console.WriteLine(article);
                    Console.WriteLine("Entrez la quantité à ajouter : ");
                    newQte = int.Parse(Console.ReadLine());
                } while (newQte <= 0);
                article.QteStock += newQte;
                articleService.modifier(article);
            }
            else
            {
                Console.WriteLine("Article introuvable");
            }
        }



        public void listerArticleDette(Dette dette)
        {
            articleService.getArticlesDette(dette).ForEach(Console.WriteLine);
        }
    }
}