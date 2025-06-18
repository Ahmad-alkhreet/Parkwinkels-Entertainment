
namespace Domain
{
    public class Product
    {
        public int ProductID { get; private set; }
        public string Naam { get; private set; }
        public string Categorie { get; private set; }
        public int VoorraadAantal { get; private set; }
        public int MinimaleVoorraad { get; private set; }
        public decimal Prijs { get; private set; }

        public Product(int id, string naam, string categorie, int voorraadAantal, int minimaleVoorraad, decimal prijs)
        {
            if (voorraadAantal < 0 || minimaleVoorraad < 0)
                throw new ArgumentException("Voorraadwaarden mogen niet negatief zijn.");
            if (prijs < 0)
                throw new ArgumentException("Prijs mag niet negatief zijn.");

            ProductID = id;
            Naam = naam;
            Categorie = categorie;
            VoorraadAantal = voorraadAantal;
            MinimaleVoorraad = minimaleVoorraad;
            Prijs = prijs;
        }

        public void UpdateVoorraad(int nieuwAantal)
        {
            if (nieuwAantal < 0)
                throw new ArgumentException("Voorraad mag niet negatief zijn.");
            VoorraadAantal = nieuwAantal;
        }
    }

    //public int GetProductID() => ProductID;
    //public string GetNaam() => Naam;
    //public string GetCategorie() => Categorie;
    //public int GetVoorraadAantal() => VoorraadAantal;
    //public int GetMinimaleVoorraad() => MinimaleVoorraad;
    //public decimal GetPrijs() => Prijs;

    //public void UpdateVoorraad(int nieuwAantal)
    //{
    //    VoorraadAantal = nieuwAantal;
    //}
}


