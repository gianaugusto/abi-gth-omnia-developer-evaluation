using Ambev.DeveloperEvaluation.Integration.Helper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Tests
{
    public class UserControllerTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly TestWebApplicationFactory _factory;

        public UserControllerTests(TestWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CreateUser_ThenAuthenticate()
        {
            var client = _factory.CreateClient();
            var helper = new UserHelper();

            // Create user
            var userRequest = helper.GenerateRandomCustomerRequest();
            var createResponse = await helper.CreateCustomerAsync(client, userRequest);

            createResponse.EnsureSuccessStatusCode();
            var createContent = await createResponse.Content.ReadAsStringAsync();
            Assert.Contains("User created successfully", createContent);

            // Authenticate
            var token = await helper.AuthenticateAsync(client, userRequest);
            Assert.False(string.IsNullOrWhiteSpace(token));
        }

    }
}
