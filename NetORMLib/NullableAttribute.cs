using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	/// <summary>
	///  Used to be able to use reflection and dectect if column is nullable or not
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Event | AttributeTargets.Field |
					 AttributeTargets.GenericParameter | AttributeTargets.Module | AttributeTargets.Parameter |
					 AttributeTargets.Property | AttributeTargets.ReturnValue,
					 AllowMultiple = false)]
	public class NullableAttribute : Attribute
	{
		public byte Mode { get; }

		public NullableAttribute(byte mode)
		{
			this.Mode = mode;
		}

		public NullableAttribute(byte[] _) => throw new System.NotImplementedException();
	}
}
