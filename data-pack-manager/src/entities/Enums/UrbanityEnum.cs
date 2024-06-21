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
using System.ComponentModel;
namespace medea.entities
{
	public enum UrbanityEnum
	{
		Rural = 0, // Rural agrupado (URP=2)
		Rural_Disperse = 1, // Rural agrupado (URP=3)
		Urban = 2, // Urbana seg�n CENSO (URP=1), 250 habitantes o m�s
		Urban_Disperse = 3, // Urbana seg�n CENSO (URP=1), menos de 250 habitantes
		None = 4
	}
}
