using gesdette.core;
using gesdette.entities;

namespace gesdette.views.viewspe
{
    public interface IArticleView : View<Article>
    {
        void listerArticleDispo();
        void updateQteArticle();
        void listerArticleDette(Dette dette);
    }
}