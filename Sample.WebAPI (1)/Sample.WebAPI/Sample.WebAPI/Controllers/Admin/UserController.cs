using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Sample.Business.IServices.Admin;
using Sample.Data.DTO.Admin;
using Sample.Data.Models;
using Sample.Validators.IValidators.Admin;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Sample.WebAPI.Controllers.Admin
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserValidator _userValidator;
        public UserController(IUserService userService, IUserValidator userValidator) { 
            _userService = userService;
            _userValidator = userValidator;
        }

        #region Get
        [HttpGet("[action]")]
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

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<APIResponse<object>>> GetbyIdAsync(Guid id)
        {
            var apiResponse = new APIResponse<object>();
            var response = new ObjectResult(apiResponse);

            try
            {
                var data = await _userService.GetbyIdAsync(id);

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

        [HttpGet("[action]/{firstName}")]
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

        #region Action
        [HttpPost("[action]")]
        public async Task<ActionResult<APIResponse<object>>> AddAsync(UserDTO user)
        {
            var apiResponse = new APIResponse<object>();
            var response = new ObjectResult(apiResponse);

            try
            {
                var isValid = await _userValidator.ValidateAsync(user, HttpMethod.Post);

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

        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult<APIResponse<object>>> DeleteAsync(UserDTO user)
        {
            var apiResponse = new APIResponse<object>();
            var response = new ObjectResult(apiResponse);

            try
            {
                var isValid = await _userValidator.ValidateAsync(user, HttpMethod.Delete);

                if (isValid.FirstOrDefault().Item1 == 400)
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

        #region Private
        [HttpGet("[action]")]
        public IActionResult GetSecureData()
        {
            return Ok("This is a secure endpoint.");
        }
        #endregion
    }
}
