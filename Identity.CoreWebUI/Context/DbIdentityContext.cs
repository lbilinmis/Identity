using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.CoreWebUI.Context
{
    public class DbIdentityContext:IdentityDbContext<AppUser,AppRole,int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data source=IGU-NB-0884;initial catalog=IdentityDB;user id=sa;password=s123456*-;");
        }

    }
}
