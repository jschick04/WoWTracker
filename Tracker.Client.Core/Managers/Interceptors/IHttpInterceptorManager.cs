using System.Threading.Tasks;
using Toolbelt.Blazor;

namespace Tracker.Client.Core.Managers.Interceptors {

    public interface IHttpInterceptorManager {

        void DisposeEvent();

        Task InterceptAfterHttpAsync(object sender, HttpClientInterceptorEventArgs e);

        Task InterceptBeforeHttpAsync(object sender, HttpClientInterceptorEventArgs e);

        void RegisterEvent();

    }

}