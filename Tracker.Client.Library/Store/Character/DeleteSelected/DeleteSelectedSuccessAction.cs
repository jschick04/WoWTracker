namespace Tracker.Client.Library.Store.Character.DeleteSelected;

public class DeleteSelectedSuccessAction
{
    public DeleteSelectedSuccessAction(int id) => Id = id;

    public int Id { get; }
}
