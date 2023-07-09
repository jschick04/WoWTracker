using Fluxor;
using Microsoft.AspNetCore.Components;
using Tracker.Client.Library.Features.State;
using Tracker.Library.Helpers;

namespace Tracker.UI.Pages;

public partial class VerifyAuth
{
    [Inject] private IState<CharacterState> Characters1 { get; set; } = null!;

    [Inject] private IState<CharacterState> Characters2 { get; set; } = null!;

    private string CurrentUsername { get; set; } = "";

    protected override async Task OnInitializedAsync()
    {
        var user = await ClientAuthStateProvider.GetAuthenticationStateProviderUserAsync();

        if (user.Identity?.IsAuthenticated is true)
        {
            CurrentUsername = user.GetUsername();
        }
    }

    private static string GetProfession(string profession) =>
        string.IsNullOrWhiteSpace(profession) ? "None" : profession;
}
