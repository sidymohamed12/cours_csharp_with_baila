namespace gesdette.core
{
    public abstract class ViewImplement<T> : View<T>
    {


        public void afficher(List<T> listes)
        {
            foreach (var t in listes)
            {
                Console.WriteLine(t);
            }
        }



        public bool askToContinue()
        {
            int choix;
            {
                do
                {
                    Console.WriteLine("1- Oui ");
                    Console.WriteLine("2- Non");
                    choix = Convert.ToInt32(Console.ReadLine());
                } while (choix < 1 || choix > 2);
                return choix == 1;
            }
        }


        public T getBy()
        {
            throw new NotImplementedException();
        }

        public T saisie()
        {
            throw new NotImplementedException();
        }
    }
}