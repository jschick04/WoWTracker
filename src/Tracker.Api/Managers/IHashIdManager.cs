namespace Tracker.Api.Managers;

public interface IHashIdManager
{
    int Decode(string value);

    string Encode(int id);
}
