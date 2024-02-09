using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using TddWebApi.Models;
using TddWebApi.Tests.Fixtures;
using Xunit;

namespace TddWebApi.Tests.Integration.System;

public class UserControllerTest: IClassFixture<WebApplicationFixture>
{
    private readonly WebApplicationFixture _factory;

    public UserControllerTest(WebApplicationFixture factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task TestGetUsers_WhenSuccess_ShouldReturnsUsers()
    {
        // Arrange
        var client = _factory.Client;

        // Act
        var response = await client.GetAsync("Users");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task TestGetUsersById_WhenSuccess_ShouldReturnUser()
    {
        // Arrange
        var client = _factory.Client;
        var userFixture = new User { Id = 1, Name = "Leanne Graham", Email = "Sincere@april.biz"};

        // Act
        var response = await client.GetAsync("Users/1");
        var content = await response.Content.ReadAsStringAsync();
        var user = JsonConvert.DeserializeObject<User>(content);

        // Assert
        user.Should().Be(userFixture);
    }

    [Fact]
    public async Task TestGetUsersById_WhenNotFound_ShouldReturnsNotFound()
    {
        // Arrange
        var client = _factory.Client;

        // Act
        var response = await client.GetAsync("Users/321321");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}