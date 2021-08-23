using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using MiddlewareNz.Evaluation.WebApi;
using MiddlewareNz.Evaluation.WebApi.Models;
using Xunit;

namespace WebApi.Tests.Integration
{
    public class CompanyControllerTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public CompanyControllerTests()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .ConfigureAppConfiguration(builder => builder.AddJsonFile("appsettings.json")));
            _client = _server.CreateClient();
        }

        [Fact]
        public async void Get_ShouldReturnHttp404_WithUnknownId()
        {
            // Arrange
            var unknownId = 0;

            // Act
            var response = await _client.GetAsync($"/company/{unknownId}");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async void Get_ShouldReturnHttpOk_WithKnownId()
        {
            // Arrange
            var knownId = 1;

            // Act
            var result = await _client.GetAsync($"/company/{knownId}");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async void Get_ShouldReturnCompanyModel_WithKnownId()
        {
            // Arrange
            var knownId = 2;

            // Act
            var response = await _client.GetAsync($"/company/{knownId}");
            var content = await response.Content.ReadAsStringAsync();
            var company = JsonSerializer.Deserialize<Company>(content);

            // Assert
            Assert.NotNull(company);
            Assert.Equal(knownId, company.Id);
        }
    }
}
