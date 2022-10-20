using Tracker.Api.Library.Helpers;

namespace Tracker.Api.Library.Models;

public class CharacterModel
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public Classes ClassId { get; set; }

    public Professions? FirstProfessionId { get; set; }

    public Professions? SecondProfessionId { get; set; }

    public bool HasCooking { get; set; }
}
