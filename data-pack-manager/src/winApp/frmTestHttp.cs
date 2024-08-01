/*
*    Poblaciones - Plataforma abierta de datos espaciales de población.
*    Copyright (C) 2018-2024. Consejo Nacional de Investigaciones Científicas y Técnicas (CONICET)
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
using System.Windows.Forms;
using NetTopologySuite.IO;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using System.IO;
using NetTopologySuite.Geometries.Utilities;
using medea.common;
using System.Collections;
using medea.controls;

namespace medea.winApp
{
	public partial class frmTestHttp : Form
	{

		public frmTestHttp()
		{
			InitializeComponent();

		}


		private void button4_Click(object sender, EventArgs e)
		{
			string start = "services/backoffice/StartPublish?w=6";
			string step = "services/backoffice/StepPublish";
			HttpInvoker.CallProgress(start, step);
			MessageBox.Show(this, "Listo!");
		}
	}
}
