using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.Library.Managers;

namespace Tracker.Client.Library.Managers.StateProviders {

    public class ApplicationStateProvider {

        private readonly ICharacterManager _characterManager;

        private List<CharacterResponse> _characters = new();

        public ApplicationStateProvider(ICharacterManager characterManager) => _characterManager = characterManager;

        public event Func<Task> OnChangeAsync;

        public async Task<List<CharacterResponse>> GetCharactersAsync() {
            if (_characters.Count == 0) { await UpdateCharactersAsync(); }

            return _characters;
        }

        public async Task UpdateCharactersAsync() {
            var result = await _characterManager.GetAllAsync();

            if (result.Succeeded) {
                _characters = result.Data;
            } else {
                _characters.Clear();
            }

            await NotifyStateChangeAsync();
        }

        private Task NotifyStateChangeAsync() => OnChangeAsync?.Invoke();

    }

}