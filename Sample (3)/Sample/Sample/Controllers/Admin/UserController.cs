using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Sample.Business.IServices.Admin;
using Sample.Data.DTO.Admin;
using Sample.Data.Models;
using Sample.Validator.IValidators.Admin;
using System.Net.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Sample.Controllers.Admin
{
    [Authorize] // NOTE : Attribute, sets each function with requirement of user authorization.
    [ApiController]
    // attribute, https://stackoverflow.com/questions/66545845/what-does-the-apicontroller-attribute-do
    [Route("api/[controller]")]
    // attribute, usually used with [controller] to automatically get class name excluding the `controller` word
    //      but can use string literal instead
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IUserValidator _userValidator;

        public UserController(IUserService userService, IUserValidator userValidator)
        {
            _userService = userService;
            _userValidator = userValidator;
        }

        #region get
        [HttpGet("[action]")] 
        // uses function name
        // conists of the http methods. parameter is usually [action] (which can be accompanied by {id} if its get)
        //      but can use string literal instead
        public async Task<ActionResult<APIResponse<object>>> GetAllAsync()
        {
            var apiResponse = new APIResponse<object>();
            var response = new ObjectResult(apiResponse);

            try
            {
                var data = await _userService.GetAllAsync();
                if (data != null)
                {
                    apiResponse.Success = true;
                    apiResponse.Result = data;

                    response = Ok(apiResponse);
                }
                else
                {
                    apiResponse.Success = false;
                    apiResponse.Result = data;

                    response = BadRequest(apiResponse);
                }
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Result = ex;

                response = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return response;
        }

        [HttpGet("[action]/{id}")] // uses function name,
        public async Task<ActionResult<APIResponse<object>>> GetByIdAsync(Guid id)
        {
            var apiResponse = new APIResponse<object>();
            var response = new ObjectResult(apiResponse);

            try
            {
                var data = await _userService.GetByIdAsync(id);
                if (data != null)
                {
                    apiResponse.Success = true;
                    apiResponse.Result = data;

                    response = Ok(apiResponse);
                }
                else
                {
                    apiResponse.Success = false;
                    apiResponse.Result = data;

                    response = BadRequest(apiResponse);
                }
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Result = ex;

                response = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return response;
        }

        [HttpGet("[action]/{firstName}")] // uses function name,
        public async Task<ActionResult<APIResponse<object>>> Search(string firstName)
        {
            var apiResponse = new APIResponse<object>();
            var response = new ObjectResult(apiResponse);

            try
            {
                var data = await _userService.Search(firstName);
                if (data != null)
                {
                    apiResponse.Success = true;
                    apiResponse.Result = data;

                    response = Ok(apiResponse);
                }
                else
                {
                    apiResponse.Success = false;
                    apiResponse.Result = data;

                    response = BadRequest(apiResponse);
                }
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Result = ex;

                response = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return response;
        }

        #endregion
        #region action

        [HttpPost("[action]")] // uses function name,
        public async Task<ActionResult<APIResponse<object>>> AddAsync(UserDTO user)
        {
            var apiResponse = new APIResponse<object>();
            var response = new ObjectResult(apiResponse);

            try
            {
                var isValid = await _userValidator.ValidateAsync(user, HttpMethod.Post); // NOTE : Validator starts here...

                if (isValid.FirstOrDefault().Item1 == 400)
                {
                    apiResponse.Success = false;
                    apiResponse.Result = null;
                    response = StatusCode(isValid.FirstOrDefault().Item1, isValid.FirstOrDefault().Item2);
                }                
                else if (isValid.FirstOrDefault().Item1 == 200)
                {
                    var userSearch = await _userService.Search(user.EmailAddress);
                    await _userService.UpdateAsync(userSearch.FirstOrDefault());

                    apiResponse.Success = true;
                    apiResponse.Result = null;
                    response = StatusCode(isValid.FirstOrDefault().Item1, isValid.FirstOrDefault().Item2);
                }
                else
                {
                    await _userService.AddAsync(user);
                    apiResponse.Success = true;
                    apiResponse.Result = null;
                    response = Ok(apiResponse);
                }
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Result = ex;

                response = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return response;
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<APIResponse<object>>> UpdateAsync(UserDTO user)
        {
            var apiResponse = new APIResponse<object>();
            var response = new ObjectResult(apiResponse);

            try
            {
                var isValid = await _userValidator.ValidateAsync(user, HttpMethod.Put);

                if (isValid.FirstOrDefault().Item1 == 400)
                {
                    apiResponse.Success = false;
                    apiResponse.Result = null;
                    response = StatusCode(isValid.FirstOrDefault().Item1, isValid.FirstOrDefault().Item2);
                }
                else
                {
                    await _userService.UpdateAsync(user);
                    apiResponse.Success = true;
                    apiResponse.Result = null;
                    response = Ok(apiResponse);
                }
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Result = ex;

                response = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return response;
        }

        [HttpDelete("[action]")] 
        public async Task<ActionResult<APIResponse<object>>> DeleteAsync(UserDTO user)
        {
            var apiResponse = new APIResponse<object>();
            var response = new ObjectResult(apiResponse);

            try
            {

                var isValid = await _userValidator.ValidateAsync(user, HttpMethod.Delete);

                if(isValid.FirstOrDefault().Item1 == 400)
                {
                    apiResponse.Success = false;
                    apiResponse.Result = null;
                    response = StatusCode(isValid.FirstOrDefault().Item1, isValid.FirstOrDefault().Item2);
                }
                else
                {
                    await _userService.DeleteAsync(user.Id);
                    apiResponse.Success = true;
                    apiResponse.Result = null;
                    response = Ok(apiResponse);
                }
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Result = ex;

                response = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return response;
        }
        #endregion


        #region private

        // NOTE : This is used for a sample endpoint for easy checking of whether or not the user is authorized (the JWT works)
        [HttpGet("[action]")]
        public IActionResult GetSecureData()
        {
            return Ok("This is a secure endpoint.");
        }
        #endregion
    }
}
