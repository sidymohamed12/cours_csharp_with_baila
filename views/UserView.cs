using gesdette.core;
using gesdette.entities;
using gesdette.enums;
using gesdette.services.servicespe;
using gesdette.views.viewspe;

namespace gesdette.views
{
    public class UserView : ViewImplement<User>, IUserView
    {
        private IUserService userService;

        public UserView(IUserService userService)
        {
            this.userService = userService;
        }

        public new User saisie()
        {
            User user = new();
            int choix;

            Console.WriteLine("entrer le login : ");
            user.Login = Console.ReadLine();

            Console.WriteLine("entrer le mdp : ");
            user.Password = Console.ReadLine();

            do
            {
                Console.WriteLine("1- admin");
                Console.WriteLine("2- boutiquier");
                choix = int.Parse(Console.ReadLine());
            } while (choix <= 0 || choix > 2);

            user.Role = (Role)(choix - 1);
            user.Etat = true;
            return user;
        }



        public Role saisieRoleUser()
        {
            int choix;
            do
            {
                Console.WriteLine("veuillez selectionner le role");
                foreach (Role role in Enum.GetValues(typeof(Role)))
                {
                    Console.WriteLine((int)role + 1 + "-" + role.ToString());
                }
                choix = int.Parse(Console.ReadLine());
            } while (choix <= 0 || choix > Enum.GetValues(typeof(Role)).Length);

            return (Role)(choix - 1);
        }




        public User? getBy()
        {
            userService.findAll().ForEach(Console.WriteLine);
            Console.WriteLine("Entrez le login du compte user à activer : ");
            string login = Console.ReadLine();
            User user = userService.getBy(login);
            if (user != null)
            {
                return user;
            }
            else
            {
                Console.WriteLine("User not found");
                return null;
            }

        }


        public void setEtatUser(Boolean status)
        {
            User user = getBy();
            if (user != null)
            {
                if (status)
                {
                    if (!user.Etat)
                    {
                        user.Etat = true;
                        user.onPreUpdated();
                        userService.modifier(user);
                    }
                    else
                    {
                        Console.WriteLine("User déjà activé");
                    }
                }
                else
                {
                    if (user.Etat)
                    {
                        user.Etat = false;
                        user.onPreUpdated();
                        userService.modifier(user);
                    }
                    else
                    {
                        Console.WriteLine("User déjà désactivé");
                    }
                }
            }
            else
            {
                Console.WriteLine("updating failed");
            }

        }


        public User authentification()
        {

            Console.WriteLine("Entrez le login du compte : ");
            string login = Console.ReadLine();

            Console.WriteLine("entrez le mdp : ");
            string password = Console.ReadLine();

            User user = userService.connexion(login, password);

            if (user != null)
            {
                Console.WriteLine("Authentification réussie ! ");
                return user;
            }
            else
            {
                Console.WriteLine("Échec de l'authentification. Vérifiez votre login et mot de passe.");
                return null;
            }
        }



        public void listerUserbyRole()
        {
            Role role = saisieRoleUser();
            userService.findAll()
                .Where(user => user.Role == role)
                .ToList()
                .ForEach(Console.WriteLine);
        }



        public void listerUserActif()
        {
            userService.findAll()
                .Where(user => user.Etat)
                .ToList()
                .ForEach(Console.WriteLine);
        }

    }
}