using Microsoft.EntityFrameworkCore;
using Tracker.Api.Entities;

namespace Tracker.Api.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<User> Users { get; set; } = null!;
}
