using System;
using System.Collections.Generic;
using ContentBlockService.Data.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static ContentBlockService.Constants;

namespace ContentBlockService.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class CallToActionContentBlock: ILoggable
    {
        public int Id { get; set; }
        
		[ForeignKey("Tenant")]
        public int? TenantId { get; set; }
        
		[Index("CallToActionContentBlockNameIndex", IsUnique = false)]
        [Column(TypeName = "VARCHAR")]     
        [StringLength(MaxStringLength)]		   
		public string Name { get; set; }

        public string Slug { get; set; }

        public string Body { get; set; }

        public string Headline { get; set; }

        public string CallToAction { get; set; }

        public string FinalNote { get; set; }

        public string ButtonCaption { get; set; }

        public DateTime CreatedOn { get; set; }
        
		public DateTime LastModifiedOn { get; set; }
        
		public string CreatedBy { get; set; }
        
		public string LastModifiedBy { get; set; }
        
		public bool IsDeleted { get; set; }

        public virtual Tenant Tenant { get; set; }
    }
}
