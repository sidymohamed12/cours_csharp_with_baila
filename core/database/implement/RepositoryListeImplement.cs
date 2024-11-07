namespace gesdette.core.database.implement
{
    public class RepositoryListeImplement<T> : RepositoryListe<T>
    {

        protected List<T> listes = [];


        public void insert(T value)
        {
            listes.Add(value);
        }


        public List<T> selectAll()
        {
            return listes;
        }


        public int count()
        {
            return listes.Count;
        }

    }
}