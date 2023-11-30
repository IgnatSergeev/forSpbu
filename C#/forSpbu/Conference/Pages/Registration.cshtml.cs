using Microsoft.AspNetCore.Mvc.RazorPages;
using Conference.Data;

namespace Conference.Pages;

[BindProperties]
public class RegistrationModel(ConferenceRegistrationDbContext context) : PageModel
{
    public Participant Participant { get; set; } = new();

    public async Task<IActionResult> OnPostAsync()
    {
        context.Participants.Add(Participant);
        await context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}