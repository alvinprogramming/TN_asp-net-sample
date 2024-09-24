using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sample.Business.IServices.Admin;
using Sample.Data.DTO;
using Sample.Data.DTO.Admin;
using Sample.Data.Entities;

namespace Sample.Business.IServices.Admin
{
    public class UserService : IUserService
    {

        private readonly SampleDbContext _dbContext;

        public UserService(SampleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region get
        public async Task<List<UserDTO>> GetAllAsync()
        {
            return await _dbContext // allows usage of functions that relate to the database
                .UserInfos // direct from table - fields of the table ; 1:1 (in entities, automatically created after migration)
                //.Where(x => x.IsEnabled) // gets only enabled values | start of query
                .Select(x => new UserDTO(x)) // converts selected values to list
                .ToListAsync(); // returns to list async
            // this is linq
            // this is same with linq (sql syntax)

        }
        public async Task<UserDTO> GetByIdAsync(Guid id)
        {

            var data = await _dbContext.UserInfos.Where(x => x.Id == id).Select(x => new UserDTO(x)).FirstOrDefaultAsync();

            //_dbContext.UserInfos.Include(x => x.Id).ThenInclude() -> for calling derived data from base
            if (data != null)
            {
                return data;
            }
            else
            {
                return null;
            }

        }

        public async Task<UserInfo> GetById(Guid id)
        {
            // userinfo = direct from db | db itself
            // userdto = interface - business to db | passage to db

            var data = await _dbContext.UserInfos.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (data != null)
            {
                return data;
            }
            else
            {
                return null;
            }            
        }

        public async Task<List<UserDTO>> Search(string emailAddress)
        {
            var data = await _dbContext 
                .UserInfos 
                .Where(x => x.EmailAddress == emailAddress) 
                .Select(x => new UserDTO(x)) 
                .ToListAsync(); 

            if (data != null)
            {
                return data;
            }
            else
            {
                return null;
            }

        }
        #endregion

        #region action
        public async Task AddAsync(UserDTO entity)
        {
            var user = new UserInfo()
            {
                Id = Guid.NewGuid(),
                FirstName = entity.FirstName,
                MiddleName = entity.MiddleName,
                LastName = entity.LastName,
                Age = entity.Age,
                EmailAddress = entity.EmailAddress,
                Address = entity.Address,
                CreatedBy = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                IsEnabled = true,
                Username = entity.Username,
                Password = entity.Password

            };
            await _dbContext.UserInfos.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }


        public async Task UpdateAsync(UserDTO entity)
        {
            var user = await GetById(entity.Id);

            if (user != null)
            {
                user.FirstName = entity.FirstName;
                user.MiddleName = entity.MiddleName;
                user.LastName = entity.LastName;
                user.Age = entity.Age;
                user.EmailAddress = entity.EmailAddress;
                user.UpdatedBy = Guid.NewGuid();
                user.UpdatedDate = DateTime.Now;
                user.Address = entity.Address;
                user.IsEnabled = entity.IsEnabled;
                
                await _dbContext.SaveChangesAsync();
                
            }
        }
        public async Task DeleteAsync(Guid id)
        {
            var user = await GetById(id);

            if (user != null)
            {
                _dbContext.Remove(user);
                await _dbContext.SaveChangesAsync(); // required for every use of the db context

            }
        }
        #endregion

        #region private

        public async Task<LoginDTO> PreLogin(string username, string password)
        {
            var data = await _dbContext
                .UserInfos
                .Where(x => x.Username == username && x.Password == password)
                .Select(x => new LoginDTO(x))
                .FirstOrDefaultAsync(); // NOTE : 

            return data;
        }
        #endregion


    }
}
