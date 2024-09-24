using Microsoft.AspNetCore.Mvc;
using Moq;
using Sample.Business.IServices.Admin;
using Sample.Data.DTO.Admin;
using Sample.Data.Models;
using Sample.Validators.IValidators.Admin;
using Sample.WebAPI.Controllers.Admin;

namespace Sample.UnitTest.Admin
{
    [TestFixture]
    public class UserTests
    {
        private Mock<IUserService> _userService;
        private Mock<IUserValidator> _userValidator;
        private UserController _userController;

        [SetUp]
        public void Setup()
        {
            _userService = new Mock<IUserService>();
            _userValidator = new Mock<IUserValidator>();
            _userController = new UserController(
                _userService.Object,
                _userValidator.Object
            );
        }

        [Test]
        public async Task Test_01_GetAllAsync()
        {
            var userId = Guid.NewGuid();

            var users = new List<UserDTO> {
                new UserDTO
                {
                    Id = userId,
                    FirstName = "FirstName",
                    MiddleName = "MiddleName",
                    LastName = "LastName",
                    FullName = null,
                    Age = 10,
                    EmailAddress = "sample@gmail.com",
                    Address = "ADDRESS",
                    CreatedBy = userId,
                    CreatedDate = DateTime.Now.ToString(),
                    UpdatedBy = null,
                    UpdatedDate = null,
                    IsEnabled = true
                },
                new UserDTO
                {
                    Id = userId,
                    FirstName = "FirstName1",
                    MiddleName = "MiddleName1",
                    LastName = "LastName1",
                    FullName = null,
                    Age = 10,
                    EmailAddress = "sample1@gmail.com",
                    Address = "ADDRESS1",
                    CreatedBy = userId,
                    CreatedDate = DateTime.Now.ToString(),
                    UpdatedBy = null,
                    UpdatedDate = null,
                    IsEnabled = true
                },
            };

            _userService.Setup(x => x.GetAllAsync()).ReturnsAsync(users);

            var actionResult = await _userController.GetAllAsync();
            var objectResult = actionResult.Result as ObjectResult;

            Assert.IsNotNull(objectResult, "Expected ObjectResult but got null.");
            Assert.IsInstanceOf<APIResponse<object>>(objectResult?.Value, "Expected APIResponse<object> but got a different type.");

            var responseObject = objectResult?.Value as APIResponse<object>;
            Assert.IsNotNull(responseObject, "APIResponse<object> was null.");
            Assert.IsTrue(responseObject.Success, "Expected Success to be true.");

            Assert.IsInstanceOf<List<UserDTO>>(responseObject.Result, "Expected Result to be of type List<UserDTO>.");
            var resultList = responseObject.Result as List<UserDTO>;
            Assert.IsNotNull(resultList, "Result list was null.");
            Assert.AreEqual(users.Count, resultList.Count, "User count does not match.");
        }

        [Test]
        public async Task Test_02_GetbyIdAsync()
        {
            var userId = Guid.NewGuid();

            var users = new UserDTO
            {
                Id = userId,
                FirstName = "FirstName",
                MiddleName = "MiddleName",
                LastName = "LastName",
                FullName = null,
                Age = 10,
                EmailAddress = "sample@gmail.com",
                Address = "ADDRESS",
                CreatedBy = userId,
                CreatedDate = DateTime.Now.ToString(),
                UpdatedBy = null,
                UpdatedDate = null,
                IsEnabled = true
            };

            _userService.Setup(x => x.GetbyIdAsync(userId)).ReturnsAsync(users);

            var actionResult = await _userController.GetbyIdAsync(userId);
            var objectResult = actionResult.Result as ObjectResult;

            Assert.IsNotNull(objectResult, "Expected ObjectResult but got null.");
            Assert.IsInstanceOf<APIResponse<object>>(objectResult?.Value, "Expected APIResponse<object> but got a different type.");

            var responseObject = objectResult?.Value as APIResponse<object>;
            Assert.IsNotNull(responseObject, "APIResponse<object> was null.");
            Assert.IsTrue(responseObject.Success, "Expected Success to be true.");

            var resultList = responseObject.Result as UserDTO;
            Assert.IsNotNull(resultList, "Result list was null.");
        }

        [Test]
        public async Task Test_03_Search()
        {
            var firstName = "FirstName";

            var users = new List<UserDTO> {
                new UserDTO
                {
                    Id = Guid.NewGuid(),
                    FirstName = "FirstName",
                    MiddleName = "MiddleName",
                    LastName = "LastName",
                    FullName = null,
                    Age = 10,
                    EmailAddress = "sample@gmail.com",
                    Address = "ADDRESS",
                    CreatedBy = Guid.NewGuid(),
                    CreatedDate = DateTime.Now.ToString(),
                    UpdatedBy = Guid.NewGuid(),
                    UpdatedDate = DateTime.Now.ToString(),
                    IsEnabled = true
                },
                new UserDTO
                {
                    Id = Guid.NewGuid(),
                    FirstName = "FirstName1",
                    MiddleName = "MiddleName1",
                    LastName = "LastName1",
                    FullName = null,
                    Age = 10,
                    EmailAddress = "sample1@gmail.com",
                    Address = "ADDRESS1",
                    CreatedBy = Guid.NewGuid(),
                    CreatedDate = DateTime.Now.ToString(),
                    UpdatedBy = Guid.NewGuid(),
                    UpdatedDate = DateTime.Now.ToString(),
                    IsEnabled = true
                },
            };

            _userService.Setup(x => x.Search(firstName)).ReturnsAsync(users);

            var actionResult = await _userController.Search(firstName);
            var objectResult = actionResult.Result as ObjectResult;

            Assert.IsNotNull(objectResult, "Expected ObjectResult but got null.");
            Assert.IsInstanceOf<APIResponse<object>>(objectResult?.Value, "Expected APIResponse<object> but got a different type.");

            var responseObject = objectResult?.Value as APIResponse<object>;
            Assert.IsNotNull(responseObject, "APIResponse<object> was null.");
            Assert.IsTrue(responseObject.Success, "Expected Success to be true.");
          
            Assert.IsInstanceOf<List<UserDTO>>(responseObject.Result, "Expected Result to be of type List<UserDTO>.");
            var resultList = responseObject.Result as List<UserDTO>;
            Assert.IsNotNull(resultList, "Result list was null.");
            Assert.AreEqual(users.Count, resultList.Count, "User count does not match.");
            Assert.That(users.Count, Is.EqualTo(resultList.Count), "User count does not match.");
        }

