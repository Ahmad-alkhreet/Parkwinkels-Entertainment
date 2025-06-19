using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain;
using Service;

public class NieuwsModel : PageModel
{
    private readonly NieuwsberichtService _nieuwsService;

    public NieuwsModel(NieuwsberichtService nieuwsService)
    {
        _nieuwsService = nieuwsService;
    }

    public List<Nieuwsbericht> Berichten { get; set; }

    [BindProperty]
    public NieuwsInput NieuwNieuws { get; set; }

    public async Task OnGetAsync()
    {
        Berichten = await _nieuwsService.GetAllNieuwsAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var nieuwsbericht = new Nieuwsbericht(
            0,
            NieuwNieuws.Titel,
            NieuwNieuws.Inhoud,
            DateTime.Now
        );

        await _nieuwsService.AddNieuwsberichtAsync(nieuwsbericht);
        return RedirectToPage();
    }

    public class NieuwsInput
    {
        public string Titel { get; set; }
        public string Inhoud { get; set; }
    }
}
