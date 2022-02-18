using Microsoft.AspNetCore.Mvc;
using Tracker.Api.Authorization;
using Tracker.Api.Contracts.Routes;
using Tracker.Api.Contracts.V1.Requests;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.Api.Library.DataAccess;
using Tracker.Api.Library.Helpers;
using Tracker.Api.Library.Models;

namespace Tracker.Api.Controllers.V1;

[Authorize]
public class CharacterController : BaseApiController {

    private readonly ICharacterData _data;

    public CharacterController(ICharacterData data) => _data = data;

    [HttpPost(ApiRoutes.Character.Create)]
    public async Task<IActionResult> Create([FromBody] CreateCharacterRequest request) {
        if (Account is null) { return Unauthorized(); }

        var model = new CharacterModel {
            UserId = Account.Id,
            Name = request.Name,
            HasCooking = request.HasCooking
        };

        if (Converter.TryParseWithMemberName(request.Class, out Classes classId)) {
            model.ClassId = classId;
        }

        if (Converter.TryParseWithMemberName(request.FirstProfession, out Professions firstProfessionId)) {
            model.FirstProfessionId = firstProfessionId;
        }

        if (Converter.TryParseWithMemberName(request.SecondProfession, out Professions secondProfessionId)) {
            model.SecondProfessionId = secondProfessionId;
        }

        await _data.Create(model);

        return Ok();
    }

    [HttpDelete(ApiRoutes.Character.Delete)]
    public async Task<IActionResult> Delete([FromRoute] int id) {
        if (Account is null) { return Unauthorized(); }

        var model = await _data.GetById(id, Account.Id);

        if (model is null) { return NotFound(); }

        await _data.Delete(model.Id);

        return NoContent();
    }

    [HttpGet(ApiRoutes.Character.GetAll)]
    public async Task<ActionResult<IEnumerable<CharacterResponse>>> GetAll() {
        if (Account is null) { return Unauthorized(); }

        var model = await _data.GetAll(Account.Id);

        if (model.Count == 0) { return NotFound(); }

        var response = model.Select(
            character => new CharacterResponse {
                Id = character.Id,
                Name = character.Name,
                Class = character.ClassId.GetName(),
                FirstProfession = character.FirstProfessionId.GetName(),
                SecondProfession = character.SecondProfessionId.GetName(),
                HasCooking = character.HasCooking
            }
        );

        return Ok(response);
    }

    [HttpGet(ApiRoutes.Character.GetById)]
    public async Task<ActionResult<CharacterResponse>> GetById([FromRoute] int id) {
        if (Account is null) { return Unauthorized(); }

        var model = await _data.GetById(id, Account.Id);

        if (model is null) { return NotFound(); }

        var response = new CharacterResponse {
            Id = model.Id,
            Name = model.Name,
            Class = model.ClassId.ToString(),
            FirstProfession = model.FirstProfessionId.ToString(),
            SecondProfession = model.SecondProfessionId.ToString(),
            HasCooking = model.HasCooking
        };

        return Ok(response);
    }

    [HttpPut(ApiRoutes.Character.Update)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCharacterRequest request) {
        if (Account is null) { return Unauthorized(); }

        var model = await _data.GetById(id, Account.Id);

        if (model is null) { return NotFound(); }

        model.Name = request.Name ?? model.Name;

        if (Converter.TryParseWithMemberName(request.Class, out Classes classId)) {
            model.ClassId = classId;
        }

        // TODO: Figure out a better way to do this to allow dropping a profession without specifying all params
        // Maybe just set an Enum value of 0 = None
        if (Converter.TryParseWithMemberName(request.FirstProfession, out Professions firstProfessionId)) {
            model.FirstProfessionId = firstProfessionId;
        } else {
            model.FirstProfessionId = null;
        }

        if (Converter.TryParseWithMemberName(request.SecondProfession, out Professions secondProfessionId)) {
            model.SecondProfessionId = secondProfessionId;
        } else {
            model.SecondProfessionId = null;
        }

        model.HasCooking = request.HasCooking ?? model.HasCooking;

        await _data.Update(model);

        return Ok();
    }

}