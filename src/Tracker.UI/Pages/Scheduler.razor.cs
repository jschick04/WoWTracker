using Microsoft.AspNetCore.Components;

namespace Tracker.UI.Pages;

public partial class Scheduler
{
    [Inject] private HttpClient HttpClient { get; set; } = null!;

    private string Uri { get; set; } = null !;

    protected override void OnInitialized() => Uri = $"{HttpClient.BaseAddress}scheduler";
}
