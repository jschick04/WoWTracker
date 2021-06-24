using System.Threading.Tasks;
using Tracker.Library.Helpers;

namespace Tracker.Client.Shared {

    public partial class NavMenu {

        private string CurrentUsername { get; set; }

        // TODO: Extract this content into its own page

        protected override async Task OnInitializedAsync() {
            var user = await _stateProvider.GetAuthenticationStateProviderUserAsync();

            if (user.Identity?.IsAuthenticated is true) {
                CurrentUsername = user.GetUsername();
            }
        }

    }

}