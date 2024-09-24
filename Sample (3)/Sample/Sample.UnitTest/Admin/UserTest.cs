using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Sample.Business.IServices.Admin;
using Sample.Controllers.Admin;
using Sample.Data.DTO.Admin;
using Sample.Data.Entities;
using Sample.Data.Models;
using Sample.Validator.IValidators.Admin;
using System.Net;

namespace Sample.UnitTest.Admin
{
    [TestFixture]
    public class UserTests
    {

        //for testing, usually add all of the interfaces 
        private Mock<IUserService> _UserService;
        private Mock<IUserValidator> _UserValidator;
        //private IUserService _userService;
        private UserController _userController;
        private HttpRequestMessage _request;

        [SetUp]
        public void Setup()
        {
            _UserService = new Mock<IUserService>();
            _UserValidator = new Mock<IUserValidator>();
            
            // all methods in the said controller class is called and tested in the unit test
            _userController = new UserController(
                _UserService.Object,
                _UserValidator.Object
                );
            
            // unit testing = dummy data
            // integration testing = actual data

            // unit testing > qa (or dev)
            // integration testing > qa

            // unit testing > integration testing > go signal (connect to ui)
            // *functional testing = 

            //_userController;
        }

        [Test]
        public async Task Test001_GetAllAsync()
        {
            
            var userId = Guid.NewGuid();

            var users = new List<UserDTO>
            {
                new UserDTO
            {
                Id = userId,
                FirstName = "Thomas",
                MiddleName = "Lee",
                LastName = "Guzman",
                Age = 99,
                EmailAddress = "tlg@gmail.com",
                Address = "New York, NY",
                CreatedBy = userId,
                CreatedDate = DateTime.Now.ToString(),
                IsEnabled = true
            },
                new UserDTO
            {
                Id = userId,
                FirstName = "John",
                MiddleName = "Vance",
                LastName = "DeWitt",
                Age = 45,
                EmailAddress = "JVD@gmail.com",
                Address = "New York, NY",
                CreatedBy = userId,
                CreatedDate = DateTime.Now.ToString(),
                IsEnabled = true

            }

            };

            //var user = new UserInfo()
            //{
            //    Id = userId,
            //    FirstName = "Thomas",
            //    MiddleName = "Lee",
            //    LastName = "Guzman",
            //    Age = 99,
            //    EmailAddress = "tlg@gmail.com",
            //    Address = "New York, NY",
            //    CreatedBy = userId,
            //    CreatedDate = DateTime.Now,
            //    IsEnabled = true

            //};

            // sets up as if users object is the database for the mock,
            // NOTE : should the setup be done outside?
            _UserService.Setup(x => x.GetAllAsync()).ReturnsAsync(users);

            var response = await _userController.GetAllAsync();
            var objectResult = response.Result as ObjectResult;
            
            Assert.IsNotNull(objectResult);
            Assert.IsInstanceOf<APIResponse<object>>(objectResult?.Value, "Hello World");

            var responseObject = objectResult?.Value as APIResponse<object>;
            Assert.IsNotNull(responseObject);
            Assert.IsTrue(responseObject.Success);

            Assert.IsInstanceOf<List<UserDTO>>(responseObject.Result);
            var resultList = responseObject.Result as List<UserDTO>;
            Assert.IsNotNull(resultList);
            Assert.AreEqual(users.Count, resultList.Count);


        }


