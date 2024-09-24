using Microsoft.EntityFrameworkCore;
using Sample.Business.IServices.Admin;
using Sample.Data.DTO.Admin;
using Sample.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Business.Services.Admin
{
    public class UserService : IUserService
    {
        private readonly SampleDbContext _dbContext;

        public UserService(SampleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Get
        public async Task<List<UserDTO>> GetAllAsync()
        {
           return await _dbContext.UserInfos.Where(x => x.IsEnabled).Select(x => new UserDTO(x)).ToListAsync();
        }

        public async Task<UserDTO> GetbyIdAsync(Guid id)
        {
            var data = await _dbContext.UserInfos.Where(x => x.Id == id).Select(x => new UserDTO(x)).FirstOrDefaultAsync();

            if (data != null)
                return data;
            else
                return null;
        }

        public async Task<UserInfo> GetbyId(Guid id)
        {
            var data = await _dbContext.UserInfos.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (data != null)
                return data;
            else
                return null;
        }

        public async Task<List<UserDTO>> Search(string emailAddress)
        {
            var data = await _dbContext.UserInfos.Where(x => x.EmailAddress == emailAddress).Select(x => new UserDTO(x)).ToListAsync();

            if (data != null)
                return data;
            else
                return null;
        }
        #endregion

        #region Action 
        public async Task AddAsync(UserDTO entity)
        {
            var user = new UserInfo
            {
                Id = Guid.NewGuid(),
                FirstName = entity.FirstName,
                MiddleName = entity.MiddleName,
                LastName = entity.LastName,
                Age = entity.Age,
                Address = entity.Address,
                EmailAddress = entity.EmailAddress,
                CreatedBy = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                IsEnabled = true
            };

            await _dbContext.UserInfos.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserDTO entity)
        {
            var user = await GetbyId(entity.Id);

            if (user != null)
            {
                user.FirstName = entity.FirstName;
                user.MiddleName = entity.MiddleName;
                user.LastName = entity.LastName;
                user.Age = entity.Age;
                user.Address = entity.Address;
                user.EmailAddress = entity.EmailAddress;
                user.UpdatedBy = Guid.NewGuid();
                user.UpdatedDate = DateTime.Now;

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await GetbyId(id);
            var userToDelete = await _dbContext.UserInfos.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (user != null)
            {
                _dbContext.Remove(user);
                await _dbContext.SaveChangesAsync();
            }

        }
        #endregion

        #region Private

        #endregion
    }
}
