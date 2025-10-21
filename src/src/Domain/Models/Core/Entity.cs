namespace Globo.TServiceNameT.Domain.Models.Core
{
	public abstract class Entity<TId>
		where TId : struct
	{
		public TId Id { get; set; }
	}
}