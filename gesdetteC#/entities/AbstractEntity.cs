using System.ComponentModel.DataAnnotations.Schema;

namespace gesdette.entities
{
    public class AbstractEntity
    {


        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [NotMapped]
        public User? CreatedBy { get; set; }
        [NotMapped]
        public User? UpdatedBy { get; set; }

        public void onPrePersist()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            CreatedBy = UserConnect.UserConnecte;
            UpdatedBy = UserConnect.UserConnecte;
        }

        public void onPreUpdated()
        {
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = UserConnect.UserConnecte;
        }

    }
}