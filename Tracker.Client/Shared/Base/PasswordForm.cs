using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Tracker.Client.Shared.Base {

    public abstract class PasswordForm : ComponentBase {

        protected string confirmIcon = Icons.Material.Filled.VisibilityOff;
        protected InputType confirmInput = InputType.Password;
        protected bool confirmVisibility;

        protected string passwordIcon = Icons.Material.Filled.VisibilityOff;
        protected InputType passwordInput = InputType.Password;
        protected bool passwordVisibility;

        protected void ToggleConfirmVisibility() {
            if (confirmVisibility) {
                confirmVisibility = false;
                confirmIcon = Icons.Material.Filled.VisibilityOff;
                confirmInput = InputType.Password;
            } else {
                confirmVisibility = true;
                confirmIcon = Icons.Material.Filled.Visibility;
                confirmInput = InputType.Text;
            }
        }

        protected void TogglePasswordVisibility() {
            if (passwordVisibility) {
                passwordVisibility = false;
                passwordIcon = Icons.Material.Filled.VisibilityOff;
                passwordInput = InputType.Password;
            } else {
                passwordVisibility = true;
                passwordIcon = Icons.Material.Filled.Visibility;
                passwordInput = InputType.Text;
            }
        }

    }

}