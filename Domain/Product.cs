namespace Domain
{
    public class Product
    {
        private int ProductID { get; set; }
        private string Naam { get; set; }
        private string Categorie { get; set; }
        private int VoorraadAantal { get; set; }
        private int MinimaleVoorraad { get; set; }
        private decimal Prijs { get; set; }

        public Product(int id, string naam, string categorie, int voorraadAantal, int minimaleVoorraad, decimal prijs)
        {
            ProductID = id;
            Naam = naam;
            Categorie = categorie;
            VoorraadAantal = voorraadAantal;
            MinimaleVoorraad = minimaleVoorraad;
            Prijs = prijs;
        }

        public int GetProductID() => ProductID;
        public string GetNaam() => Naam;
        public string GetCategorie() => Categorie;
        public int GetVoorraadAantal() => VoorraadAantal;
        public int GetMinimaleVoorraad() => MinimaleVoorraad;
        public decimal GetPrijs() => Prijs;

        public void UpdateVoorraad(int nieuwAantal)
        {
            VoorraadAantal = nieuwAantal;
        }
    }
}
