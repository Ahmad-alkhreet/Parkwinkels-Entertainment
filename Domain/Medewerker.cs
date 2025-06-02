
namespace Domain
{
    public class Medewerker
    {
        public  int MedewerkerID { get;  }
        public string Naam { get; }
        public string Rol { get;  }
        public string Email { get;  }

        public Medewerker(int id, string naam, string rol, string email)
        {
            MedewerkerID = id;
            Naam = naam;
            Rol = rol;
            Email = email;
        }

        //public int GetMedewerkerID() => MedewerkerID;
        //public string GetNaam() => Naam;
        //public string GetRol() => Rol;
        //public string GetEmail() => Email;
    }
}
