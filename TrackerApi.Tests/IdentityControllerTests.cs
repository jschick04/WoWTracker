using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using TrackerApi.Contracts.V1;
using TrackerApi.Contracts.V1.Requests;
using TrackerApi.Contracts.V1.Responses;
using TrackerApi.Entities;
using Xunit;

namespace TrackerApi.Tests {

    public class IdentityControllerTests : Tests {

        [Fact]
        public async Task GetById_ReturnsCurrentUserAsAdmin_WhenUserIsFirstAccount() {
            await AuthenticateAsync();

            var response = await testClient.GetAsync(ApiRoutes.Identity.GetById.Replace("{id}", "1"));
            var returnedUser = await response.Content.ReadFromJsonAsync<UserResponse>();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            returnedUser?.Username.Should().Be(Username);
            returnedUser?.Role.Should().Be(Role.Admin.ToString());
            returnedUser?.IsVerified.Should().BeTrue();
        }

        [Fact]
        public async Task Update_ReturnsCurrentUserWithNewValues() {
            await AuthenticateAsync();

            var updates = new UpdateRequest {
                LastName = "User",
            };

            var response = await testClient.PutAsJsonAsync(ApiRoutes.Identity.Update.Replace("{id}", "1"), updates);
            var returnedUser = await response.Content.ReadFromJsonAsync<UserResponse>();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            returnedUser?.FirstName.Should().Be("Api");
            returnedUser?.LastName.Should().Be("User");
            returnedUser?.Username.Should().Be(Username);
            returnedUser?.Updated.Should().NotBeNull();
        }

    }

}