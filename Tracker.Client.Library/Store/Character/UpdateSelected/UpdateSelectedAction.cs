using Tracker.Api.Contracts.V1.Requests;

namespace Tracker.Client.Library.Store.Character.UpdateSelected;

public class UpdateSelectedAction
{
    public UpdateSelectedAction(int id, UpdateCharacterRequest request)
    {
        Id = id;
        Request = request;
    }

    public int Id { get; }

    public UpdateCharacterRequest Request { get; }
}
