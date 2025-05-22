using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain;
using Service;

public class BewerkModel : PageModel
{
    private readonly ProductService _productService;

    public BewerkModel(ProductService productService)
    {
        _productService = productService;
    }

    [BindProperty]
    public int ProductId { get; set; }
    [BindProperty]
    public string Naam { get; set; }
    [BindProperty]
    public string Categorie { get; set; }
    [BindProperty]
    public int VoorraadAantal { get; set; }
    [BindProperty]
    public int MinimaleVoorraad { get; set; }
    [BindProperty]
    public decimal Prijs { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var producten = await _productService.GetAllProductsAsync();
        var p = producten.FirstOrDefault(p => p.GetProductID() == id);

        if (p == null) return RedirectToPage("/Producten");

        ProductId = id;
        Naam = p.GetNaam();
        Categorie = p.GetCategorie();
        VoorraadAantal = p.GetVoorraadAantal();
        MinimaleVoorraad = p.GetMinimaleVoorraad();
        Prijs = p.GetPrijs();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _productService.UpdateVoorraadAsync(ProductId, VoorraadAantal);
        // Je kunt hier ook een UpdateProductAsync maken met alle velden
        return RedirectToPage("/Producten");
    }
}
