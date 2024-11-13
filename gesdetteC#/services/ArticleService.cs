using gesdette.entities;
using gesdette.repository.implement;
using gesdette.services.servicespe;

namespace gesdette.services
{
    public class ArticleService : IArticleService
    {

        private IArticleRepository articleRepository;

        public ArticleService(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }


        public void create(Article value)
        {
            value.onPrePersist();
            articleRepository.insert(value);
        }


        public List<Article> findAll()
        {
            return articleRepository.selectAll();
        }


        public Article getBy(string name)
        {

            return articleRepository.selectBy(name);
        }


        public int count()
        {
            return articleRepository.count();
        }


        public void modifier(Article article)
        {
            article.onPreUpdated();
            articleRepository.update(article);
        }


        public List<Article> getArticlesDette(Dette dette)
        {
            return articleRepository.articleOfDette(dette);
        }

    }
}