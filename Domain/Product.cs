
namespace Domain
{
    public class Product
    {
        public int ProductID { get;  }
        public string Naam { get;  }
        public string Categorie { get;  }
        public int VoorraadAantal { get;  }
        public int MinimaleVoorraad { get; }
        public decimal Prijs { get; }

        public Product(int id, string naam, string categorie, int voorraadAantal, int minimaleVoorraad, decimal prijs)
        {
            ProductID = id;
            Naam = naam;
            Categorie = categorie;
            VoorraadAantal = voorraadAantal;
            MinimaleVoorraad = minimaleVoorraad;
            Prijs = prijs;
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
}


