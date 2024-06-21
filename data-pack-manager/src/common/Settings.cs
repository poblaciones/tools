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
namespace medea.common
{
	public class Settings
	{
		/// <summary>
		/// Cada cuantos inserts gerenados se agregan los campos a insertar.
		/// </summary>
		public static int InsertGeneratorFieldsEvery { get { return 100; } }

		/// <summary>
		/// Cantidad de líneas para ejecutar batch de sql.
		/// </summary>
		public static int BulkInsertBatchSize { get { return 100; } }

		/// <summary>
		/// Cantidad de ítems en los listados.
		/// </summary>
		public static int CantItems { get { return 1000; } }

		/// <summary>
		/// Porcentaje (%) mínimo de intersección de areas para joinear
		/// Región x Geografía.
		/// </summary>
		public static double MinIntersectionPercent { get { return 50; }  }
	}
}
