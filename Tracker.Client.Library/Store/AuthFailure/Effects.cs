using Fluxor;
using Tracker.Client.Library.Managers.Authentication;

namespace Tracker.Client.Library.Store.AuthFailure;

public class Effects
{
    private readonly IAuthenticationManager _authenticationManager;

    public Effects(IAuthenticationManager authenticationManager) => _authenticationManager = authenticationManager;

    [EffectMethod(typeof(LogOutAction))]
    public async Task HandleLogOutAction(IDispatcher dispatcher) => await _authenticationManager.Logout();
}
