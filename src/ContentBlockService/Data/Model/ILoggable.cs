﻿using System;

namespace ContentBlockService.Data.Model
{
    public interface ILoggable
    {
        DateTime CreatedOn { get; set; }
        DateTime LastModifiedOn { get; set; }
        string CreatedBy { get; set; }
        string LastModifiedBy { get; set; }
    }
}
