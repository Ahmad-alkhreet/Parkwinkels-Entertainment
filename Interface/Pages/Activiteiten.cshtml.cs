using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain;
using Service;
using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

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
        if (!ModelState.IsValid) return Page();

        try
        {
            var activiteit = new Activiteit(
                0,
                NieuweActiviteit.Naam,
                NieuweActiviteit.Beschrijving,
                NieuweActiviteit.Locatie,
                NieuweActiviteit.Starttijd,
                NieuweActiviteit.Eindtijd,
                NieuweActiviteit.MaxDeelnemers
            );

            await _activiteitService.AddActiviteitAsync(activiteit);
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return Page();
        }

        return RedirectToPage();
    }


    public class ActiviteitInput
    {
        [Required]
        public string Naam { get; set; }

        [Required]
        public string Beschrijving { get; set; }

        [Required]
        public string Locatie { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Starttijd { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Eindtijd { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Aantal deelnemers moet groter zijn dan 0")]
        public int MaxDeelnemers { get; set; }
    }
}
