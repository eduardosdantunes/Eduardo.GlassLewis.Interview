using Interview.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Interview.Infrastructure;

internal class InterviewContext : DbContext
{
    public InterviewContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    public DbSet<Company> Companies { get; set; } = null!;
}