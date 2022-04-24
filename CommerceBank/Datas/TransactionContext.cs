using CommerceBank.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CommerceBank.Datas
{
    public class TransactionContext : IdentityDbContext<ApplicationUser>
    {
        public TransactionContext()
        {
        }

        public TransactionContext(DbContextOptions options):base(options)
        {

        }

        public virtual DbSet<Transaction> Transac { get; set; }


        public virtual DbSet<Notifica> Notification { get; set; }
    }
}
