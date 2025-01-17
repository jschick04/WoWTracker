using Microsoft.AspNetCore.Components;

namespace Tracker.UI.Shared.Components;

public partial class FloatingSelect
{
    private bool _expanded;

    [Parameter]
    public bool AddEmpty { get; set; }

    [Parameter]
    public IEnumerable<string> ItemList { get; set; } = Enumerable.Empty<string>();

    [Parameter]
    public string Label { get; set; } = null!;

    [Parameter]
    public EventCallback LinkedValue { get; set; }

    [Parameter]
    public bool ShortWidth { get; set; }

    [Parameter]
    public string Value { get; set; } = null!;

    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }

    private void SetDropdownVisibility(bool visible) => _expanded = visible;

    private async void SetValue(string value)
    {
        Value = value;
        SetDropdownVisibility(false);

        await LinkedValue.InvokeAsync();

        await ValueChanged.InvokeAsync(Value);
    }
}
