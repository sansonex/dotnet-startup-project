namespace TServiceNameT.Domain.Interfaces
{
	public interface IUnitOfWork
	{
		public System.Threading.Tasks.Task BeginTransactionAsync();

		public System.Threading.Tasks.Task RollbackAsync();

		public System.Threading.Tasks.Task CommitAsync();
	}
}