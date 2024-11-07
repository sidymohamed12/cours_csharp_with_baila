using gesdette.core;
using gesdette.entities;

namespace gesdette.services.servicespe
{
    public interface IArticleService : Service<Article>
    {
        void modifier(Article article);
        List<Article> getArticlesDette(Dette dette);
    }
}