using DB.Entities;
using DB.Interfaces;
using LB4.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LB4.Services
{
    public class DictionaryService : IDictionaryService
    {
        private readonly ITenantContext _unitOfTenant;

        public DictionaryService(
            ITenantContext unitOfTenant)
        {
            _unitOfTenant = unitOfTenant;
        }

        public async Task<User> GetUserAsync(string email)
        {
            IQueryable<User> query = _unitOfTenant.Users.AsNoTracking();

            if (!string.IsNullOrEmpty(email))
                query = query.Where(x => x.Email == email);

            User result = await query
                .OrderBy(i => i.Email)                
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<User>> GetUsersAsync()
        {

            IQueryable<User> query = _unitOfTenant.Users.AsNoTracking();

            List<User> result = await query
                .OrderBy(i => i.Email)
                .ToListAsync();

            return result;
        }

        public async Task<List<ActionType>> GetActionTypesAsync()
        {

            IQueryable<ActionType> query = _unitOfTenant.ActionTypes.AsNoTracking();

            List<ActionType> result = await query
                .OrderBy(i => i.ActionTypeId)                
                .ToListAsync();

            return result;
        }

        public async Task<List<Role>> GetRolesAsync()
        {

            IQueryable<Role> query = _unitOfTenant.Roles.AsNoTracking();

            List<Role> result = await query
                .OrderBy(i => i.RoleId)
                .ToListAsync();

            return result;
        }

        public async Task<List<DocumentStatus>> GetDocumentStatusAsync()
        {

            IQueryable<DocumentStatus> query = _unitOfTenant.DocumentStatus.AsNoTracking();

            List<DocumentStatus> result = await query
                .OrderBy(i => i.DocumentStatusId)
                .ToListAsync();

            return result;
        }
    }
}
