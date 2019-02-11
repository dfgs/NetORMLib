﻿using NetORMLib.DbTypes;
using NetORMLib.Filters;
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

		string Table
		{
			get;
		}
			   
		bool IsPrimaryKey
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
	}

	public interface IColumn<T>:IColumn
	{
		IIsNullFilter<T> IsNull();
		IIsNotNullFilter<T> IsNotNull();
	}

	public interface IColumn<T,TVal> : IColumn<T>
		where TVal: IDbType
	{
		
		IIsEqualToFilter<T,TVal> IsEqualTo(TVal Value);
		IIsNotEqualToFilter<T, TVal> IsNotEqualTo(TVal Value);
		IIsGreaterThanFilter<T, TVal> IsGreaterThan(TVal Value);
		IIsLowerThanFilter<T, TVal> IsLowerThan(TVal Value);
		IIsGreaterOrEqualToFilter<T, TVal> IsGreaterOrEqualTo(TVal Value);
		IIsLowerOrEqualToFilter<T, TVal> IsLowerOrEqualTo(TVal Value);
	}

}
