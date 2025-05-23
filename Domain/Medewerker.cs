

namespace Domain
{
    public class Medewerker
    {
        private int MedewerkerID { get; set; }
        private string Naam { get; set; }
        private string Rol { get; set; }
        private string Email { get; set; }

        public Medewerker(int id, string naam, string rol, string email)
        {
            MedewerkerID = id;
            Naam = naam;
            Rol = rol;
            Email = email;
        }

        public int GetMedewerkerID() => MedewerkerID;
        public string GetNaam() => Naam;
        public string GetRol() => Rol;
        public string GetEmail() => Email;
    }
}
