using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.Library.Managers;

namespace Tracker.Client.Library.Managers.StateProviders {

    public class ApplicationStateProvider {

        private readonly ICharacterManager _characterManager;

        public ApplicationStateProvider(ICharacterManager characterManager) => _characterManager = characterManager;

        public event Action OnChange;

        public List<CharacterResponse> Characters { get; private set; } = new();

        public async Task UpdateCharactersAsync() {
            var result = await _characterManager.GetAllAsync();

            if (result.Succeeded) {
                Characters = result.Data;
            } else {
                Characters.Clear();
            }

            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

    }

}