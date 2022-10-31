namespace Tracker.Client.Library.Store.NeededItem.RemoveItem;

public class RemoveItemSuccessAction
{
    public RemoveItemSuccessAction(string name, int amount)
    {
        Name = name;
        Amount = amount;
    }

    public int Amount { get; }

    public string Name { get; }
}
