using System.Threading.Tasks;
using Tracker.Library.Helpers;

namespace Tracker.Client.Shared {

    public partial class NavMenu {

        private string CurrentUsername { get; set; }

        // TODO: Extract this content into its own page
        // Possibly a Scoped class to store all character data
        // This could save on API calls, preventing potential double token refresh
        // Pages and Menu only pull from this class
        // That class is the only thing that can call the API
        // This way first login or opening page after login only pulls from this class
        // Initialized on MainLayout
        // This would be similar to how we get CurrentUsername from the StateProvider

        protected override async Task OnInitializedAsync() {
            var user = await _stateProvider.GetAuthenticationStateProviderUserAsync();

            if (user.Identity?.IsAuthenticated is true) {
                CurrentUsername = user.GetUsername();
            }
        }

    }

}