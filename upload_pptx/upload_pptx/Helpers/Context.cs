using Microsoft.EntityFrameworkCore;

namespace upload_pptx.Helpers
{
    public class Context:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-EHVPQGR;Initial Catalog=upload_pptx;Integrated Security=True;Pooling=False");
        }
        public DbSet<slide>slides { get; set; }
    }
}
