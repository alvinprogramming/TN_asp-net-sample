using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Sample.Business.IServices.Admin;
using Sample.Data.DTO;
using Sample.Data.DTO.Admin;
using Sample.Data.Entities;
using Sample.Validator.IValidators.Admin;

namespace Sample.Validator.Validators.Admin
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
                        errorList = ValidateRole(users, entity, "add");
                    }
                    break;
                // Edit / Put
                case HttpMethod m when m == HttpMethod.Put:
                    {
                        var users = await _userService.GetAllAsync();
                        errorList = ValidateRole(users, entity, "edit");
                    }
                    break;
                // Remove / Restore
                case HttpMethod m when m == HttpMethod.Delete:
                    {
                        var users = await _userService.GetAllAsync();
                        errorList = ValidateRole(users, entity, "delete");
                    }
                    break;
            }

            return errorList;
        }

        private List<(int, string)> ValidateRole(List<UserDTO> users, UserDTO entity, string action)
        {
            var errorList = new List<(int, string)>(); // request goes here

            if (action == "add")
            {
                if (users.FirstOrDefault(d => d.Username.ToUpper() == entity.Username.ToUpper()) != null)
                    errorList.Add((StatusCodes.Status400BadRequest, "Unable to add the role. Duplicate record."));

                // firstordefault(where statement as lambda) -> checks if email address already exists from d (all users) to entity (currently handled record) || // if returns true IF inserting data email exists 
                // changed from email address to username

                if (users.FirstOrDefault(d => d.EmailAddress.ToUpper() == entity.EmailAddress.ToUpper() && !d.IsEnabled) != null)
                    errorList.Add((StatusCodes.Status200OK, "User Disabled. Will set to Enabled")); // checks if same AND if the data is enabled, || admin adds new email -> if email is found, admin will use function that enables old record 
            }

            if (action == "edit")
            {
                if (users.FirstOrDefault(d => d.Id != entity.Id
                                              && d.EmailAddress.ToUpper() == entity.EmailAddress.ToUpper()) != null)
                    errorList.Add((StatusCodes.Status400BadRequest, "Unable to edit the role. Duplicate record.")); // checks id and email address | if email is equal, checks id | if id is equal -> return "duplicate record" // NOTE : get back on this

            }

            if (action == "delete")
            {
                var userRole = users.Where(x => x.Id == entity.Id).FirstOrDefault();

                if (userRole == null)
                    errorList.Add((StatusCodes.Status400BadRequest, "Unable to delete user. User not found"));
            }

            return errorList;
        }



        // if errors, usually can do solution > clean solution > rebuild solution
    }
}
