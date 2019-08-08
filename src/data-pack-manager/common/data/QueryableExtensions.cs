/*
*    Poblaciones - Plataforma abierta de datos espaciales de población.
*    Copyright (C) 2018-2019. Consejo Nacional de Investigaciones Científicas y Técnicas (CONICET)
*		 y Universidad Católica Argentina (UCA).
*
*    This program is free software: you can redistribute it and/or modify
*    it under the terms of the GNU General Public License as published by
*    the Free Software Foundation, either version 3 of the License, or
*    (at your option) any later version.
*
*    This program is distributed in the hope that it will be useful,
*    but WITHOUT ANY WARRANTY; without even the implied warranty of
*    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*    GNU General Public License for more details.
*
*    You should have received a copy of the GNU General Public License
*    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
﻿using System;
using System.Linq;
using System.Reflection;
using NHibernate;
using NHibernate.AdoNet;
using NHibernate.Engine;
using NHibernate.Impl;
using NHibernate.Linq;

namespace medea.Data
{
	/// <summary>
	/// Ref: https://weblogs.asp.net/ricardoperes/strongly-typed-delete-with-nhibernate
	/// https://github.com/rjperes/DevelopmentWithADot.NHibernateExtensions/blob/master/DevelopmentWithADot.NHibernateExtensions/QueryableExtensions.cs
	/// </summary>
	public static class QueryableExtensions
	{
		#region Private static readonly fields
		private static readonly PropertyInfo sessionProperty = typeof(DefaultQueryProvider).GetProperty("Session", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetProperty);
		private static readonly FieldInfo batcherInterceptorField = typeof(AbstractBatcher).GetField("_interceptor", BindingFlags.NonPublic | BindingFlags.Instance);
		private static readonly FieldInfo sessionImplInterceptorField = typeof(SessionImpl).GetField("interceptor", BindingFlags.NonPublic | BindingFlags.Instance);
		#endregion

		#region Public extension methods
		public static void DeleteQ<T>(this IQueryable<T> queryable)
		{
			if (queryable.GetType().GetGenericTypeDefinition() == typeof(NhQueryable<>))
			{
				ISessionImplementor impl = sessionProperty.GetValue(queryable.Provider, null) as ISessionImplementor;
				IInterceptor oldInterceptor = sessionImplInterceptorField.GetValue(impl) as IInterceptor;
				IInterceptor deleteInterceptor = new DeleteInterceptor();

				try
				{
					batcherInterceptorField.SetValue(impl.Batcher, deleteInterceptor);
					queryable.Any();
				}
				finally
				{
					batcherInterceptorField.SetValue(impl.Batcher, oldInterceptor);
				}

			}
			else
				throw new ArgumentException("Invalid type", "queryable");
		}
		#endregion
	}
}
