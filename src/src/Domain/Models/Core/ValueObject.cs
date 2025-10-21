namespace Globo.TServiceNameT.Domain.Models.Core
{
	using System.Collections.Generic;
	using System.Linq;

	public abstract class ValueObject
	{
		public override bool Equals(object? obj)
		{
			if (obj == null)
			{
				return false;
			}

			if (this.GetType() != obj.GetType())
			{
				return false;
			}

			var valueObject = (ValueObject)obj;

			return this.GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
		}

		public override int GetHashCode()
		{
			return this.GetEqualityComponents()
				.Aggregate(
					1,
					(current, obj) =>
					{
						unchecked
						{
							return (current * 23) + (obj?.GetHashCode() ?? 0);
						}
					});
		}

		protected abstract IEnumerable<object> GetEqualityComponents();
	}
}