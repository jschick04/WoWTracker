using Microsoft.AspNetCore.Mvc;
using Tracker.Api.Authorization;
using Tracker.Api.Contracts.Routes;
using Tracker.Api.Contracts.V1.Requests;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.Api.Library.DataAccess;
using Tracker.Api.Library.Helpers;
using Tracker.Api.Library.Models;
using Tracker.Api.Managers;

namespace Tracker.Api.Controllers.V1;

[Authorize]
public sealed class CharacterController : BaseApiController
{
    private readonly ICharacterData _data;
    private readonly IHashIdManager _hashIdManager;

    public CharacterController(ICharacterData data, IHashIdManager hashIdManager)
    {
        _data = data;
        _hashIdManager = hashIdManager;
    }

    [HttpPut(ApiRoutes.Character.AddNeededItemUri)]
    public async Task<IActionResult> AddNeededItem([FromRoute] string id, [FromBody] NeededItemRequest request)
    {
        if (Account is null) { return Unauthorized(); }

        if (!Converter.TryParseWithMemberName(request.Profession, out Professions professionId))
        {
            return NotFound();
        }

        var result = await _data.AddNeededItem(
            new NeededItemModel(
                _hashIdManager.Decode(id),
                request.CharacterName ?? string.Empty,
                professionId,
                request.Name,
                request.Amount));

        return result switch
        {
            { IsSuccess: true } => Ok(),
            { IsFailed: true } => Problem(result.Errors.FirstOrDefault()?.Message),
            _ => Problem()
        };
    }

    [HttpPost(ApiRoutes.Character.CreateUri)]
    public async Task<ActionResult<CharacterResponse>> Create([FromBody] CreateCharacterRequest request)
    {
        if (Account is null) { return Unauthorized(); }

        var model = new CharacterModel
        {
            UserId = Account.Id,
            Name = request.Name,
            HasCooking = request.HasCooking
        };

        if (Converter.TryParseWithMemberName(request.Class, out Classes classId))
        {
            model.ClassId = classId;
        }

        if (Converter.TryParseWithMemberName(request.FirstProfession, out Professions firstProfessionId))
        {
            model.FirstProfessionId = firstProfessionId;
        }

        if (Converter.TryParseWithMemberName(request.SecondProfession, out Professions secondProfessionId))
        {
            model.SecondProfessionId = secondProfessionId;
        }

        var result = await _data.Create(model);

        return result switch
        {
            { IsSuccess: true } => CreatedAtAction(nameof(GetById),
                new { id = _hashIdManager.Encode(result.Value)},
                new CharacterResponse
                {
                    Id = _hashIdManager.Encode(result.Value),
                    Name = model.Name,
                    Class = classId.GetName(),
                    FirstProfession = request.FirstProfession,
                    SecondProfession = request.SecondProfession,
                    HasCooking = request.HasCooking
                }),
            { IsFailed: true } => Problem(result.Errors.FirstOrDefault()?.Message),
            _ => Problem()
        };
    }

    [HttpDelete(ApiRoutes.Character.DeleteUri)]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        if (Account is null) { return Unauthorized(); }

        var model = await _data.GetById(_hashIdManager.Decode(id), Account.Id);

        if (model.Value is null) { return NotFound(); }

        var result = await _data.Delete(model.Value.Id);

