using Microsoft.EntityFrameworkCore;
using WebApiCrud.Model;

namespace WebApiCrud.Data
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext>options):base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }//read write
    }
}
