
using NetORMLib.Filters;
using NetORMLib.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetORMLib.Columns
{
	public interface IColumn
	{
		string Name
		{
			get;
		}

		ISingleTable Table
		{
			get;
		}
			   
		ColumnConstraints Constraint
		{
			get;
			set;
		}

		bool IsNullable
		{
			get;
		}

		Type DataType
		{
			get;
		}

		bool IsIdentity
		{
			get;
			set;
		}

		void Initialize(ISingleTable Table,string Name);
		/*object GetValue(IRow Row);
		void SetValue(IRow Row,object Value);*/
	}

	public interface IColumn<TVal>:IColumn
	{
		TVal DefaultValue
		{
			get;
			set;
		}

		IIsNullFilter IsNull();
		IIsNotNullFilter IsNotNull();
		
		IIsEqualToFilter<TVal> IsEqualTo(TVal Value);
		IIsNotEqualToFilter< TVal> IsNotEqualTo(TVal Value);
		IIsGreaterThanFilter<TVal> IsGreaterThan(TVal Value);
		IIsLowerThanFilter<TVal> IsLowerThan(TVal Value);
		IIsGreaterOrEqualToFilter<TVal> IsGreaterOrEqualTo(TVal Value);
		IIsLowerOrEqualToFilter<TVal> IsLowerOrEqualTo(TVal Value);

	}

}
