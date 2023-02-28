using IdentityServer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Data
{
    public class AuthApplicationContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public AuthApplicationContext(DbContextOptions<AuthApplicationContext> options)
            : base(options)
        {

        }
    }
}
