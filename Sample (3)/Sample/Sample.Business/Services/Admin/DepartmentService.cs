using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sample.Business.IServices.Admin;
using Sample.Data.DTO.Admin;
using Sample.Data.Entities;

namespace Sample.Business.IServices.Admin
{
    public class DepartmentService : IDepartmentService
    {

        private readonly SampleDbContext _dbContext;


        public DepartmentService(SampleDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        #region get
        public async Task<List<DepartmentDTO>> GetAllAsync()
        {
            return await _dbContext 
                .DepartmentInfos
                .Select(x => new DepartmentDTO(x)) 
                .ToListAsync(); 

            //throw new NotImplementedException();


        }

        //public Task<UserInfo> GetById(Guid id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<DepartmentDTO> GetByIdAsync(Guid id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<UserDTO>> Search(string emailAddress)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<DepartmentDTO> GetByIdAsync(Guid id)
        {

            var data = await _dbContext.DepartmentInfos.Where(x => x.DepartmentID == id).Select(x => new DepartmentDTO(x)).FirstOrDefaultAsync();
            // 
            if (data != null)
            {
                return data;
            }
            else
            {
                return null;
            }

        }

        public async Task<DepartmentInfo> GetById(Guid id)
        {
            // userinfo = direct from db | db itself
            // userdto = interface - business to db | passage to db

            var data = await _dbContext.DepartmentInfos.Where(x => x.DepartmentID == id).FirstOrDefaultAsync();

            if (data != null)
            {
                return data;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<DepartmentDTO>> Search(string emailAddress)
        {
            var data = await _dbContext
                .DepartmentInfos
                .Where(x => x.DepartmentName == emailAddress)
                .Select(x => new DepartmentDTO(x))
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
        //#endregion

        //#region action
        public async Task AddAsync(DepartmentDTO entity)
        {
            var user = new DepartmentInfo()
            {
                DepartmentID = Guid.NewGuid(),
                DepartmentName = entity.DepartmentName,
                CreatedBy = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                IsEnabled = true

            };
            await _dbContext.DepartmentInfos.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }






        public async Task UpdateAsync(DepartmentDTO entity)
        {
            var user = await GetById(entity.DepartmentID);

            if (user != null)
            {
                user.DepartmentName = entity.DepartmentName;
                user.CreatedDate = entity.CreatedDate;
                user.CreatedBy = entity.CreatedBy;
                user.UpdatedBy = Guid.NewGuid();
                user.UpdatedDate = DateTime.Now;
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
        //#endregion
        //#region private
        #endregion


    }
}
