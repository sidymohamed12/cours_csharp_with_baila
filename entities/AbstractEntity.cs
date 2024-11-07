namespace gesdette.entities
{
    public class AbstractEntity
    {

        protected int id;
        protected DateTime createdAt;
        protected DateTime updatedAt;
        private User? createdBy;
        protected User? updatedBy;

        public int Id
        {
            get => id;
            set => id = value;
        }

        public DateTime CreatedAt
        {
            get => createdAt;
            set => createdAt = value;
        }

        public DateTime UpdatedAt
        {
            get => updatedAt;
            set => updatedAt = value;
        }

        public User? CreatedBy
        {
            get => createdBy;
            set => createdBy = value;
        }

        public User? UpdatedBy
        {
            get => updatedBy;
            set => updatedBy = value;
        }

        public void onPrePersist()
        {
            createdAt = DateTime.Now;
            updatedAt = DateTime.Now;
            createdBy = UserConnect.UserConnecte;
            UpdatedBy = UserConnect.UserConnecte;
        }

        public void onPreUpdated()
        {
            updatedAt = DateTime.Now;
            updatedBy = UserConnect.UserConnecte;
        }

    }
}