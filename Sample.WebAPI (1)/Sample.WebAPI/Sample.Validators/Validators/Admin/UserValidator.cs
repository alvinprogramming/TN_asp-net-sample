using Microsoft.AspNetCore.Http;
using Sample.Business.IServices.Admin;
using Sample.Data.DTO.Admin;
using Sample.Data.Entities;
using Sample.Validators.IValidators.Admin;

namespace Sample.Validators.Validators.Admin
{
    public class UserValidator : IUserValidator
    {
        readonly IUserService _userService;

        public UserValidator(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<List<(int, string)>> ValidateAsync(UserDTO entity, HttpMethod httpMethod)
        {
            var errorList = new List<(int, string)>();

            switch (httpMethod)
            {
                // Add / Post
                case HttpMethod m when m == HttpMethod.Post:
                    {
                        var users = await _userService.GetAllAsync();
                        errorList = ValidateUser(users, entity, "add");
                    }
                    break;
                // Edit / Put
                case HttpMethod m when m == HttpMethod.Put:
                    {
                        var users = await _userService.GetAllAsync();
                        errorList = ValidateUser(users, entity, "edit");
                    }
                    break;
                // Remove / Restore
                case HttpMethod m when m == HttpMethod.Delete:
                    {
                        var users = await _userService.GetAllAsync();
                        errorList = ValidateUser(users, entity, "delete");
                    }
                    break;
            }

            return errorList;
        }

        private List<(int, string)> ValidateUser(List<UserDTO> users, UserDTO entity, string action)
        {
            var errorList = new List<(int, string)>();

            if (action == "add")
            {
                if (users.FirstOrDefault(d => d.EmailAddress.ToUpper() == entity.EmailAddress.ToUpper()) != null)
                    errorList.Add((StatusCodes.Status400BadRequest, "Unable to add the user. Duplicate record."));
                
                if (users.FirstOrDefault(d => d.EmailAddress.ToUpper() == entity.EmailAddress.ToUpper() && !d.IsEnabled) != null)
                    errorList.Add((StatusCodes.Status200OK, "User Disabled. Will set to Enabled"));
            }

            if (action == "edit")
            {
                if (users.FirstOrDefault(d => d.Id != entity.Id
                                              && d.EmailAddress.ToUpper() == entity.EmailAddress.ToUpper()) != null)
                    errorList.Add((StatusCodes.Status400BadRequest, "Unable to edit the user. Duplicate record."));
            }

            if (action == "delete")
            {
                var user = users.Where(x => x.Id == entity.Id).FirstOrDefault();
                
                if (user == null)
                    errorList.Add((StatusCodes.Status400BadRequest, "Unable to delete user. User not found."));
            }

            return errorList;

        }
    }
}
