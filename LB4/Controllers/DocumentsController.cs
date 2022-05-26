using DB.Entities;
using LB4.DTO;
using LB4.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LB4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentsService _documentsService;

        /// <summary>
        ///     Represents constructor for the controller
        /// </summary>
        /// <param name="documentsService">Dictionary service</param>
        public DocumentsController(IDocumentsService documentsService)
        {
            _documentsService = documentsService;
        }

        /// <summary>
        ///     Gets Documents
        /// </summary>
        /// <response code="200">Successful response</response>
        [HttpGet]
        [Route("/Documents/Documents")]
        [SwaggerResponse(200, type: typeof(IEnumerable<Documents>), description: "List of items")]

        public async Task<IEnumerable<Documents>> GetDocumentsAsync()
        {
            var result = await _documentsService.GetDocumentsAsync();

            return result;
        }

        /// <summary>
        ///     Get Document by ID
        /// </summary> 
        /// <param name="documentId"></param>
        /// <param name="url"></param>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="price"></param>
        /// <param name="title"></param>
        /// <param name="photourl"></param>
        /// <param name="transactionnumber"></param>
        /// <response code="200">Successful response</response>
        [HttpGet]
        [Route("/Documents/Document")]
        [SwaggerResponse(200, type: typeof(Documents), description: "Item by filter")]

        public async Task<List<Documents>> GetDocumentAsync([FromQuery] int? documentId = null, [FromQuery] string url = null, [FromQuery] string startDate = null, [FromQuery] string endDate = null, [FromQuery] string title = null, [FromQuery] string photoUrl = null, [FromQuery] string price = null, [FromQuery] string transactionNumber = null)
        {
            var result = await _documentsService.GetDocumentAsync(documentId, url, startDate,  endDate,  title, photoUrl,  price,  transactionNumber);

            return result;
        }

        [HttpPut]
        [Route("/Documents/Document")]
        [SwaggerResponse(200, type: typeof(Documents), description: "Item by filter")]
        public async Task<ActionResult<Documents>> UpdateDocumentAsync(DocumentDto documentDto)
        {                     
            var result = await _documentsService.UpdateDocument(documentDto, "admin@gmail.com");

            if (result == null)
                BadRequest("Document not found");

            return result;
        }

        [HttpPost]
        [Route("/Documents/Document")]
        [SwaggerResponse(200, type: typeof(Documents), description: "Item by filter")]
        public async Task<ActionResult<Documents>> AddDocumentAsync(DocumentDto documentDto)
        {
            var u = User.Identity.Name;
            var result = await _documentsService.AddDocument(documentDto, "admin@gmail.com");
            return result;
        }

        [HttpDelete]
        [Route("/Documents/Document")]
        [SwaggerResponse(200, type: typeof(Documents), description: "Item by filter")]
        public async Task<ActionResult<Documents>> DeleteDocumentAsync(DocumentDto documentDto)
        {
            var result = await _documentsService.DeleteDocument(documentDto, "admin@gmail.com");

            if (result == null)
                BadRequest("Document not found");

            return result;
        }
    }
}
