
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gesDetteWebCS.Models

{
    public class Client : AbstractEntity
    {
        [Required]
        public string? Surnom { get; set; }
        [Required]
        public string? Telephone { get; set; }
        [Required]
        public string? Adresse { get; set; }
        [Required]
        public User? User { get; set; }
        [NotMapped]
        public List<Dette>? Dettes { get; set; } = [];

        public void AddDettes(Dette dette)
        {
            Dettes?.Add(dette);
        }

        public override bool Equals(object? obj)
        {
            return obj is Client client &&
                   Surnom == client.Surnom;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Surnom);
        }

        public override string ToString()
        {
            return $"Surnom: {Surnom}, " +
                   $"Téléphone: {Telephone}, " +
                   $"Adresse: {Adresse}, " +
                   $"User: {(User != null ? User.ToString() : "NULL")}";
        }
    }
}