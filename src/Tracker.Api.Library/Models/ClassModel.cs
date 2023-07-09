namespace Tracker.Api.Library.Models;

public class ClassModel
{
    public string Name { get; set; } = null!;

    public List<SpecModel> Specs { get; set; } = null!;
}
