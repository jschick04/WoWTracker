namespace Tracker.Api.Services;

public interface IServiceInstaller {

    void InstallService(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env);

}