using gesdette.entities;
using gesdette.repository.implement;
using gesdette.repository.list;

namespace gesdette.core.factory.Impl
{
    public class FactoryRepo<T> : IFactoryRepo<T>
    {
        private readonly Repository<T> repositorie;

        public FactoryRepo(Type clazz)
        {

            if (typeof(Client).IsAssignableFrom(clazz))
            {
                repositorie = (Repository<T>)new ClientRepositoryListe();
            }
            else if (typeof(User).IsAssignableFrom(clazz))
            {
                repositorie = (Repository<T>)new UserRepositoryListe();
            }
            else if (typeof(Article).IsAssignableFrom(clazz))
            {
                repositorie = (Repository<T>)new ArticleRepositoryList();
            }
            else if (typeof(Dette).IsAssignableFrom(clazz))
            {
                repositorie = (Repository<T>)new DetteRepositoryList();
            }
            else if (typeof(Detail).IsAssignableFrom(clazz))
            {
                repositorie = (Repository<T>)new DetailRepositoryList();
            }
            else if (typeof(Payement).IsAssignableFrom(clazz))
            {
                repositorie = (Repository<T>)new PayementRepositoryList();
            }
        }


        public Repository<T> createRepository()
        {
            return repositorie;
        }

    }
}