using Microsoft.AspNetCore.Components;

namespace Tracker.Client.Shared.Dialogs.Characters;

public partial class Delete {

    private bool _isLoading;

    [CascadingParameter] private MudDialogInstance MudDialog { get; set; } = null!;

    [Parameter] public string ContextText { get; set; } = null!;

    [Parameter] public string ButtonText { get; set; } = null!;

    [Parameter] public Color Color { get; set; }

    [Parameter] public int Id { get; set; }

    private void Cancel() => MudDialog.Cancel();

    private async Task Submit() {
        _isLoading = true;

        var response = await _characterManager.DeleteAsync(Id);

        _isLoading = false;

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