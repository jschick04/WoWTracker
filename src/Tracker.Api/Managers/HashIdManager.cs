using HashidsNet;
using Tracker.Api.Settings;

namespace Tracker.Api.Managers;

public class HashIdManager : IHashIdManager
{
    private readonly Hashids _hashids;

    public HashIdManager(HashIdSettings settings) => _hashids = new Hashids(settings.Secret, minHashLength: 10);

    public int Decode(string value)
    {
        try
        {
            return _hashids.DecodeSingle(value);
        }
        catch (NoResultException)
        {
            return 0;
        }
    }

    public string Encode(int id) => _hashids.Encode(id);
}
