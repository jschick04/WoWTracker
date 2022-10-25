using Tracker.Api.Contracts.V1.Requests;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.Library.Helpers;

namespace Tracker.Library.Managers;

public interface ICharacterManager
{
    Task<IResult> AddNeededItemAsync(int id, NeededItemRequest request);

    Task<Result<CharacterResponse>> CreateAsync(CreateCharacterRequest request);

    Task<IResult> DeleteAsync(int id);

    Task<Result<List<CharacterResponse>>> GetAllAsync();

    Task<Result<CharacterResponse>> GetByIdAsync(int id);

    Task<Result<List<NeededItemResponse>>> GetNeededItemsAsync(int id);

    Task<IResult> RemoveNeededItemAsync(int id, NeededItemRequest request);

    Task<IResult> UpdateAsync(int id, UpdateCharacterRequest request);
}