        [Test]
        public async Task Test002_GetByIdAsync()
        {

            var userId = Guid.NewGuid();

            //var users = new List<UserDTO>
            //{
            //    new UserDTO
            //{
            //    Id = userId,
            //    FirstName = "Thomas",
            //    MiddleName = "Lee",
            //    LastName = "Guzman",
            //    Age = 99,
            //    EmailAddress = "tlg@gmail.com",
            //    Address = "New York, NY",
            //    CreatedBy = userId,
            //    CreatedDate = DateTime.Now.ToString(),
            //    IsEnabled = true
            //},
            //    new UserDTO
            //{
            //    Id = userId,
            //    FirstName = "John",
            //    MiddleName = "Vance",
            //    LastName = "DeWitt",
            //    Age = 45,
            //    EmailAddress = "JVD@gmail.com",
            //    Address = "New York, NY",
            //    CreatedBy = userId,
            //    CreatedDate = DateTime.Now.ToString(),
            //    IsEnabled = true

            //}

            //};

            var user = new UserDTO()
            {
                Id = userId,
                FirstName = "Thomas",
                MiddleName = "Lee",
                LastName = "Guzman",
                Age = 99,
                EmailAddress = "tlg@gmail.com",
                Address = "New York, NY",
                CreatedBy = userId,
                CreatedDate = DateTime.Now.ToString(),
                IsEnabled = true

            };

            // sets up as if users object is the database for the mock,
            // NOTE : should the setup be done outside?
            // returns > 
            _UserService.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync(user);
            //_UserService.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync(user);


            var response = await _userController.GetByIdAsync(userId);
            var objectResult = response.Result as ObjectResult;

            Assert.IsNotNull(objectResult);
            Assert.IsInstanceOf<APIResponse<object>>(objectResult?.Value, "Hello World");

            var responseObject = objectResult?.Value as APIResponse<object>;
            Assert.IsNotNull(responseObject);
            Assert.IsTrue(responseObject.Success);

            //Assert.IsInstanceOf<UserDTO>(responseObject.Result);
            var resultList = responseObject.Result as UserDTO;
            Assert.IsNotNull(resultList);
            //Assert.AreEqual(users.Count, resultList.Count);



        }
        [Test]
        public async Task Test003_Search()
        {

            var firstName = "John1";

            var userId = Guid.NewGuid();

            var users = new List<UserDTO>
            {
                new UserDTO
            {
                Id = userId,
                FirstName = "Thomas",
                MiddleName = "Lee",
                LastName = "Guzman",
                Age = 99,
                EmailAddress = "tlg@gmail.com",
                Address = "New York, NY",
                CreatedBy = userId,
                CreatedDate = DateTime.Now.ToString(),
                IsEnabled = true
            },
                new UserDTO
            {
                Id = userId,
                FirstName = "John",
                MiddleName = "Vance",
                LastName = "DeWitt",
                Age = 45,
                EmailAddress = "JVD@gmail.com",
                Address = "New York, NY",
                CreatedBy = userId,
                CreatedDate = DateTime.Now.ToString(),
                IsEnabled = true

            }

            };

            //var user = new UserDTO()
            //{
            //    Id = userId,
            //    FirstName = "Thomas",
            //    MiddleName = "Lee",
            //    LastName = "Guzman",
            //    Age = 99,
            //    EmailAddress = "tlg@gmail.com",
            //    Address = "New York, NY",
            //    CreatedBy = userId,
            //    CreatedDate = DateTime.Now.ToString(),
            //    IsEnabled = true

            //};

            try
            {


            _UserService.Setup(x => x.Search(firstName)).ReturnsAsync(users);

            var response = await _userController.Search(firstName);
            var objectResult = response.Result as ObjectResult;

            Assert.IsNotNull(objectResult);
            Assert.IsInstanceOf<APIResponse<object>>(objectResult?.Value);

            var responseObject = objectResult?.Value as APIResponse<object>;
            Assert.IsNotNull(responseObject);
            Assert.IsTrue(responseObject.Success);

            Assert.IsInstanceOf<List<UserDTO>>(responseObject.Result);
            var resultList = responseObject.Result as List<UserDTO>;
            Assert.IsNotNull(resultList);
            Assert.AreEqual(users.Count, resultList.Count);
            Assert.That(users.Count, Is.EqualTo(resultList.Count));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }




        }

        [Test]
        public async Task Test004_AddAsync()
        {

            var firstName = "John1";

            var userId = Guid.NewGuid();



            var user = new UserDTO()
            {
                Id = userId,
                FirstName = "Thomas",
                MiddleName = "Lee",
                LastName = "Guzman",
                Age = 99,
                EmailAddress = "tlg@gmail.com",
                Address = "New York, NY",
                CreatedBy = userId,
                CreatedDate = DateTime.Now.ToString(),
                IsEnabled = true

            };


            //_UserService.Setup(x => x.Search(firstName)).ReturnsAsync(users);
            _UserValidator.Setup(x => x.ValidateAsync(user, HttpMethod.Post))
                .Returns(Task.FromResult(new List<(int, string)>()));

            _UserService.Setup(x => x.Search(user.EmailAddress))
                .Returns(Task.FromResult(new List<UserDTO>()));

            _UserService.Setup(x => x.AddAsync(user)).Returns(Task.CompletedTask);

            var controller = new UserController(_UserService.Object, _UserValidator.Object);

                var response = await controller.AddAsync(user);
                var objectResult = response.Result as ObjectResult;

                Assert.IsNotNull(objectResult);
                Assert.IsInstanceOf<APIResponse<object>>(objectResult?.Value);

                var responseObject = objectResult?.Value as APIResponse<object>;
                Assert.IsNotNull(responseObject);
                Assert.IsTrue(responseObject.Success);

                //Assert.IsInstanceOf<UserDTO>(responseObject.Result);
                //var resultList = responseObject.Result as UserDTO;
                //Assert.IsNotNull(resultList);
            //    Assert.AreEqual(user.Count, resultList.Count);
            //    Assert.That(users.Count, Is.EqualTo(resultList.Count));
            try
            {


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }




        }


