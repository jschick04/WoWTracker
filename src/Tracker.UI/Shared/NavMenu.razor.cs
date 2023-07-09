using Blazored.Modal;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Tracker.UI.Helpers;
using Tracker.Client.Library.Features.State;
using Tracker.UI.Shared.Dialogs.Characters;

namespace Tracker.UI.Shared;

public partial class NavMenu : IDisposable
{
    private bool _dropdownActive = true;

    [CascadingParameter] protected bool IsDarkMode { get; set; }

    [Inject] private IState<NavMenuState> NavMenuState { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        if (CharacterState.Value.Characters.Any() is not true)
        {
            CharacterStateProvider.GetAllCharacters();
        }

        if (CharacterState.Value.HasCurrentErrors)
        {
            ToastService.ShowError(CharacterState.Value.CurrentErrorMessage);
        }
    }

    private void CreateCharacter()
    {
        var parameters = new ModalParameters();
        var options = new ModalOptions().GetClass(IsDarkMode);

        parameters.Add(nameof(Create.ButtonText), "Create");

        DialogService.Show<Create>("Create Character", parameters, options);
    }

    private void LoadCharacter(int id) { NavigationManager.NavigateTo($"/character/{id}"); }

    private void ToggleDropdown() => _dropdownActive = !_dropdownActive;
}
