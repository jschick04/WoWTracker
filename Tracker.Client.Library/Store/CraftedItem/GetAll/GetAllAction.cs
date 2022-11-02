namespace Tracker.Client.Library.Store.CraftedItem.GetAll;

public class GetAllAction
{
    public GetAllAction(string? primary, string? secondary)
    {
        Primary = primary;
        Secondary = secondary;
    }

    public string? Primary { get; }

    public string? Secondary { get; }
}
