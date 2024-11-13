using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using gesdette.enums;

namespace gesdette.entities
{
    public class User : AbstractEntity
    {

        public string? Login { get; set; }
        [MaxLength(60)]
        public string? Password { get; set; }

        public Role Role { get; set; }

        public bool Etat { get; set; }
        [NotMapped]
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