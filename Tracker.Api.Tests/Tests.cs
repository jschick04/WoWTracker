using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tracker.Api.Contracts.Identity.Requests;
using Tracker.Api.Contracts.Identity.Responses;
using Tracker.Api.Contracts.Routes;
using Tracker.Api.Data;

namespace Tracker.Api.Tests;

public class Tests {

    protected const string Username = "test@apitesting.com";
    protected const string Password = "Int3grat1on!";

    protected readonly HttpClient testClient;

    protected Tests() {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var data = new DataContext(options);

        var appFactory = new WebApplicationFactory<Startup>()
            .WithWebHostBuilder(
                builder => {
                    builder.UseSetting("https_port", "5001");
                    builder.ConfigureServices(services => services.AddSingleton(_ => data));
                }
            );

        testClient = appFactory.CreateClient();
    }

    protected async Task AuthenticateAsync() {
        testClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("bearer", await GetJwtAsync());
    }

    protected async Task<string> GetJwtAsync() {
        var registrationResponse = await testClient.PostAsJsonAsync(
            ApiRoutes.Identity.Register,
            new RegistrationRequest {
                FirstName = "Api",
                LastName = "Admin",
                Username = Username,
                Password = Password,
                ConfirmPassword = Password,
                AcceptTerms = true
            }
        );

        if (!registrationResponse.IsSuccessStatusCode) {
            return null;
        }

        var authenticateResponse = await testClient.PostAsJsonAsync(
            ApiRoutes.Identity.Authenticate,
            new AuthenticationRequest { Username = Username, Password = Password }
        );

        var results = await authenticateResponse.Content.ReadFromJsonAsync<AuthenticationResponse>();

        return results?.Token;
    }

}