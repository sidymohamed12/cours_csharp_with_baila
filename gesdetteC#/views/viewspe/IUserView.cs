using gesdette.core;
using gesdette.entities;
using gesdette.enums;

namespace gesdette.views.viewspe
{
    public interface IUserView : View<User>
    {
        Role saisieRoleUser();
        void setEtatUser(bool status);
        User? authentification();
        void listerUserbyRole();
        void listerUserActif();
    }
}