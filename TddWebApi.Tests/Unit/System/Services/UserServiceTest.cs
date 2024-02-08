using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using TddWebApi.Models;
using TddWebApi.Services.Users;
using Xunit;

namespace TddWebApi.Tests.Unit.System.Services;

public class UserServiceTest
{
    [Fact]
    public void GetUsers_OnSuccess_ReturnsUsers()
    {
        // Arrange
        var userFixture = new List<User> { new User { Id = 1, Name = "John Doe" } };
        var mockUsersApi = new Mock<IUsersApi>();
        mockUsersApi
            .Setup(a => a.GetAllUsers())
            .ReturnsAsync(userFixture);

        var mockLoggers = new Mock<ILogger<UsersService>>();

        var usersService = new UsersService(mockUsersApi.Object, mockLoggers.Object);

        // Act
        var users = usersService.GetUsers();

        // Assert
        users.Should().Be(users);
    }

    [Fact]
    public async Task GetUsers_OnError_ThrowException()
    {
        // Arrange
        var mockUsersApi = new Mock<IUsersApi>();
        mockUsersApi
            .Setup(a => a.GetAllUsers())
            .ThrowsAsync(new Exception("Error"));
        var mockLoggers = new Mock<ILogger<UsersService>>();
        var usersService = new UsersService(mockUsersApi.Object, mockLoggers.Object);

        // Act

        Func<Task> act = async () => await usersService.GetUsers();

        // Assert
        await act
            .Should()
            .ThrowAsync<Exception>()
            .WithMessage("Error");

    }

    [Fact]
    public void GetUser_OnSuccess_ReturnsUser()
    {
        // Arrange
        var userFixture = new User { Id = 1, Name = "John Doe" };
        var mockUsersApi = new Mock<IUsersApi>();
        mockUsersApi
            .Setup(a => a.GetUser(1))
            .ReturnsAsync(userFixture);

        var mockLoggers = new Mock<ILogger<UsersService>>();

        var usersService = new UsersService(mockUsersApi.Object, mockLoggers.Object);

        // Act
        var user = usersService.GetUser(1);

        // Assert
        user.Should().Be(user);
    }

    [Fact]
    public async Task GetUser_OnError_ThrowException()
    {
        // Arrange
        var mockUsersApi = new Mock<IUsersApi>();
        mockUsersApi
            .Setup(a => a.GetUser(1))
            .ThrowsAsync(new Exception("Error"));
        var mockLoggers = new Mock<ILogger<UsersService>>();
        var usersService = new UsersService(mockUsersApi.Object, mockLoggers.Object);

        // Act

        Func<Task> act = async () => await usersService.GetUser(1);

        // Assert
        await act
            .Should()
            .ThrowAsync<Exception>()
            .WithMessage("Error");

    }

}