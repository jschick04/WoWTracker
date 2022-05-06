using Toolbelt.Blazor;

namespace Tracker.Client.Library.Managers.Interceptors {

    public interface IHttpInterceptorManager {

        void DisposeEvent();

        Task InterceptBeforeHttpAsync(object sender, HttpClientInterceptorEventArgs e);

        void RegisterEvent();

    }

}