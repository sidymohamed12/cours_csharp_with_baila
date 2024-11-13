using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using gesDetteWebCS.Models.enums;

namespace gesDetteWebCS.Models

{
    public class User : AbstractEntity
    {
        [Required]
        public string? Login { get; set; }
        [MaxLength(60)]
        [Required]
        public string? Password { get; set; }
        [Required]
        public Role Role { get; set; }
        [Required]
        public bool Etat { get; set; }
        [NotMapped]
        [Required]
        public Client? Client { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is User user &&
                   Password == user.Password;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Password);
        }

        public override string ToString()
        {
            return $"Login: {Login}, " +
                   $"Password: {Password}, " +
                   $"Role: {Role}, ";
        }
    }
}