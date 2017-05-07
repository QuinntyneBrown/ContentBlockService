using System;
using ContentBlockService.Data.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

using static ContentBlockService.Constants;
using System.ComponentModel.DataAnnotations;

namespace ContentBlockService.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class ContentBlock : ILoggable
    {
        public int Id { get; set; }

        [ForeignKey("Tenant")]
        public int? TenantId { get; set; }

        [Index("ContentBlockNameIndex", IsUnique = false)]
        [Column(TypeName = "VARCHAR")]
        [StringLength(MaxStringLength)]
        public string Name { get; set; }

        public string Slug { get; set; }

        public string Title { get; set; }

        public string IconUrl { get; set; }

        public string ImageUrl { get; set; }

        public string Url { get; set; }

        public string HTMLContent { get; set; }

        public string Heading1 { get; set; }

        public string Heading2 { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastModifiedOn { get; set; }

        public string CreatedBy { get; set; }

        public string LastModifiedBy { get; set; }

        public bool IsDeleted { get; set; }

        public virtual Tenant Tenant { get; set; }
    }
}
