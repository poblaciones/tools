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
using System.Linq;
using medea.common;
using medea.entities;
using medea.Data;
using System.Collections.Generic;
using System.Globalization;
using System;

namespace medea.actions
{
	public class VersionUpdater
	{
		string key;
		public VersionUpdater(string key)
		{
			this.key = key;
		}
		public bool NeedUpdate(RegenEnum option)
		{
			if (option == RegenEnum.No) 
				return false;
			else if (option == RegenEnum.Yes) 
				return true;
			else
				return (0 == medea.context.Data.Session.SqlActions.ExecuteScalarInt("select count(*) from version v1, "
						+" version v2 where v1.ver_value = v2.ver_value AND v2.ver_name='CARTO_GEO' and v1. ver_name = '" + key + "'"));
		}
		public void SetUpdated()
		{
			string val = medea.context.Data.Session.SqlActions.ExecuteScalar("select v2.ver_value from version v2 where v2.ver_name='CARTO_GEO'") as string;

			medea.context.Data.Session.SqlActions.ExecuteNonQuery("update version set ver_value = "
					+" '" + val + "' where ver_name = '" + key + "'");
		}

		public static void Increment()
		{
			medea.context.Data.Session.SqlActions.ExecuteNonQuery("update version set ver_value = ver_value + 1 where ver_name = 'CARTO_GEO'");
		}
		public void IncrementCustomVersion()
		{
			medea.context.Data.Session.SqlActions.ExecuteNonQuery("update version set ver_value = ver_value + 1 where ver_name = '" + key + "'");
		}
	}
}
