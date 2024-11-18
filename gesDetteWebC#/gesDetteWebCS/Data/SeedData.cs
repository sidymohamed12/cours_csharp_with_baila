using gesDetteWebCS.Models;
using gesDetteWebCS.Models.enums;

namespace gesDetteWebCS.Data
{
    public static class SeedData
    {

        public static void Initialize(IServiceProvider serviceProvider, ApplicationDbContext context)
        {
            if (context.Clients.Any())
            {
                Console.WriteLine("Les clients existent déjà dans la base de données.");
                return;
            }

            for (int i = 0; i < 10; i++)
            {

                var article = new Article
                {
                    Reference = "ref" + i,
                    Libelle = "libelle" + i,
                    QteStock = new Random().Next(0, 101),
                    Prix = new Random().Next(10, 101) * 100,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };


                var client = new Client
                {
                    Telephone = "77123456" + i,
                    Surnom = "surnom" + i,
                    Adresse = "adresse" + i,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                client.Dettes = new List<Dette>();

                if (i % 2 == 0)
                {
                    var user = new User
                    {
                        Login = "login" + i,
                        Password = "password",
                        Role = (i % 4 == 0) ? Role.client : Role.boutiquier,
                        Etat = true,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };

                    if (user != null)
                    {
                        client.User = user;
                    }

                    // Création des dettes pour les clients avec utilisateur
                    for (int j = 1; j < 3; j++)
                    {
                        var dette = new Dette
                        {
                            Montant = 1500 * j,
                            MontantVerser = 1500 * j,
                            ClientD = client,
                            EtatD = Etat.encours,
                            Date = DateTime.UtcNow,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        };
                        client.Dettes.Add(dette);
                        context.Dettes.Add(dette);
                    }
                }
                else
                {

                    for (int j = 1; j < 3; j++)
                    {
                        var dette = new Dette
                        {
                            Montant = 1500 * j,
                            MontantVerser = 1500,
                            ClientD = client,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        };
                        client.Dettes.Add(dette);
                        context.Dettes.Add(dette);
                    }


                }

                context.Clients.Add(client);
                context.Articles.Add(article);

            }

            context.SaveChanges();
        }

    }
}