using bookystufflocal.domain.DomainLayer.Library;
using Microsoft.EntityFrameworkCore;

namespace bookystufflocal.domain.RepositoryLayer.EntityFrameworkCore
{
    public class BookyStuffLocalDbContaxt : DbContext
    {
        public BookyStuffLocalDbContaxt(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
    }
}
