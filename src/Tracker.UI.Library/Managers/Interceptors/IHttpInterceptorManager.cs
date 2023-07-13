using Toolbelt.Blazor;

namespace Tracker.UI.Library.Managers.Interceptors;

public interface IHttpInterceptorManager
{
    void DisposeEvent();

    Task InterceptBeforeHttpAsync(object sender, HttpClientInterceptorEventArgs e);

    void RegisterEvent();
}
