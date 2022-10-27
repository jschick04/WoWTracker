using Tracker.Api.Contracts.V1.Requests;

namespace Tracker.Client.Library.StateProviders;

public interface INeededItemStateProvider
{
    void AddNeededItem(int id, string name, NeededItemRequest request);

    void GetAllNeededItems(int id);
}
