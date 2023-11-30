using Microsoft.AspNetCore.Mvc.RazorPages;
using Practice.Data;

namespace Practice.Pages;

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