using System.Threading.Tasks;
using Tracker.Library.Helpers;

namespace Tracker.Client.Shared {

    public partial class NavMenu {

        private string CurrentUsername { get; set; }

        protected override async Task OnInitializedAsync() {
            var user = await _stateProvider.GetAuthenticationStateProviderUserAsync();

            if (user.Identity?.IsAuthenticated is true) {
                var result = await _userManager.GetAsync(user.GetId());

                if (result.Succeeded) {
                    CurrentUsername = result.Data.Username;
                }
            }
        }

    }

}