using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain;
using Service;
using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

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
        [Required(ErrorMessage = "Naam is verplicht")]
        public string Naam { get; set; }

        [Required(ErrorMessage = "Categorie is verplicht")]
        public string Categorie { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Voorraad moet 0 of meer zijn")]
        public int VoorraadAantal { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Minimale voorraad moet 0 of meer zijn")]
        public int MinimaleVoorraad { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "Prijs moet positief zijn")]
        public decimal Prijs { get; set; }
    }

}
