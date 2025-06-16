
namespace Domain
{
    public class Activiteit

    {
        public int ActiviteitID { get; private set; }
        public string Naam { get; private set; }
        public string Beschrijving { get; private set; }
        public string Locatie { get; private set; }
        public DateTime Starttijd { get; private set; }
        public DateTime Eindtijd { get; private set; }
        public int MaxDeelnemers { get; private set; }

        public Activiteit(int id, string naam, string beschrijving, string locatie, DateTime starttijd, DateTime eindtijd, int maxDeelnemers)
        {
            ActiviteitID = id;
            Naam = naam;
            Beschrijving = beschrijving;
            Locatie = locatie;
            Starttijd = starttijd;
            Eindtijd = eindtijd;
            MaxDeelnemers = maxDeelnemers;
        }

        //public int GetActiviteitID() => ActiviteitID;
        //public string GetNaam() => Naam;
        //public string GetBeschrijving() => Beschrijving;
        //public string GetLocatie() => Locatie;
        //public DateTime GetStarttijd() => Starttijd;
        //public DateTime GetEindtijd() => Eindtijd;
        //public int GetMaxDeelnemers() => MaxDeelnemers;
    }
}
