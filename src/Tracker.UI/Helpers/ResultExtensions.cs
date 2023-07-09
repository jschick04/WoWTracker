using Blazored.Toast.Services;
using Tracker.Library.Helpers;

namespace Tracker.UI.Helpers;

public static class ResultExtensions
{
    public static void ToastError(this IResult result, IToastService toastService)
    {
        if (string.IsNullOrWhiteSpace(result.Message)) { return; }

        toastService.ShowError(result.Message);
    }

    public static void ToastMessage(this IResult result, IToastService toastService)
    {
        if (string.IsNullOrWhiteSpace(result.Message)) { return; }

        if (result.Succeeded)
        {
            toastService.ShowSuccess(result.Message);
        }
        else
        {
            toastService.ShowError(result.Message);
        }
    }
}
