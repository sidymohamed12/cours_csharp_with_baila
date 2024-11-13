using gesdette.core.database.implement;
using gesdette.entities;
using gesdette.repository.implement;

namespace gesdette.repository.list
{
    public class ArticleRepositoryList : RepositoryListeImplement<Article>, IArticleRepository
    {
        public List<Article> articleOfDette(Dette dette)
        {
            if (dette.Details == null) return [];
            return dette.Details
                    .Where(detail => detail?.Dette?.Id == dette.Id)
                    .Select(detail => detail.Article)
                    .Where(article => article != null)
                    .ToList()!;
        }


        public Article? selectBy(string name)
        {
            return listes.FirstOrDefault(art => art?.Reference?.Equals(name, StringComparison.Ordinal) == true);
        }

        public Article? selectById(int id)
        {
            return listes.FirstOrDefault(art => art.Id == id);
        }

        public void update(Article article)
        {
            var existingArticle = listes.FirstOrDefault(art => art?.Reference?.Equals(article.Reference) == true);
            if (existingArticle != null)
            {
                existingArticle.Reference = article.Reference;
                existingArticle.Libelle = article.Libelle;
                existingArticle.Prix = article.Prix;
                existingArticle.QteStock = article.QteStock;
                existingArticle.Details = article.Details;
                existingArticle.onPreUpdated();
            }
        }

    }
}