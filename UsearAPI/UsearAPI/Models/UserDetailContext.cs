using Microsoft.EntityFrameworkCore;

namespace UsearAPI.Models
{
    public class UserDetailContext:DbContext
    {

        // Constructor with parameter 'options'
        public UserDetailContext(DbContextOptions<UserDetailContext> options):base(options) 
        { 

        }        

        
        // Property of UserDetails class that we created
        public DbSet<UserDetails> UserDetails { get; set; }

    }
}
