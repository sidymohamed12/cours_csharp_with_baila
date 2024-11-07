
using gesdette.entities;
using gesdette.services.servicespe;
using gesdette.views;

namespace gesdette.core.factory.Impl
{
    public class FactoryView<T> : IFactoryView<T>
    {
        private readonly View<T> view;

        public FactoryView(Type clazz,
                           IUserService? userService,
                           IClientService? clientService,
                           IArticleService? articleService,
                           IDetteService? detteService,
                           IDetailService? detailService,
                           IPayementService? payementService)
        {
            if (typeof(Client).IsAssignableFrom(clazz))
            {
                view = (View<T>)new ClientView(clientService, userService);
            }
            else if (typeof(User).IsAssignableFrom(clazz))
            {
                view = (View<T>)new UserView(userService);
            }
            else if (typeof(Article).IsAssignableFrom(clazz))
            {
                view = (View<T>)new ArticleView(articleService);
            }
            else if (typeof(Dette).IsAssignableFrom(clazz))
            {
                view = (View<T>)new DetteView(articleService, detailService, detteService);
            }
            else if (typeof(Payement).IsAssignableFrom(clazz))
            {
                view = (View<T>)new PayementView(payementService, detteService);
            }
        }


        public View<T> createView()
        {
            return view;
        }

    }
}