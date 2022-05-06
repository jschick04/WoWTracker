using Tracker.Api.Contracts.V1.Responses;
using Tracker.Library.Helpers;

namespace Tracker.Client.Pages;

public partial class VerifyAuth : IDisposable {

    private bool _isLoading;

    private List<CharacterResponse> Characters1 { get; set; } = new();

    private List<CharacterResponse> Characters2 { get; set; } = new();

    private string CurrentUsername { get; set; } = "";

    public void Dispose() {
        AppStateProvider.OnChangeAsync -= UpdateCharactersAsync;
        GC.SuppressFinalize(this);
    }

    protected override async Task OnInitializedAsync() {
        var user = await StateProvider.GetAuthenticationStateProviderUserAsync();

        AppStateProvider.OnChangeAsync += UpdateCharactersAsync;

        if (user.Identity?.IsAuthenticated is true) {
            CurrentUsername = user.GetUsername();

            await UpdateCharactersAsync();
        }
    }

    private static string GetProfession(string? profession) =>
        string.IsNullOrWhiteSpace(profession) ? "None" : profession;

    private async Task UpdateCharactersAsync() {
        _isLoading = true;
        StateHasChanged();

        Characters1 = await AppStateProvider.GetCharactersAsync();
        Characters2 = await AppStateProvider.GetCharactersAsync();

        _isLoading = false;
        StateHasChanged();
    }

}