        [Test]
        public async Task Test005_UpdateAsync()
        {

            var firstName = "John1";

            var userId = Guid.NewGuid();

            //var users = new List<UserDTO>
            //{
            //    new UserDTO
            //{
            //    Id = userId,
            //    FirstName = "Thomas",
            //    MiddleName = "Lee",
            //    LastName = "Guzman",
            //    Age = 99,
            //    EmailAddress = "tlg@gmail.com",
            //    Address = "New York, NY",
            //    CreatedBy = userId,
            //    CreatedDate = DateTime.Now.ToString(),
            //    IsEnabled = true
            //},
            //    new UserDTO
            //{
            //    Id = userId,
            //    FirstName = "John",
            //    MiddleName = "Vance",
            //    LastName = "DeWitt",
            //    Age = 45,
            //    EmailAddress = "JVD@gmail.com",
            //    Address = "New York, NY",
            //    CreatedBy = userId,
            //    CreatedDate = DateTime.Now.ToString(),
            //    IsEnabled = true

            //}

            //};

            var user = new UserDTO()
            {
                Id = userId,
                FirstName = "Thomas",
                MiddleName = "Lee",
                LastName = "Guzman",
                Age = 99,
                EmailAddress = "tlg@gmail.com",
                Address = "New York, NY",
                CreatedBy = userId,
                CreatedDate = DateTime.Now.ToString(),
                IsEnabled = true

            };


            //_UserService.Setup(x => x.Search(firstName)).ReturnsAsync(users);
            _UserValidator.Setup(x => x.ValidateAsync(user, HttpMethod.Put))
                .Returns(Task.FromResult(new List<(int, string)>()));

            //_UserService.Setup(x => x.Search(user.EmailAddress))
            //    .Returns(Task.FromResult(new List<UserDTO>()));

            _UserService.Setup(x => x.UpdateAsync(user)).Returns(Task.CompletedTask);

            var controller = new UserController(_UserService.Object, _UserValidator.Object);

            var response = await controller.UpdateAsync(user);
            var objectResult = response.Result as ObjectResult; // should be result...

            Assert.IsNotNull(objectResult?.Value);
            Assert.IsInstanceOf<APIResponse<object>>(objectResult?.Value);

            var responseObject = objectResult?.Value as APIResponse<object>;
            Assert.IsNotNull(responseObject);
            Assert.IsTrue(responseObject.Success);

            //Assert.IsInstanceOf<List<UserDTO>>(responseObject.Result);
            var resultList = responseObject.Result as List<UserDTO>;
            //Assert.IsNotNull(resultList);
            //    Assert.AreEqual(user.Count, resultList.Count);
            //    Assert.That(users.Count, Is.EqualTo(resultList.Count));

        }

        [Test]
        public async Task Test006_DeleteAsync()
        {

            var firstName = "John1";

            var userId = Guid.NewGuid();

            var user = new UserDTO()
            {
                Id = userId,
                FirstName = "Thomas",
                MiddleName = "Lee",
                LastName = "Guzman",
                Age = 99,
                EmailAddress = "tlg@gmail.com",
                Address = "New York, NY",
                CreatedBy = userId,
                CreatedDate = DateTime.Now.ToString(),
                IsEnabled = true

            };


            //_UserService.Setup(x => x.Search(firstName)).ReturnsAsync(users);
            _UserValidator.Setup(x => x.ValidateAsync(user, HttpMethod.Delete))
                .Returns(Task.FromResult(new List<(int, string)>()));

            //_UserService.Setup(x => x.Search(user.EmailAddress))
            //    .Returns(Task.FromResult(new List<UserDTO>()));

            _UserService.Setup(x => x.DeleteAsync(userId)).Returns(Task.CompletedTask);

            var controller = new UserController(_UserService.Object, _UserValidator.Object);

            var response = await controller.DeleteAsync(user);
            var objectResult = response.Result as ObjectResult; // should be result...

            Assert.IsNotNull(objectResult?.Value);
            Assert.IsInstanceOf<APIResponse<object>>(objectResult?.Value);

            var responseObject = objectResult?.Value as APIResponse<object>;
            Assert.IsNotNull(responseObject);
            Assert.IsTrue(responseObject.Success);

            //Assert.IsInstanceOf<List<UserDTO>>(responseObject.Result);
            var resultList = responseObject.Result as List<UserDTO>;
            //Assert.IsNotNull(resultList);
            //    Assert.AreEqual(user.Count, resultList.Count);
            //    Assert.That(users.Count, Is.EqualTo(resultList.Count));

        }

    }
}