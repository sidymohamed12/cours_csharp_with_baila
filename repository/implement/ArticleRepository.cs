using gesdette.core;
using gesdette.entities;

namespace gesdette.repository.implement
{
    public interface IArticleRepository : Repository<Article>
    {
        Article selectBy(string name);

        Article selectById(int id);

        void update(Article client);

        List<Article> articleOfDette(Dette dette);
    }
}