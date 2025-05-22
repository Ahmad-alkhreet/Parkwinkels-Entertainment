namespace Domain
{
    public class Activiteit
    {
        private int ActiviteitID { get; set; }
        private string Naam { get; set; }
        private string Beschrijving { get; set; }
        private string Locatie { get; set; }
        private DateTime Starttijd { get; set; }
        private DateTime Eindtijd { get; set; }
        private int MaxDeelnemers { get; set; }

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

        public int GetActiviteitID() => ActiviteitID;
        public string GetNaam() => Naam;
        public string GetBeschrijving() => Beschrijving;
        public string GetLocatie() => Locatie;
        public DateTime GetStarttijd() => Starttijd;
        public DateTime GetEindtijd() => Eindtijd;
        public int GetMaxDeelnemers() => MaxDeelnemers;
    }
}
