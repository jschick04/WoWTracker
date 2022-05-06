using Microsoft.Extensions.Logging;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.Library.Helpers;
using Tracker.Library.Managers;

namespace Tracker.Client.Library.Managers.StateProviders;

public class ApplicationStateProvider {

    private readonly ICharacterManager _characterManager;
    private readonly ILogger<ApplicationStateProvider> _logger;

    private List<CharacterResponse> _characters = new();

    public ApplicationStateProvider(ICharacterManager characterManager, ILogger<ApplicationStateProvider> logger) {
        _characterManager = characterManager;
        _logger = logger;
    }

    public event Func<Task>? OnChangeAsync;

    public async Task<List<CharacterResponse>> GetCharactersAsync() {
        if (_characters.Count == 0) {
            await UpdateCharactersAsync();
        }

        return _characters;
    }

    public async Task UpdateCharactersAsync() {
        var result = await _characterManager.GetAllAsync();

        if (result.GetDataIfSuccess(ref _characters)) {
            await NotifyStateChangeAsync();
        } else {
            _logger.LogWarning("Failed to update character data");
            _characters.Clear();
        }
    }

    private Task NotifyStateChangeAsync() => OnChangeAsync?.Invoke() ?? Task.CompletedTask;

}