using Tracker.Api.Contracts.V1.Requests;

namespace Tracker.UI.Library.StateProviders;

public interface INeededItemStateProvider
{
    void AddNeededItem(string id, string name, NeededItemRequest request);

    void GetAllNeededItems(string id);

    void RemoveNeededItem(string id, NeededItemRequest request);
}
