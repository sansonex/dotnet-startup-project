namespace Globo.TServiceNameT.Infrastructure.Database
{
	using System.Threading.Tasks;

	using Globo.TServiceNameT.Domain.Interfaces;

	public class UnitOfWork(DataContext context) : IUnitOfWork
	{
		public Task BeginTransactionAsync()
		{
			return context.Database.BeginTransactionAsync();
		}

		public Task RollbackAsync()
		{
			return context.Database.RollbackTransactionAsync();
		}

		public Task CommitAsync()
		{
			return context.Database.CommitTransactionAsync();
		}
	}
}