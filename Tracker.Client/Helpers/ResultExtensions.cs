using Blazored.Toast.Services;
using Tracker.Library.Helpers;

namespace Tracker.Client.Helpers;

public static class ResultExtensions {

    public static void ToastError(this IResult result, IToastService toastService) {
        if (result.Messages is null) { return; }

        foreach (var message in result.Messages) {
            toastService.ShowError(message);
        }
    }

    public static void ToastMessage(this IResult result, IToastService toastService) {
        if (result.Messages is null) { return; }

        if (result.Succeeded) {
            foreach (var message in result.Messages) {
                toastService.ShowSuccess(message);
            }
        } else {
            foreach (var message in result.Messages) {
                toastService.ShowError(message);
            }
        }
    }

}