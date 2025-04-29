namespace Ambev.DeveloperEvaluation.Integration.Helper;

using System.Net.Http.Json;
using AutoFixture;
using Bogus;
using Newtonsoft.Json.Linq;
using Xunit;

public class UserHelper
{
    private readonly Fixture _fixture;

    public UserHelper()
    {
        _fixture = new Fixture();
    }

    public UserCreateRequest GenerateRandomCustomerRequest()
    {
        var faker = new Faker();

        return _fixture.Build<UserCreateRequest>()
            .With(u => u.Username, faker.Internet.UserName())
            .With(u => u.Email, faker.Internet.Email())
            .With(u => u.Phone, faker.Phone.PhoneNumber("##########"))
            .With(u => u.Password, "Str0ngP@ssw0rd!")
            .With(u => u.Status, 1)
            .With(u => u.Role, 1)
            .Create();
    }

    public async Task<HttpResponseMessage> CreateCustomerAsync(HttpClient client, UserCreateRequest request)
    {
        return await client.PostAsJsonAsync("/api/Users", request);
    }

    public async Task<string> AuthenticateAsync(HttpClient client, UserCreateRequest request)
    {
        var authPayload = new
        {
            request.Email,
            request.Password
        };

        var authResponse = await client.PostAsJsonAsync("/api/Auth", authPayload);
        authResponse.EnsureSuccessStatusCode();

        return await authResponse.Content.ReadAsStringAsync();
    }

    public async Task<string> CreateRandomCustomerGetTokenFromAsync(HttpClient client, UserCreateRequest request)
    {
        // Create user
        var createResponse = await CreateCustomerAsync(client, request);

        createResponse.EnsureSuccessStatusCode();
        var createContent = await createResponse.Content.ReadAsStringAsync();
        
        // Authenticate
        var responseContent = await AuthenticateAsync(client, request);

        // Parse the JSON and extract the token directly
        var responseObject = JObject.Parse(responseContent);
        var token = responseObject.SelectToken("data.data.token")?.ToString();

        // Return the token or throw an exception if token is not found
        if (string.IsNullOrEmpty(token))
        {
            throw new Exception("Authentication failed: No token found in response.");
        }

        return token;
    }
    
}

/// <summary>
/// Represents the data required to create a new user.
/// </summary>
public class UserCreateRequest
{
    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    public required string Username { get; set; }

    /// <summary>
    /// Gets or sets the password of the user. 
    /// Must meet password policy (e.g., contain uppercase, lowercase, special characters, etc.).
    /// </summary>
    public required string Password { get; set; } 

    /// <summary>
    /// Gets or sets the phone number of the user.
    /// </summary>
    public required string Phone { get; set; }

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Gets or sets the status of the user. 
    /// Typically 1 = Active, 0 = Inactive.
    /// </summary>
    public int Status { get; set; } = 1;

    /// <summary>
    /// Gets or sets the role of the user. 
    /// Example: 1 = Customer, 2 = Admin.
    /// </summary>
    public int Role { get; set; } = 1;
}
