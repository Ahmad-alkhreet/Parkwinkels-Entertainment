using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain;
using Service;

public class MedewerkersModel : PageModel
{
    private readonly MedewerkerService _medewerkerService;
    public MedewerkersModel(MedewerkerService medewerkerService) => _medewerkerService = medewerkerService;

    public List<Medewerker> Medewerkers { get; set; }

    [BindProperty]
    public MedewerkerInput NieuweMedewerker { get; set; }

    public async Task OnGetAsync()
    {
        Medewerkers = await _medewerkerService.GetAllMedewerkersAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _medewerkerService.AddMedewerkerAsync(NieuweMedewerker.Naam, NieuweMedewerker.Rol, NieuweMedewerker.Email);
        return RedirectToPage();
    }

    public class MedewerkerInput
    {
        public string Naam { get; set; }
        public string Rol { get; set; }
        public string Email { get; set; }
    }
}
