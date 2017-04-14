using System;
using ContentBlockService.Data.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContentBlockService.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class ContentBlock: ILoggable
    {
        public int Id { get; set; }
        
		[ForeignKey("Tenant")]
        public int? TenantId { get; set; }
        
		[Index("NameIndex", IsUnique = false)]
        [Column(TypeName = "VARCHAR")]        
		public string Name { get; set; }

        public ContentBlockType ContentBlockType { get; set; }

        public string Title { get; set; }

        public string IconUrl { get; set; }

        public string ImageUrl { get; set; }

        public string Url { get; set; }
        
        public string HTMLContent { get; set; }
        
        public DateTime CreatedOn { get; set; }
        
		public DateTime LastModifiedOn { get; set; }
        
		public string CreatedBy { get; set; }
        
		public string LastModifiedBy { get; set; }
        
		public bool IsDeleted { get; set; }

        public virtual Tenant Tenant { get; set; }
    }
}
