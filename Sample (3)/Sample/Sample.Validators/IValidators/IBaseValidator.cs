using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample.Data.DTO;

namespace Sample.Validator.IValidators
{
    public interface IBaseValidator<T>
    {
        Task<List<(int, string)>> ValidateAsync(T entity, HttpMethod httpMethod);

        #region aug27
        ////Task<T> ValidateFName(string fname);
        ////Task<T> ValidateMName(string mname);
        ////Task<T> ValidateLName(string lname);
        ////Task<T> ValidateAge(int age);
        ////Task<T> ValidateEmail(string email);
        ////Task<T> ValidateAddress(string address);

        ////Task<T> AddFields(string fname, string mname, string lname, int age, string email, string address);

        //Task<T> AddFields(UserDTO userDTOObject);

        //Task<T> ValidateFields(UserDTO userDTOObject);

        //Task<T> EditFields(UserDTO userDTOObject);

        //Task<T> DeleteFields(UserDTO userDTOObject);

        ////Task<T> ValidateFields(string fname, string mname, string lname, int age, string email, string address);

        ////Task<T> ValidateExistingFields(int id, string fname, string mname, string lname, int age, string email, string address);



        ////Task<T> ValidateExistingFields(UserDTO userDTOObject);

        //Task<T> ValidateExistingFieldsEdit(UserDTO userDTOObject);

        //Task<T> ValidateExistingFieldsDelete(UserDTO userDTOObject);
        #endregion

    }
}
