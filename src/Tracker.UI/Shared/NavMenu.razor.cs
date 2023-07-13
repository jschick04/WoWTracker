using Blazored.Modal;
using Blazored.Modal.Services;
using Blazored.Toast.Services;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Tracker.UI.Helpers;
using Tracker.UI.Library.Features.State;
using Tracker.UI.Library.StateProviders;
using Tracker.UI.Shared.Dialogs.Characters;

namespace Tracker.UI.Shared;

public partial class NavMenu : IDisposable
{
    private bool _dropdownActive = true;

    [CascadingParameter] protected bool IsDarkMode { get; set; }

    [Inject] private IState<CharacterState> CharacterState { get; set; } = null!;

    [Inject] private ICharacterStateProvider CharacterStateProvider { get; set; } = null!;

    [Inject] private IModalService ModalService { get; set; } = null!;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Inject] private IState<NavMenuState> NavMenuState { get; set; } = null!;

    [Inject] private IToastService ToastService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        if (!CharacterState.Value.Characters.Any())
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

        ModalService.Show<Create>("Create Character", parameters, options);
    }

    private void LoadCharacter(int id) { NavigationManager.NavigateTo($"/character/{id}"); }

    private void ToggleDropdown() => _dropdownActive = !_dropdownActive;
}
