using DB.Entities;
using DB.Interfaces;
using LB4.DTO;
using LB4.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LB4.Services
{
    public class DocumentsService : IDocumentsService
    {
        private readonly ITenantContext _unitOfTenant;

        public DocumentsService(
            ITenantContext unitOfTenant)
        {
            _unitOfTenant = unitOfTenant;
        }

        public async Task<List<Documents>> GetDocumentsAsync()
        {

            IQueryable<Documents> query = _unitOfTenant.Documents.AsNoTracking();
            List<Documents> result = await query
                .OrderBy(i => i.DocumentId)
                .ToListAsync();

            return result;
        }

        public async Task<List<Documents>> GetDocumentAsync(int? documentId, string url, string startDate, string endDate, string title, string photoUrl, string price, string transactionNumber )
        {
            IQueryable<Documents> query = _unitOfTenant.Documents.AsNoTracking();

            if (documentId != null && documentId > 0)
                query = query.Where(x => x.DocumentId == documentId);

            if (!string.IsNullOrEmpty(url))
                query = query.Where(x => x.Url.Contains(url));

            if (DateTime.TryParse(startDate, out DateTime date))
                query = query.Where(x => x.StartDate == date);

            if (DateTime.TryParse(endDate, out date))
                query = query.Where(x => x.EndDate == date);

            if (!string.IsNullOrEmpty(title))
                query = query.Where(x => x.Title.Contains(title));

            if (!string.IsNullOrEmpty(photoUrl))
                query = query.Where(x => x.PhotoUrl.Contains(photoUrl));

            if (decimal.TryParse(price, out decimal pr))
                query = query.Where(x => x.Price == pr);

            if (!string.IsNullOrEmpty(transactionNumber))
                query = query.Where(x => x.TransactionNumber.Contains(transactionNumber));

            List<Documents> result = await query
                .OrderBy(i => i.DocumentId)
                .ToListAsync();

            return result;
        }

        public async Task<Documents> AddDocument(DocumentDto document, string email)
        {
            var user = await _unitOfTenant.Users.FirstOrDefaultAsync(w => w.Email == email);

            var newDocument = new Documents
            {
                Url = document.Url,
                StartDate = document.StartDate,
                EndDate = document.EndDate,
                Title = document.Title,
                PhotoUrl = document.PhotoUrl,
                Price = document.Price,
                TransactionNumber = document.TransactionNumber,
                
        };

            var doc = await _unitOfTenant.Documents.AddAsync(newDocument);
            _unitOfTenant.Commit();

            var newDocumentExt = new DocumentExt
            {
                DocumentId = doc.Entity.DocumentId,
                DocumentStatusId = 1,//draft
                ActionId = 1,        //new
                UserId = user.UserId
            };

            await _unitOfTenant.DocumentExts.AddAsync(newDocumentExt);
            
            await _unitOfTenant.SaveChangesAsync();

            return doc.Entity;
        }

        public async Task<Documents> UpdateDocument(DocumentDto document, string email)
        {
            var user = await _unitOfTenant.Users.FirstOrDefaultAsync(w => w.Email == email);

            var findDocument = await _unitOfTenant.Documents.FirstOrDefaultAsync(w => w.DocumentId == document.Id);

            if (findDocument == null)
                return null;

            findDocument.Url  = document.Url ?? findDocument.Url;
            findDocument.StartDate = document.StartDate;
            findDocument.EndDate = document.EndDate;
            findDocument.Title = document.Title;
            findDocument.PhotoUrl = document.PhotoUrl;
            findDocument.Price = document.Price;
            findDocument.TransactionNumber = document.TransactionNumber;

            var updateDocumentExt = new DocumentExt
            {
                DocumentId = findDocument.DocumentId,
                DocumentStatusId = 1,//draft
                ActionId = 2,        //update
                UserId = user.UserId
            };

            await _unitOfTenant.DocumentExts.AddAsync(updateDocumentExt);

            await _unitOfTenant.SaveChangesAsync();

            return findDocument;
        }

        public async Task<Documents> DeleteDocument(DocumentDto document, string email)
        {
            var user = await _unitOfTenant.Users.FirstOrDefaultAsync(w => w.Email == email);

            var findDocument = await _unitOfTenant.Documents.FirstOrDefaultAsync(w => w.DocumentId == document.Id);

            if (findDocument == null)
                return null;


            var deleteDocumentsExt = await _unitOfTenant.DocumentExts.Where(w => w.DocumentId == findDocument.DocumentId).ToListAsync();

            _unitOfTenant.DocumentExts.RemoveRange(deleteDocumentsExt);

            _unitOfTenant.Documents.Remove(findDocument);
            _unitOfTenant.SaveChangesAsync();

            return findDocument;
        }
    }
}
