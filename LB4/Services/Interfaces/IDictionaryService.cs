using DB.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LB4.Services.Interfaces
{
    public interface IDictionaryService
    {
        Task<User> GetUserAsync(string email);
        Task<List<User>> GetUsersAsync();
        Task<List<ActionType>> GetActionTypesAsync();
        Task<List<Role>> GetRolesAsync();
        Task<List<DocumentStatus>> GetDocumentStatusAsync();
    }
}
