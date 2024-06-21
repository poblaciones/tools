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
using System.Reflection;
using System.ComponentModel;

namespace medea.entities
{
	public enum SpecialColumnEnum
	{
		[Description("Ninguno")]
		Null = 0,
		[Description("Personas")]
		People = -1,
		[Description("Hogares")]
		Household = -2,
		[Description("Adultos (>=18)")]
		Adult = -3,
		[Description("Niños (<18)")]
		Children = -4,
		[Description("Area m2")]
		AreaM2 = -5,
		[Description("Conteo")]
		Count = -10,
		[Description("Otro")]
		Other = -100,
		[Description("------------------------")]
		Separator = -999
	}
}
