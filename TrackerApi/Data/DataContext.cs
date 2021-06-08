using Microsoft.EntityFrameworkCore;
using TrackerApi.Entities;

namespace TrackerApi.Data {

    public class DataContext : DbContext {

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

    }

}