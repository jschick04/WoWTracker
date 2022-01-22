using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Tracker.Api.Contracts.V1.Requests;

namespace Tracker.Client.Shared.Dialogs.Characters {

    public partial class Create {

        private readonly CreateCharacterRequest _request = new();

        private bool _isLoading;

        [Parameter] public string ButtonText { get; set; }

        [Parameter] public Color Color { get; set; }

        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

        private void Cancel() => MudDialog.Cancel();

        private async Task Submit() {
            _isLoading = true;

            var response = await _characterManager.CreateAsync(_request);

            if (response.Succeeded) {
                await _appStateProvider.UpdateCharactersAsync();

                _snackbar.Add("Creation Successful", Severity.Success);
            } else {
                foreach (var message in response.Messages) {
                    _snackbar.Add(message, Severity.Error);
                }
            }

            _isLoading = false;

            MudDialog.Close(DialogResult.Ok(true));
        }

    }

}