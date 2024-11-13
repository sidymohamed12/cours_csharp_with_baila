using gesdette.core;
using gesdette.entities;
using gesdette.enums;
using gesdette.services.servicespe;
using gesdette.views.viewspe;

namespace gesdette.views
{
    public class PayementView(IPayementService payementService, IDetteService detteService) : ViewImplement<Payement>, IPayementView
    {

        private IDetteService detteService = detteService;
        private IPayementService payementService = payementService;

        public new Payement saisie()
        {

            detteService.findAll()
                    .Where(dette => dette.EtatD == Etat.accepter && !dette.MontantRestant.Equals(0.0))
                    .ToList()
                    .ForEach(Console.WriteLine);
            Console.WriteLine("choisissez la dette à payer par id");

            Dette dette = detteService.getById(Convert.ToInt32(Console.ReadLine()));

            if (dette != null && dette.EtatD.Equals(Etat.accepter)
                    && !dette.Montant.Equals(dette.MontantVerser))
            {

                double montantRestant = dette.Montant - dette.MontantVerser;
                Payement payement = new()
                {
                    Date = DateTime.Now
                };
                Console.WriteLine("Montant Restant : " + montantRestant);
                double verser;
                do
                {
                    Console.WriteLine("entrez le montant à payer : ");
                    verser = Convert.ToDouble(Console.ReadLine());
                } while (verser <= 0 || verser > montantRestant);

                payement.Montant = verser;

                dette.addPayement(payement);
                dette.MontantVerser = dette.MontantVerser + verser;

                if (dette.MontantVerser == dette.Montant || dette.MontantRestant == 0)
                {
                    dette.Archiver = true;
                }

                detteService.modifier(dette);

                payement.Dette = dette;
                return payement;
            }
            else
            {
                Console.WriteLine("dette non accepté/trouvé");
            }
            return null;
        }


        public new Payement getBy()
        {
            throw new NotSupportedException("Method 'getBy' is not supported.");
        }



        public void listePayementsDette(Dette dette)
        {
            payementService.getPayementsDette(dette).ForEach(payement => Console.WriteLine(payement));
        }


    }
}