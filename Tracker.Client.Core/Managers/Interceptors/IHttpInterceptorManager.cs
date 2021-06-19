using System.Threading.Tasks;
using Toolbelt.Blazor;

namespace Tracker.Client.Core.Managers.Interceptors {

    public interface IHttpInterceptorManager {

        void DisposeEvent();

        Task InterceptBeforeHttpAsync(object sender, HttpClientInterceptorEventArgs e);

        void RegisterEvent();

    }

}