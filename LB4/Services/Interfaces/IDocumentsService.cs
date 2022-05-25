using DB.Entities;
using LB4.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LB4.Services.Interfaces
{
    public interface IDocumentsService
    {
        Task<List<Documents>> GetDocumentsAsync();
        Task<List<Documents>> GetDocumentAsync(int documentId, string url, string startDate, string endDate, string title, string photoUrl, string price, string transactionNumber);
        Task<Documents> AddDocument(DocumentDto document, string email);
        Task<Documents> UpdateDocument(DocumentDto document, string email);
        Task<Documents> DeleteDocument(DocumentDto document, string email);
    }
}
