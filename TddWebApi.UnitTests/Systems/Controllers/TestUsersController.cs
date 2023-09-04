using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TddWebApi.Controllers;
using TddWebApi.Models;
using TddWebApi.Services;
using TddWebApi.UnitTests.Fixtures;
using Xunit;

namespace TddWebApi.UnitTests.Systems.Controllers;

public class TestUsersController
{
    [Fact]
    public async Task GetOnSuccessReturns200StatusCode()
    {
        // Arrange
        var mockUsersService = new Mock<IUsersService>();
        mockUsersService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(UsersFixture.GetTestUsers());
        
        var controller = new UsersController(mockUsersService.Object);
        
        // Act
        var controllerResult = (OkObjectResult)await controller.Get();
        
        // Assert
        
        controllerResult.StatusCode.Should().Be(200);
        
    }

    [Fact]
    public async Task GetOnSuccessReturnsInvokeUsersServiceExactlyOnce()
    {
         // Arrange
         var mockUsersService = new Mock<IUsersService>();
         
         mockUsersService
             .Setup(service => service.GetAllUsers())
             .ReturnsAsync(new List<User>());
         
         var controller = new UsersController(mockUsersService.Object);
         
         // Act
         var controllerResult = await controller.Get();
        
         // Assert
         mockUsersService.Verify(
             service => service.GetAllUsers(),
             Times.Once
        );
         
    }

    [Fact]
    public async Task GetOnSuccessReturnsListOfUsers()
    {
        // Arrange
        var mockUsersService = new Mock<IUsersService>();
        mockUsersService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(UsersFixture.GetTestUsers());

        var controller = new UsersController(mockUsersService.Object);
        
        // Act
        var controllerResult = await controller.Get();

        // Assert
        controllerResult.Should().BeOfType<OkObjectResult>();
        var okObjectResult = (OkObjectResult) controllerResult;
        okObjectResult.Value.Should().BeOfType<List<User>>();


    }
    
    [Fact]
    public async Task GetOnNoUsersFoundReturns404()
    {
        // Arrange
        var mockUsersService = new Mock<IUsersService>();
        mockUsersService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());

        var controller = new UsersController(mockUsersService.Object);
        
        // Act
        var controllerResult = await controller.Get();

        // Assert
        controllerResult.Should().BeOfType<NotFoundResult>();
        var notFoundResult = (NotFoundResult) controllerResult;
        notFoundResult.StatusCode.Should().Be(404);


    }
    
}