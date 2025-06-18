
namespace Domain
{
    public class Medewerker
    {
        public  int MedewerkerID { get; private set; }
        public string Naam { get; private set; }
        public string Rol { get; private set; }
        public string Email { get; private set; }

        public Medewerker(int id, string naam, string rol, string email)
        {

            if (string.IsNullOrWhiteSpace(naam))
                throw new ArgumentException("Naam mag niet leeg zijn.");
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email mag niet leeg zijn.");


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
