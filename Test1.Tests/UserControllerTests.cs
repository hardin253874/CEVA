using Xunit;
using Moq;
using Test1.Controllers;
using Test1.Services;
using Test1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Test1.Tests
{
    public class UserControllerTests
    {
        private readonly Mock<IUsers> _mockService;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _mockService = new Mock<IUsers>();
            _controller = new UserController(_mockService.Object);
        }

        [Fact]
        public void GetAllUsers_ReturnsOkResult_WithUsers()
        {
            // Arrange
            var users = new List<User> { new User { ID = 1, EmployeeID = "EMP1" } };
            _mockService.Setup(s => s.GetAll()).Returns(users);

            // Act
            var result = _controller.GetAllUsers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnUsers = Assert.IsAssignableFrom<IEnumerable<User>>(okResult.Value);
            Assert.Single(returnUsers);
        }

        [Fact]
        public void GetUserById_ExistingId_ReturnsUser()
        {
            var user = new User { ID = 1 };
            _mockService.Setup(s => s.GetById(1)).Returns(user);

            var result = _controller.GetUserById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnUser = Assert.IsType<User>(okResult.Value);
            Assert.Equal(1, returnUser.ID);
        }

        [Fact]
        public void GetUserById_NotFound_Returns404()
        {
            _mockService.Setup(s => s.GetById(2)).Returns((User?)null);

            var result = _controller.GetUserById(2);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void CreateUser_ReturnsCreatedUser()
        {
            var newUser = new User { EmployeeID = "NEW" };
            _mockService.Setup(s => s.Create(It.IsAny<User>())).Returns(new User { ID = 99, EmployeeID = "NEW" });

            var result = _controller.CreateUser(newUser);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnUser = Assert.IsType<User>(createdResult.Value);
            Assert.Equal(99, returnUser.ID);
        }

        [Fact]
        public void UpdateUser_Success_ReturnsNoContent()
        {
            _mockService.Setup(s => s.Update(1, It.IsAny<User>())).Returns(true);

            var result = _controller.UpdateUser(1, new User());

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateUser_NotFound_Returns404()
        {
            _mockService.Setup(s => s.Update(1, It.IsAny<User>())).Returns(false);

            var result = _controller.UpdateUser(1, new User());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteUser_Success_ReturnsNoContent()
        {
            _mockService.Setup(s => s.Delete(1)).Returns(true);

            var result = _controller.DeleteUser(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteUser_NotFound_Returns404()
        {
            _mockService.Setup(s => s.Delete(1)).Returns(false);

            var result = _controller.DeleteUser(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetUsersByEmployeeId_Found_ReturnsUsers()
        {
            _mockService.Setup(s => s.GetByEmployeeId("EMPX")).Returns(new List<User> { new User { ID = 3, EmployeeID = "EMPX" } });

            var result = _controller.GetUsersByEmployeeId("EMPX");

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var data = Assert.IsAssignableFrom<IEnumerable<User>>(ok.Value);
            Assert.Single(data);
        }

        [Fact]
        public void GetUsersByEmployeeId_NotFound_Returns404()
        {
            _mockService.Setup(s => s.GetByEmployeeId("NOTFOUND")).Returns(new List<User>());

            var result = _controller.GetUsersByEmployeeId("NOTFOUND");

            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
