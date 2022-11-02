using Blazored.Modal;

namespace Tracker.Client.Helpers;

public static class ModalExtensions
{
    public static ModalOptions GetClass(this ModalOptions options, bool isDarkMode, bool isMinWidth = false)
    {
        string css = "global-modal-template";

        if (isDarkMode) { css += " darkmode"; }

        if (isMinWidth) { css += " min-width"; }

        options.Class = css;

        return options;
    }
}
