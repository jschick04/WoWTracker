using Blazored.Modal;
using Microsoft.AspNetCore.Components;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.Client.Helpers;
using Tracker.Client.Shared.Dialogs.Characters;

namespace Tracker.Client.Shared;

public partial class NavMenu : IDisposable {

    private bool _dropdownActive = true;
    private bool _isLoading = true;

    [Parameter] public bool DrawerOpen { get; set; }

    [CascadingParameter] protected bool IsDarkMode { get; set; }

    private List<CharacterResponse> Characters { get; set; } = new();

    public void Dispose() {
        AppStateProvider.OnChangeAsync -= UpdateCharactersAsync;
        GC.SuppressFinalize(this);
    }

    protected override async Task OnInitializedAsync() {
        AppStateProvider.OnChangeAsync += UpdateCharactersAsync;

        await AppStateProvider.UpdateCharactersAsync();
    }

    private async Task CreateCharacter() {
        var parameters = new ModalParameters();
        var options = new ModalOptions().GetClass(IsDarkMode);

        parameters.Add(nameof(Create.ButtonText), "Create");

        var dialog = DialogService.Show<Create>("Create Character", parameters, options);

        var result = await dialog.Result;

        if (!result.Cancelled) {
            await AppStateProvider.UpdateCharactersAsync();
            NavigationManager.NavigateTo("/");
        }
    }

    private void LoadCharacter(int id) {
        NavigationManager.NavigateTo($"/character/{id}");
    }

    private void ToggleDropdown() => _dropdownActive = !_dropdownActive;

    private async Task UpdateCharactersAsync() {
        _isLoading = true;
        StateHasChanged();

        Characters = await AppStateProvider.GetCharactersAsync();

        _isLoading = false;
        StateHasChanged();
    }

}