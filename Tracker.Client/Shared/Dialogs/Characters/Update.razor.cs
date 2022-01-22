using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Tracker.Api.Contracts.V1.Requests;
using Tracker.Api.Contracts.V1.Responses;

namespace Tracker.Client.Shared.Dialogs.Characters {

    public partial class Update {

        private readonly UpdateCharacterRequest _request = new();

        private bool _isLoading;

        [Parameter] public string ButtonText { get; set; }

        [Parameter] public CharacterResponse Character { get; set; }

        [Parameter] public Color Color { get; set; }

        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

        protected override void OnInitialized() {
            _request.Name = Character.Name;
            _request.Class = Character.Class;
            _request.FirstProfession = Character.FirstProfession;
            _request.SecondProfession = Character.SecondProfession;
            _request.HasCooking = Character.HasCooking;
        }

        private void Cancel() => MudDialog.Cancel();

        private async Task Submit() {
            _isLoading = true;

            var response = await _characterManager.UpdateAsync(Character.Id, _request);

            if (response.Succeeded) {
                await _appStateProvider.UpdateCharactersAsync();

                _isLoading = false;

                _snackbar.Add("Creation Successful", Severity.Success);
            } else {
                foreach (var message in response.Messages) {
                    _snackbar.Add(message, Severity.Error);
                }
            }

            MudDialog.Close(DialogResult.Ok(true));
        }

    }

}