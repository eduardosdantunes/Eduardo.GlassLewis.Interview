using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Interview.Infrastructure;

internal class EfContextFactory : IDesignTimeDbContextFactory<InterviewContext>
{
    public InterviewContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<InterviewContext>();
        optionsBuilder.UseSqlServer("Data Source=localhost;Database=InterviewDb;Integrated Security=True");


        return new InterviewContext(optionsBuilder.Options);
    }
}