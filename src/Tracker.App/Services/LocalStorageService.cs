using Blazored.LocalStorage;

namespace Tracker.App.Services;

public class LocalStorageService : ILocalStorageService
{
    public event EventHandler<ChangingEventArgs> Changing;

    public event EventHandler<ChangedEventArgs> Changed;

    public ValueTask ClearAsync(CancellationToken cancellationToken = default) =>
        new(Task.Run(() => Preferences.Default.Clear(), cancellationToken));

    public ValueTask<bool> ContainKeyAsync(string key, CancellationToken cancellationToken = default) =>
        new(Preferences.Default.ContainsKey(key));

    public ValueTask<string> GetItemAsStringAsync(string key, CancellationToken cancellationToken = default) =>
        new(Preferences.Default.Get(key, string.Empty));

    public ValueTask<T> GetItemAsync<T>(string key, CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();

    public ValueTask<string> KeyAsync(int index, CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();

    public ValueTask<IEnumerable<string>> KeysAsync(CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();

    public ValueTask<int> LengthAsync(CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();

    public ValueTask RemoveItemAsync(string key, CancellationToken cancellationToken = default) =>
        new(Task.Run(() => Preferences.Default.Remove(key), cancellationToken));

    public ValueTask RemoveItemsAsync(IEnumerable<string> keys, CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();

    public ValueTask SetItemAsStringAsync(string key, string data, CancellationToken cancellationToken = default) =>
        new(Task.Run(() => Preferences.Default.Set(key, data), cancellationToken));

    public ValueTask SetItemAsync<T>(string key, T data, CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();
}
