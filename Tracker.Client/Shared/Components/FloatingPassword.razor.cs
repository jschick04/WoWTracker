using Microsoft.AspNetCore.Components;

namespace Tracker.Client.Shared.Components;

public partial class FloatingPassword
{
    private bool _isVisible;

    [Parameter]
    public string Label { get; set; } = null!;

    [Parameter]
    public string Value { get; set; } = null!;

    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }

    private Task OnValueChanged(ChangeEventArgs e)
    {
        Value = e.Value?.ToString() ?? string.Empty;
        return ValueChanged.InvokeAsync(Value);
    }
}