        [Test]
        public async Task Test_04_AddAsync()
        {
            var userId = Guid.NewGuid();

            var user = new UserDTO
            {
                Id = userId,
                FirstName = "FirstName",
                MiddleName = "MiddleName",
                LastName = "LastName",
                FullName = null,
                Age = 10,
                EmailAddress = "sample@gmail.com",
                Address = "ADDRESS",
                CreatedBy = userId,
                CreatedDate = DateTime.Now.ToString(),
                UpdatedBy = null,
                UpdatedDate = null,
                IsEnabled = true
            };

            _userValidator.Setup(x => x.ValidateAsync(user, HttpMethod.Post))
                          .Returns(Task.FromResult(new List<(int, string)>()));

            _userService.Setup(x => x.Search(user.EmailAddress))
                        .Returns(Task.FromResult(new List<UserDTO>()));

            _userService.Setup(x => x.AddAsync(user))
                        .Returns(Task.CompletedTask);

            var controller = new UserController(_userService.Object, _userValidator.Object);

            var actionResult = await controller.AddAsync(user);
            var objectResult = actionResult.Result as ObjectResult;

            Assert.IsNotNull(objectResult, "Expected ObjectResult but got null.");
            Assert.IsInstanceOf<APIResponse<object>>(objectResult?.Value, "Expected APIResponse<object> but got a different type.");

            var responseObject = objectResult?.Value as APIResponse<object>;
            Assert.IsNotNull(responseObject, "APIResponse<object> was null.");
            Assert.IsTrue(responseObject.Success, "Expected Success to be true.");
        }

        [Test]
        public async Task Test_05_UpdateAsyncAsync()
        {
            var userId = Guid.NewGuid();

            var user = new UserDTO
            {
                Id = userId,
                FirstName = "FirstName",
                MiddleName = "MiddleName",
                LastName = "LastName",
                FullName = null,
                Age = 10,
                EmailAddress = "sample@gmail.com",
                Address = "ADDRESS",
                CreatedBy = userId,
                CreatedDate = DateTime.Now.ToString(),
                UpdatedBy = null,
                UpdatedDate = null,
                IsEnabled = true
            };

            _userValidator.Setup(x => x.ValidateAsync(user, HttpMethod.Put))
                          .Returns(Task.FromResult(new List<(int, string)>()));

            _userService.Setup(x => x.UpdateAsync(user)).Returns(Task.CompletedTask);

            var controller = new UserController(_userService.Object, _userValidator.Object);

            var actionResult = await controller.UpdateAsync(user);
            var objectResult = actionResult.Result as ObjectResult;

            Assert.IsNotNull(objectResult, "Expected ObjectResult but got null.");
            Assert.IsInstanceOf<APIResponse<object>>(objectResult?.Value, "Expected APIResponse<object> but got a different type.");

            var responseObject = objectResult?.Value as APIResponse<object>;
            Assert.IsNotNull(responseObject, "APIResponse<object> was null.");
            Assert.IsTrue(responseObject.Success, "Expected Success to be true.");
        }

        [Test]
        public async Task Test_06_DeleteAsyncAsync()
        {
            var userId = Guid.NewGuid();

            var user = new UserDTO
            {
                Id = userId,
                FirstName = "FirstName",
                MiddleName = "MiddleName",
                LastName = "LastName",
                FullName = null,
                Age = 10,
                EmailAddress = "sample@gmail.com",
                Address = "ADDRESS",
                CreatedBy = userId,
                CreatedDate = DateTime.Now.ToString(),
                UpdatedBy = null,
                UpdatedDate = null,
                IsEnabled = true
            };

            _userValidator.Setup(x => x.ValidateAsync(user, HttpMethod.Delete))
                          .Returns(Task.FromResult(new List<(int, string)>()));

            _userService.Setup(x => x.DeleteAsync(user.Id))
                        .Returns(Task.CompletedTask);

            var controller = new UserController(_userService.Object, _userValidator.Object);

            var actionResult = await controller.DeleteAsync(user);
            var objectResult = actionResult.Result as ObjectResult;

            Assert.IsNotNull(objectResult, "Expected ObjectResult but got null.");
            Assert.IsInstanceOf<APIResponse<object>>(objectResult?.Value, "Expected APIResponse<object> but got a different type.");

            var responseObject = objectResult?.Value as APIResponse<object>;
            Assert.IsNotNull(responseObject, "APIResponse<object> was null.");
            Assert.IsTrue(responseObject.Success, "Expected Success to be true.");
        }
    }
}