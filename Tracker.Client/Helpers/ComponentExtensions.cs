namespace Tracker.Client.Helpers;

public enum ButtonColor {

    None,
    Primary,
    Secondary,
    Accent,
    Cancel

}

public static class ComponentExtensions {

    public static string GetString(this ButtonColor buttonColor) {
        return buttonColor switch {
            ButtonColor.Primary => "primary-btn",
            ButtonColor.Secondary => "secondary-btn",
            ButtonColor.Accent => "accent-btn",
            ButtonColor.Cancel => "cancel-btn",
            _ => ""
        };
    }

}