using Tracker.Api.Contracts.V1.Requests;

namespace Tracker.Client.Library.Store.NeededItem.AddItem;

public class AddItemAction
{
    public AddItemAction(int id, string characterName, NeededItemRequest request)
    {
        Id = id;
        CharacterName = characterName;
        Request = request;
    }

    public string CharacterName { get; }

    public int Id { get; }

    public NeededItemRequest Request { get; }
}
