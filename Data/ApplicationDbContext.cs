//namespace Bismillah.Data
//{
//    public class ApplicationDbContext
//    {

//    }
//}



using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
//using Bismillah.Modal;
//using 

namespace Bismillah.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

    }
}