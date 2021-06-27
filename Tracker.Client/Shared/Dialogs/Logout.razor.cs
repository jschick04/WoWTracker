using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Tracker.Client.Shared.Dialogs {

    public partial class Logout {

        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

        [Parameter] public string ContextText { get; set; }

        [Parameter] public string ButtonText { get; set; }

        [Parameter] public Color Color { get; set; }

        private void Cancel() => MudDialog.Cancel();

        private async Task Submit() {
            await _authenticationManager.Logout();
            _navigationManager.NavigateTo("/");

            MudDialog.Close(DialogResult.Ok(true));
        }

    }

}