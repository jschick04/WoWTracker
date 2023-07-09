using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Tracker.Api.Contracts.Identity.Requests;
using Tracker.Api.Contracts.Identity.Responses;
using Tracker.Api.Contracts.Routes;
using Tracker.Api.Entities;
using Xunit;

namespace Tracker.Api.Tests;

public class IdentityControllerTests : Tests
{
    [Fact]
    public async Task GetById_ReturnsCurrentUserAsAdmin_WhenUserIsFirstAccount()
    {
        await AuthenticateAsync();

        var response = await testClient.GetAsync(ApiRoutes.Account.GetByIdReplace(1));
        var returnedUser = await response.Content.ReadFromJsonAsync<UserResponse>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        returnedUser?.Username.Should().Be(Username);
        returnedUser?.Role.Should().Be(Role.Admin.ToString());
        returnedUser?.IsVerified.Should().BeTrue();
    }

    [Fact]
    public async Task Update_ReturnsCurrentUserWithNewValues()
    {
        await AuthenticateAsync();

        var updates = new UpdateRequest
        {
            LastName = "User",
        };

        var response = await testClient.PutAsJsonAsync(ApiRoutes.Account.UpdateReplace(1), updates);
        var returnedUser = await response.Content.ReadFromJsonAsync<UserResponse>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        returnedUser?.FirstName.Should().Be("Api");
        returnedUser?.LastName.Should().Be("User");
        returnedUser?.Username.Should().Be(Username);
        returnedUser?.Updated.Should().NotBeNull();
    }
}
