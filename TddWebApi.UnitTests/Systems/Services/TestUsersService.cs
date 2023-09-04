using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using TddWebApi.Configs;
using TddWebApi.Models;
using TddWebApi.Services;
using TddWebApi.UnitTests.Fixtures;
using TddWebApi.UnitTests.Helpers;
using Xunit;

namespace TddWebApi.UnitTests.Systems.Services;

public class TestUsersService
{

    [Fact]
    public async Task GetAllUsersWhenCalledInvokesHttpGetRequest()
    {
        // Arrange
        var expectedResponse = UsersFixture.GetTestUsers();
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourcesList(expectedResponse);
        var httpClient = new HttpClient(handlerMock.Object);
        var endpoint = "https://example.com/users";
        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });
        
        var service = new UsersService(httpClient, config);

        // Act
        await service.GetAllUsers();

        // Assert
        handlerMock
            .Protected()
            .Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
            );
    } 
    
    [Fact]
    public async Task GetAllUsersWhenCalledInvokesConfiguredUrl()
    {
        // Arrange
        var expectedResponse = UsersFixture.GetTestUsers();
        var handlerMock = MockHttpMessageHandler<User>
            .SetupBasicGetResourcesList(expectedResponse);
        var httpClient = new HttpClient(handlerMock.Object);
        var endpoint = "https://example.com/users";
        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });
        var service = new UsersService(httpClient, config);

        
        // Act
        await service.GetAllUsers();

        // Assert
        handlerMock
            .Protected()
            .Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(
                    req => req.Method == HttpMethod.Get
                    && req.RequestUri.ToString() == endpoint
                    ),
                ItExpr.IsAny<CancellationToken>()
            );
    } 

    
    [Fact]
    public async Task GetAllUsersWhenCalledReturnsListOfUsersOfExpectedSize()
    {
        // Arrange
        var expectedResponse = UsersFixture.GetTestUsers();
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourcesList(expectedResponse);
        var httpClient = new HttpClient(handlerMock.Object);
        var endpoint = "https://example.com/users";
        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });
        var service = new UsersService(httpClient, config);

        // Act
        var result = await service.GetAllUsers();

        // Assert
        result.Count().Should().Be(expectedResponse.Count);

    }
    
    [Fact]
    public async Task GetAllUsersWhenHits404Returns()
    {
        // Arrange
        var handlerMock = MockHttpMessageHandler<User>.SetupReturn404();
        var httpClient = new HttpClient(handlerMock.Object);
        var endpoint = "https://example.com/users";
        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });
        var service = new UsersService(httpClient, config);

        // Act
        var result = await service.GetAllUsers();

        // Assert
        result.Count().Should().Be(0);

    }
}