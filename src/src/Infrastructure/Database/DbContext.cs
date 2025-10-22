namespace TServiceNameT.Infrastructure.Database
{
	using Microsoft.EntityFrameworkCore;

	public class DataContext(DbContextOptions<DataContext> options) : DbContext(options);
}