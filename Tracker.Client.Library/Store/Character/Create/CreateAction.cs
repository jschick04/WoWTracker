using Tracker.Api.Contracts.V1.Requests;

namespace Tracker.Client.Library.Store.Character.Create;

public class CreateAction
{
    public CreateAction(CreateCharacterRequest request) => Request = request;

    public CreateCharacterRequest Request { get; }
}
