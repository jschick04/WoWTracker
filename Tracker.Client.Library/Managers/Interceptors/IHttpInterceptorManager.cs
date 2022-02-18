using Toolbelt.Blazor;

namespace Tracker.Client.Library.Managers.Interceptors {

    public interface IHttpInterceptorManager {

        void DisposeEvent();

        void InterceptAfterHttp(object? sender, HttpClientInterceptorEventArgs e);

        Task InterceptBeforeHttpAsync(object sender, HttpClientInterceptorEventArgs e);

        void RegisterEvent();

    }

}