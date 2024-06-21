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
namespace medea.Data
{
	/// <summary>
	/// Descripci�n del tipo motor de base de datos.
	/// </summary>
	public enum DatabaseTypeEnum
	{
		/// <summary>
		/// No definido.
		/// </summary>
		Undefined = 0,
		/// <summary>
		/// Microsoft SQL Server.
		/// </summary>
		SqlServer = 1,
		/// <summary>
		/// Oracle.
		/// </summary>
		Oracle = 2,
		/// <summary>
		/// SQLite.
		/// </summary>
		SQLite = 3,
		/// <summary>
		/// MySql.
		/// </summary>
		MySql = 4
	}
}