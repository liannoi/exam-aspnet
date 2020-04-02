using Microsoft.EntityFrameworkCore;

namespace Exam.Persistence.Context
{
    public class IntervalsDbContextFactory : DesignTimeDbContextFactoryBase<FilmsDbContext>
    {
        protected override FilmsDbContext CreateNewInstance(DbContextOptions<FilmsDbContext> options)
        {
            return new FilmsDbContext(options);
        }
    }
}