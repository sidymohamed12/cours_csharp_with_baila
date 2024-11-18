using Microsoft.AspNetCore.Mvc;

namespace gesDetteWebCS.Models
{
    public class Panier
    {
        public List<Detail> ArticlesPanier { get; set; } = new List<Detail>();
        public double Total => ArticlesPanier.Sum(a => a.MontantVendu);

        public void AddinPanier(Article article, int qte)
        {
            var detail = ArticlesPanier.FirstOrDefault(a => a.Article.Id == article.Id);
            if (detail == null)
            {
                ArticlesPanier.Add(new Detail
                {
                    Article = article,
                    QteVendu = qte,
                    MontantVendu = article.Prix * qte
                });
            }
            else
            {
                detail.QteVendu += qte;
                detail.MontantVendu = detail.QteVendu * article.Prix;
            }
        }
        public Panier DeleteArticle(int articleId)
        {
            var article = ArticlesPanier.FirstOrDefault(a => a.Article.Id == articleId);
            if (article != null)
            {
                ArticlesPanier.Remove(article);
            }
            return this;
        }
    }
}