
namespace Domain
{
    public class Nieuwsbericht
    {
        public int NieuwsID { get;  }
        public string Titel { get;  }
        public string Inhoud { get; }
        public DateTime Publicatiedatum { get; }

        public Nieuwsbericht(int id, string titel, string inhoud, DateTime publicatiedatum)
        {
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
