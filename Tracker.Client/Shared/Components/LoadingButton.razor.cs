using Microsoft.AspNetCore.Components;
using Tracker.Client.Helpers;

namespace Tracker.Client.Shared.Components;

public partial class LoadingButton {

    [Parameter] public ButtonColor Color { get; set; } = ButtonColor.None;

    [Parameter] public bool IsDisabled { get; set; }

    [Parameter] public bool IsFullWidth { get; set; }

    private string GetButtonColor() => Color.GetString();

    private string GetFullWidthValue() => IsFullWidth ? "form-button-full" : "";

}