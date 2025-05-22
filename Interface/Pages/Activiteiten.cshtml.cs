using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain;
using Service;

public class ActiviteitenModel : PageModel
{
    private readonly ActiviteitService _activiteitService;
    public ActiviteitenModel(ActiviteitService activiteitService) => _activiteitService = activiteitService;

    public List<Activiteit> Activiteiten { get; set; }

    [BindProperty]
    public ActiviteitInput NieuweActiviteit { get; set; }

    public async Task OnGetAsync()
    {
        Activiteiten = await _activiteitService.GetAllActiviteitenAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _activiteitService.AddActiviteitAsync(
            NieuweActiviteit.Naam,
            NieuweActiviteit.Beschrijving,
            NieuweActiviteit.Locatie,
            NieuweActiviteit.Starttijd,
            NieuweActiviteit.Eindtijd,
            NieuweActiviteit.MaxDeelnemers
        );
        return RedirectToPage();
    }

    public class ActiviteitInput
    {
        public string Naam { get; set; }
        public string Beschrijving { get; set; }
        public string Locatie { get; set; }
        public DateTime Starttijd { get; set; }
        public DateTime Eindtijd { get; set; }
        public int MaxDeelnemers { get; set; }
    }
}
