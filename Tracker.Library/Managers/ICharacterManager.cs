using System.Collections.Generic;
using System.Threading.Tasks;
using Tracker.Api.Contracts.V1.Requests;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.Library.Helpers;

namespace Tracker.Library.Managers {

    public interface ICharacterManager {

        Task<IResult> CreateAsync(CreateCharacterRequest request);

        Task<IResult> DeleteAsync(int id);

        Task<Result<List<CharacterResponse>>> GetAllAsync();

    }

}