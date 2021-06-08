using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrackerApi.Library.DataAccess;

namespace TrackerApi.Services {

    public class WowService : IServiceInstaller {

        public void InstallService(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env) {
            services.AddTransient<ICharacterData, CharacterData>();
            services.AddTransient<IItemData, ItemData>();
            services.AddTransient<IProfessionData, ProfessionData>();
        }

    }

}