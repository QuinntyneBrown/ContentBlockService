using ContentBlockService.Data.Model;

namespace ContentBlockService.Features.Accounts
{
    public class RegisterAccountApiModel
    {        
        public int Id { get; set; }

        public int? TenantId { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
