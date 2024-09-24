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
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {

        private readonly IDepartmentService _userService;
        private readonly IDepartmentValidator _userValidator;

        public DepartmentController(IDepartmentService userService, IDepartmentValidator userValidator)
        {
            _userService = userService;
            _userValidator = userValidator;
        }

        #region get
        [HttpGet("[action]")] 
        public async Task<ActionResult<APIResponse<object>>> GetAllAsync()
        {
            var apiResponse = new APIResponse<object>(); // 
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

        [HttpGet("[action]/{departmentID}")] // uses function name,
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

        [HttpGet("[action]/{departmentName}")] // uses function name,
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

        //#endregion
        //#region action

        [HttpPost("[action]")] // uses function name,
        public async Task<ActionResult<APIResponse<object>>> AddAsync(DepartmentDTO user)
        {
            var apiResponse = new APIResponse<object>();
            var response = new ObjectResult(apiResponse);

            try
            {
                // validators here
                var isValid = await _userValidator.ValidateAsync(user, HttpMethod.Post);

                if (isValid.FirstOrDefault().Item1 == 400)
                {
                    apiResponse.Success = false;
                    apiResponse.Result = null;
                    response = StatusCode(isValid.FirstOrDefault().Item1, isValid.FirstOrDefault().Item2);
                }
                else if (isValid.FirstOrDefault().Item1 == 200)
                {
                    var userSearch = await _userService.Search(user.DepartmentName);
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
        public async Task<ActionResult<APIResponse<object>>> UpdateAsync(DepartmentDTO user)
        {
            var apiResponse = new APIResponse<object>();
            var response = new ObjectResult(apiResponse);

            try
            {
                // validators here
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

            return apiResponse;
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult<APIResponse<object>>> DeleteAsync(DepartmentDTO user)
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
                    await _userService.DeleteAsync(user.DepartmentID);
                    apiResponse.Success = true;
                    apiResponse.Result = null;
                    response = Ok(apiResponse);
                }
                // validators here
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Result = ex;

                response = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return response;
        }
        //#endregion


        //#region private
        #endregion
    }
}
