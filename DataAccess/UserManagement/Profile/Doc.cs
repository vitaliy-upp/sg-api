﻿using Common.DataAccess;

namespace DataAccess.UserManagement
{
    public class Doc : Attachment, IBaseDomainModel<int>
    {
        public string Type { get; set; }
        public string Url { get; set; }
    }

}
