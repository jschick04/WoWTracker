using Blazored.Modal;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Tracker.Client.Helpers;
using Tracker.Client.Library.Store.State;
using Tracker.Client.Shared.Dialogs.Characters;

namespace Tracker.Client.Shared;

public partial class NavMenu : IDisposable
{
    private bool _dropdownActive = true;

    [CascadingParameter] protected bool IsDarkMode { get; set; }

    [Inject] private IState<NavMenuState> NavMenuState { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        if (CharacterState.Value.Characters is null)
        {
            CharacterStateProvider.GetAllCharacters();
        }

        if (CharacterState.Value.HasCurrentErrors)
        {
            ToastService.ShowError(CharacterState.Value.CurrentErrorMessage);
        }
    }

    private async Task CreateCharacter()
    {
        var parameters = new ModalParameters();
        var options = new ModalOptions().GetClass(IsDarkMode);

        parameters.Add(nameof(Create.ButtonText), "Create");

        var dialog = DialogService.Show<Create>("Create Character", parameters, options);

        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            //await AppStateProvider.UpdateCharactersAsync();
            //NavigationManager.NavigateTo("/");
        }
    }

    private void LoadCharacter(int id) { NavigationManager.NavigateTo($"/character/{id}"); }

    private void ToggleDropdown() => _dropdownActive = !_dropdownActive;
}
