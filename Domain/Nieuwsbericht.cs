
namespace Domain
{
    public class Nieuwsbericht
    {
        public int NieuwsID { get; private set; }
        public string Titel { get; private set; }
        public string Inhoud { get; private set; }
        public DateTime Publicatiedatum { get; private set; }

        public Nieuwsbericht(int id, string titel, string inhoud, DateTime publicatiedatum)
        {


            if (string.IsNullOrWhiteSpace(titel))
                throw new ArgumentException("Titel mag niet leeg zijn.");
            if (string.IsNullOrWhiteSpace(inhoud))
                throw new ArgumentException("Inhoud mag niet leeg zijn.");


            NieuwsID = id;
            Titel = titel;
            Inhoud = inhoud;
            Publicatiedatum = publicatiedatum;
        }

        //public int GetNieuwsID() => NieuwsID;
        //public string GetTitel() => Titel;
        //public string GetInhoud() => Inhoud;
        //public DateTime GetPublicatiedatum() => Publicatiedatum;
    }
}
