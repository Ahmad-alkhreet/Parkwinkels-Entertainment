namespace Domain
{
    public class Nieuwsbericht
    {
        private int NieuwsID { get; set; }
        private string Titel { get; set; }
        private string Inhoud { get; set; }
        private DateTime Publicatiedatum { get; set; }

        public Nieuwsbericht(int id, string titel, string inhoud, DateTime publicatiedatum)
        {
            NieuwsID = id;
            Titel = titel;
            Inhoud = inhoud;
            Publicatiedatum = publicatiedatum;
        }

        public int GetNieuwsID() => NieuwsID;
        public string GetTitel() => Titel;
        public string GetInhoud() => Inhoud;
        public DateTime GetPublicatiedatum() => Publicatiedatum;
    }
}
