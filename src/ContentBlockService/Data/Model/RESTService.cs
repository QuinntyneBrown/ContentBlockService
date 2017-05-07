using System;
using System.Collections.Generic;
using ContentBlockService.Data.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

using static ContentBlockService.Constants;
using System.ComponentModel.DataAnnotations;

namespace ContentBlockService.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class RESTService: ILoggable
    {
        public int Id { get; set; }
        
		[ForeignKey("Tenant")]
        public int? TenantId { get; set; }
        
		[Index("NameIndex", IsUnique = false)]
        [Column(TypeName = "VARCHAR")]
        [StringLength(MaxStringLength)]
        public string Name { get; set; }
        
		public DateTime CreatedOn { get; set; }
        
		public DateTime LastModifiedOn { get; set; }
        
		public string CreatedBy { get; set; }
        
		public string LastModifiedBy { get; set; }
        
		public bool IsDeleted { get; set; }

        public virtual Tenant Tenant { get; set; }
    }
}
