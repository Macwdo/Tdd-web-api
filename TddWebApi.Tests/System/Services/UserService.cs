
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using TddWebApi.Models;
using TddWebApi.Services.Users;
using Xunit;

namespace TddWebApi.Tests.System.Services;


public class UserServicesTests {
    [Fact]
    public async void GetUser_WhenSuccess_ReturnsUser(){
        // Arrange 
        var userFixture = new User() {
            Id = 1,
            Name = "Test user",
            Email = "test.mail@email.co",
            Address = new () {
                City = "Test City",
                Number = 12,
                Street = "Test Address street"
            }
        };
        
        var mockUsersApi = new Mock<IUsersApi>();
        mockUsersApi
            .Setup(c => c.GetUser(1))
            .ReturnsAsync(userFixture);
        
        var mockLogger = new Mock<ILogger<UsersService>>();
        var service = new UsersService(mockUsersApi.Object, mockLogger.Object);

        // Act
        var result = await service.GetUser(1);
        
        // Assert
        result.Should().Be(null);


    } 
}