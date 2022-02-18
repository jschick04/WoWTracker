using Tracker.Api.Library.DataAccess;

namespace Tracker.Api.Services;

public class WowService : IServiceInstaller {

    public void InstallService(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env) {
        services.AddTransient<ICharacterData, CharacterData>();
        services.AddTransient<IItemData, ItemData>();
        services.AddTransient<IProfessionData, ProfessionData>();
    }

}