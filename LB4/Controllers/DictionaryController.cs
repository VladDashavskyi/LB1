using DB.Entities;
using LB4.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LB4.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DictionaryController : ControllerBase
    {
        private readonly IDictionaryService _dictionaryService;

        /// <summary>
        ///     Represents constructor for the controller
        /// </summary>
        /// <param name="dictionaryService">Dictionary service</param>
        /// <param name="currentUserProvider">Current authorized user</param>
        public DictionaryController(IDictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        /// <summary>
        ///     Gets Action Type
        /// </summary>
        /// <response code="200">Successful response</response>
        [HttpGet]
        [Route("/Dictionaries/ActionType")]
        [SwaggerResponse(200, type: typeof(IEnumerable<ActionType>), description: "List of items")]

        public async Task<IEnumerable<ActionType>> GetActionTypesAsync()
        {
            var result = await _dictionaryService.GetActionTypesAsync();

            return result;
        }

        /// <summary>
        ///     Gets Roles
        /// </summary>        
        /// <response code="200">Successful response</response>        
        [HttpGet]
        [Route("/Dictionaries/Role")]
        [SwaggerResponse(200, type: typeof(IEnumerable<Role>), description: "List of items")]

        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            var result = await _dictionaryService.GetRolesAsync();

            return result;
        }

        /// <summary>
        ///     Get Users
        /// </summary> 
        /// <response code="200">Successful response</response>
        [HttpGet]
        [Route("/Dictionaries/Users")]
        [SwaggerResponse(200, type: typeof(User), description: "List of items")]

        public async Task<List<User>> GetUsersAsync()
        {
            var result = await _dictionaryService.GetUsersAsync();

            return result;
        }

        /// <summary>
        ///     Get User by email
        /// </summary> 
        /// <param name="email"></param>
        /// <response code="200">Successful response</response>
        [HttpGet]
        [Route("/Dictionaries/User")]
        [SwaggerResponse(200, type: typeof(User), description: "Users by email")]

        public async Task<User> GetUserAsync([FromQuery] string email)
        {
            var result = await _dictionaryService.GetUserAsync(email);

            return result;
        }

        /// <summary>
        ///     Gets DocumentStatus
        /// </summary>        
        /// <response code="200">Successful response</response>
        [HttpGet]
        [Route("/Dictionaries/DocumentStatus")]
        [SwaggerResponse(200, type: typeof(IEnumerable<DocumentStatus>), description: "List of items")]

        public async Task<IEnumerable<DocumentStatus>> GetDocumentStatusAsync()
        {
            var result = await _dictionaryService.GetDocumentStatusAsync();

            return result;
        }
    }
}
