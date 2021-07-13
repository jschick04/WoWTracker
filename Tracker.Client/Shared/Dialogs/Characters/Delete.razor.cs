using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Tracker.Client.Shared.Dialogs.Characters {

    public partial class Delete {

        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

        [Parameter] public string ContextText { get; set; }

        [Parameter] public string ButtonText { get; set; }

        [Parameter] public Color Color { get; set; }

        [Parameter] public int Id { get; set; }

        private void Cancel() => MudDialog.Cancel();

        private async Task Submit() {
            var response = await _characterManager.DeleteAsync(Id);

            if (response.Succeeded) {
                _snackbar.Add("Deletion Successful", Severity.Success);
            } else {
                foreach (var message in response.Messages) {
                    _snackbar.Add(message, Severity.Error);
                }
            }

            MudDialog.Close(DialogResult.Ok(true));
        }

    }

}