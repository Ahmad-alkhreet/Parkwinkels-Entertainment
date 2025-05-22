using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain;
using Service;

public class ProductenModel : PageModel
{
    private readonly ProductService _productService;

    public ProductenModel(ProductService productService)
    {
        _productService = productService;
    }

    [BindProperty]
    public ProductInputModel NieuwProduct { get; set; }

    public List<Product> Producten { get; set; }

    public async Task OnGetAsync()
    {
        Producten = await _productService.GetAllProductsAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        await _productService.AddProductAsync(
            NieuwProduct.Naam,
            NieuwProduct.Categorie,
            NieuwProduct.VoorraadAantal,
            NieuwProduct.MinimaleVoorraad,
            NieuwProduct.Prijs
        );

        return RedirectToPage(); // Refresh met nieuwe data
    }
    public async Task<IActionResult> OnPostVerwijderAsync(int id)
    {
        await _productService.DeleteProductAsync(id);
        return RedirectToPage();
    }


    public class ProductInputModel
    {
        public string Naam { get; set; }
        public string Categorie { get; set; }
        public int VoorraadAantal { get; set; }
        public int MinimaleVoorraad { get; set; }
        public decimal Prijs { get; set; }
    }
}
