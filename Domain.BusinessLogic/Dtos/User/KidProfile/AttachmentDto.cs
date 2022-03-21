using FileManagement.DataAccess;
using Microsoft.AspNetCore.Http;
using System;

namespace Domain.BusinessLogic.Models
{
    public class AttachmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public AttachmentTypeEnum Type { get; set; }
        public string MimeType { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public IFormFile File { get; set; }
    }
}