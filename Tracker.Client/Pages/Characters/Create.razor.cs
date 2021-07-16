using System.Threading.Tasks;
using MudBlazor;
using Tracker.Api.Contracts.V1.Requests;

namespace Tracker.Client.Pages.Characters {

    public partial class Create {

        private bool _isLoading;

        private readonly CreateCharacterRequest _request = new();

        private async Task SubmitAsync() {
            _isLoading = true;

            var result = await _characterManager.CreateAsync(_request);

            if (result.Succeeded) {
                await _appStateProvider.UpdateCharactersAsync();
                
                _isLoading = false;

                _snackbar.Add("Creation Successful", Severity.Success);
                _navigationManager.NavigateTo("/");
            } else {
                _isLoading = false;

                foreach (var message in result.Messages) {
                    _snackbar.Add(message, Severity.Error);
                }
            }
        }

    }

}