        return result switch
        {
            { IsSuccess: true } => NoContent(),
            { IsFailed: true } => Problem(result.Errors.FirstOrDefault()?.Message),
            _ => Problem()
        };
    }

    [HttpGet(ApiRoutes.Character.GetAllUri)]
    public async Task<ActionResult<IEnumerable<CharacterResponse>>> GetAll()
    {
        if (Account is null) { return Unauthorized(); }

        var result = await _data.GetAll(Account.Id);

        return result switch
        {
            { IsSuccess: true } => Ok(result.Value.Select(
                character => new CharacterResponse
                {
                    Id = _hashIdManager.Encode(character.Id),
                    Name = character.Name,
                    Class = character.ClassId.GetName(),
                    FirstProfession = character.FirstProfessionId.GetName(),
                    SecondProfession = character.SecondProfessionId.GetName(),
                    HasCooking = character.HasCooking
                }
            )),
            { IsFailed: true } => Problem(result.Errors.FirstOrDefault()?.Message),
            _ => Problem()
        };
    }

    [HttpGet(ApiRoutes.Character.GetByIdUri)]
    public async Task<ActionResult<CharacterResponse>> GetById([FromRoute] string id)
    {
        if (Account is null) { return Unauthorized(); }

        var result = await _data.GetById(_hashIdManager.Decode(id), Account.Id);

        return result switch
        {
            { IsSuccess: true, ValueOrDefault: { } value } => Ok(new CharacterResponse
            {
                Id = _hashIdManager.Encode(value.Id),
                Name = value.Name,
                Class = value.ClassId.GetName(),
                FirstProfession = value.FirstProfessionId.ToString(),
                SecondProfession = value.SecondProfessionId.ToString(),
                HasCooking = value.HasCooking
            }),
            { IsFailed: true } => Problem(result.Errors.FirstOrDefault()?.Message),
            { ValueOrDefault: null } => NotFound(),
            _ => Problem()
        };
    }

    [HttpGet(ApiRoutes.Character.GetNeededItemsUri)]
    public async Task<ActionResult<IEnumerable<NeededItemResponse>>> GetNeededItems([FromRoute] string id)
    {
        if (Account is null) { return Unauthorized(); }

        var result = await _data.GetNeededItems(_hashIdManager.Decode(id));

        return result switch
        {
            { IsSuccess: true } => Ok(result.Value.Select(
                item => new NeededItemResponse
                {
                    CharacterId = id,
                    CharacterName = item.CharacterName,
                    Profession = item.ProfessionId.GetName(),
                    Name = item.Name,
                    Amount = item.Amount
                }
            )),
            { IsFailed: true } => Problem(result.Errors.FirstOrDefault()?.Message),
            _ => Problem()
        };
    }

    [HttpPut(ApiRoutes.Character.RemoveNeededItemUri)]
    public async Task<IActionResult> RemoveNeededItem([FromRoute] string id, [FromBody] NeededItemRequest request)
    {
        if (Account is null) { return Unauthorized(); }

        if (!Converter.TryParseWithMemberName(request.Profession, out Professions professionId))
        {
            return NotFound();
        }

        var result = await _data.RemoveNeededItem(
            new NeededItemModel(
                _hashIdManager.Decode(id),
                request.CharacterName ?? string.Empty,
                professionId,
                request.Name,
                request.Amount));

        return result switch
        {
            { IsSuccess: true } => Ok(),
            { IsFailed: true } => Problem(result.Errors.FirstOrDefault()?.Message),
            _ => Problem()
        };
    }

    [HttpPut(ApiRoutes.Character.UpdateUri)]
    public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateCharacterRequest request)
    {
        if (Account is null) { return Unauthorized(); }

        var model = await _data.GetById(_hashIdManager.Decode(id), Account.Id);

        if (model.Value is null) { return NotFound(); }

        model.Value.Name = request.Name ?? model.Value.Name;

        if (Converter.TryParseWithMemberName(request.Class, out Classes classId))
        {
            model.Value.ClassId = classId;
        }

        // TODO: Figure out a better way to do this to allow dropping a profession without specifying all params
        // Maybe just set an Enum value of 0 = None
        if (Converter.TryParseWithMemberName(request.FirstProfession, out Professions firstProfessionId))
        {
            model.Value.FirstProfessionId = firstProfessionId;
        }
        else
        {
            model.Value.FirstProfessionId = null;
        }

        if (Converter.TryParseWithMemberName(request.SecondProfession, out Professions secondProfessionId))
        {
            model.Value.SecondProfessionId = secondProfessionId;
        }
        else
        {
            model.Value.SecondProfessionId = null;
        }

        model.Value.HasCooking = request.HasCooking;

        var result = await _data.Update(model.Value);

        return result switch
        {
            { IsSuccess: true } => Ok(),
            { IsFailed: true } => Problem(result.Errors.FirstOrDefault()?.Message),
            _ => Problem()
        };
    }
}
