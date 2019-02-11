using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.DbTypes
{
	public abstract class DbType<T>:IEquatable<T>,IDbType
	{
		private readonly T value;

		private DbType()
		{

		}

		public DbType(T Value)
		{
			if (Value == null) throw new ArgumentNullException("Value");
			this.value = Value;
		}

		public bool Equals(T other)
		{
			return value.Equals(other);
		}

		public override string ToString()
		{
			return value.ToString();
		}

		public static implicit operator T(DbType<T> Value)
		{
			return Value.value;
		}
		

		public object GetCLRValue()
		{
			return value;
		}

	}
}
