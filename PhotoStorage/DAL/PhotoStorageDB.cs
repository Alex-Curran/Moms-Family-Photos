using System.Data.Entity;
using PhotoStorage.Models;

namespace PhotoStorage.DAL
{
    public class PhotoStorageDb : DbContext
    {
        public PhotoStorageDb()
            :base("PhotoStorageDB")
        {
            
        }

        public DbSet<Photo> Photos { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
 
    }
}