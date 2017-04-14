using System.Data.Entity.Migrations;
using ContentBlockService.Data;
using ContentBlockService.Data.Model;
using ContentBlockService.Features.Users;

namespace ContentBlockService.Migrations
{
    public class RoleConfiguration
    {
        public static void Seed(ContentBlockServiceContext context) {

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.SYSTEM
            });

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.ACCOUNT_HOLDER
            });

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.DEVELOPMENT
            });

            context.SaveChanges();
        }
    }
}
