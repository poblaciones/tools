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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using medea.entities;
using GeoAPI.Geometries;
using medea.common;
using NetTopologySuite.Geometries;
using medea.controls;
using medea.actions;

namespace medea.winApp
{
	public partial class frmsimplify : Form
	{
		public frmsimplify()
		{
			InitializeComponent();
		}

		private void frmsimplify_Load(object sender, EventArgs e)
		{
			//Trae todos los items de la geografía padre para asociar.

		}

		private void button1_Click(object sender, EventArgs e)
		{
			var call = new GeographySimplify();
			if (Invoker.CallProgress(call))
			{
				if (call.Errors.Count > 0)
					UI.ShowInfoMessage(this, "Listo con errores " + call.Errors.Count);
				else
					UI.ShowInfoMessage(this, "Listo.");
				int i = call.Errors.Count;
			}

		}

		private void button2_Click(object sender, EventArgs e)
		{
			var call = new ClippingRegionSimplify();
			if (Invoker.CallProgress(call))
			{
				if (call.Errors.Count > 0)
					UI.ShowInfoMessage(this, "Listo con errores " + call.Errors.Count);
				else
					UI.ShowInfoMessage(this, "Listo.");
				int i = call.Errors.Count;
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{

			var call1 = new ClippingRegionSimplify();
			Invoker.CallProgress(call1);

			var call2 = new GeographySimplify();
			if (Invoker.CallProgress(call2))
			{
				if (call1.Errors.Count > 0)
					UI.ShowInfoMessage(this, "Listo 1 con errores " + call2.Errors.Count);

				if (call2.Errors.Count > 0)
					UI.ShowInfoMessage(this, "Listo 2 con errores " + call2.Errors.Count);

				UI.ShowInfoMessage(this, "Listo.");
				int i = call1.Errors.Count + call2.Errors.Count;
			}

		}

		private void btnToDisk_Click(object sender, EventArgs e)
		{
			var call = new GeographySimplifyExecDb();
			if (Invoker.CallProgress(call))
			{
				if (call.Errors.Count > 0)
					UI.ShowInfoMessage(this, "Listo con errores " + call.Errors.Count);
				else
					UI.ShowInfoMessage(this, "Listo.");
				int i = call.Errors.Count;
			}
		}
	}
}
