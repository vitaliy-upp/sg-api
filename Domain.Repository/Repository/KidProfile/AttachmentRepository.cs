using Domain.DataAccess;
using Domain.DataAccess.Entities.KidProfile;
using Domain.DataAccess.Entities.KidProfile.Education;
using Domain.Repository.Interfaces.KidProfile;
using FileManagement.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Repository.KidProfile
{
    public class AttachmentRepository : DomainRepository<Attachment, int>, IAttachmentRepository
    {
        public AttachmentRepository(DomainDbContext dbContext) : base(dbContext)
        {
        }
    }
}
