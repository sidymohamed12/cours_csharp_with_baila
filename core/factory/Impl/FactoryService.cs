
using gesdette.entities;
using gesdette.repository.implement;
using gesdette.services;

namespace gesdette.core.factory.Impl
{
    public class FactoryService<T> : IFactoryService<T>
    {
        private readonly Service<T> service;

        public FactoryService(
            Type clazz,
            Repository<T> repository)
        {
            if (typeof(Client).IsAssignableFrom(clazz))
            {
                service = (Service<T>)new ClientService((IClientRepository)repository);
            }
            else if (typeof(User).IsAssignableFrom(clazz))
            {
                service = (Service<T>)new UserService((IUserRepository)repository);
            }
            else if (typeof(Article).IsAssignableFrom(clazz))
            {
                service = (Service<T>)new ArticleService((IArticleRepository)repository);
            }
            else if (typeof(Dette).IsAssignableFrom(clazz))
            {
                service = (Service<T>)new DetteService((IDetteRepository)repository);
            }
            else if (typeof(Detail).IsAssignableFrom(clazz))
            {
                service = (Service<T>)new DetailService((IDetailRepository)repository);
            }
            else if (typeof(Payement).IsAssignableFrom(clazz))
            {
                service = (Service<T>)new PayementService((IPayementRepository)repository);
            }
        }

        public Service<T> createService()
        {
            return service;
        }
    }
}