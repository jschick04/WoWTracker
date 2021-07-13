using System.Threading.Tasks;
using MudBlazor;
using Tracker.Api.Contracts.V1.Requests;

namespace Tracker.Client.Pages.Characters {

    public partial class Create {

        private readonly CreateCharacterRequest _request = new();

        private async Task SubmitAsync() {
            var result = await _characterManager.CreateAsync(_request);

            if (result.Succeeded) {
                _snackbar.Add("Creation Successful", Severity.Success);
                await _appStateProvider.UpdateCharactersAsync();
                _navigationManager.NavigateTo("/");
            } else {
                foreach (var message in result.Messages) {
                    _snackbar.Add(message, Severity.Error);
                }
            }
        }

    }

}