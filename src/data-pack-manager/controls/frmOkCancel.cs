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
using System.Windows.Forms;
using medea.common;

namespace medea.controls
{
	public partial class frmOkCancel : Form
	{
		//action currentAction;
		protected event EventHandler Submit;
		protected bool isNew = true;
		protected bool _showMandatoryLegend = true;

		public bool ShowMandatoryLegend
		{
			get
			{
				return _showMandatoryLegend;
			}
			set
			{
				_showMandatoryLegend = value;
				lblCamposObligatorios.Visible = _showMandatoryLegend;
			}
		}

		public frmOkCancel()
		{
			InitializeComponent();
		}

		protected virtual void OnSubmit()
		{
			if (Submit != null)
				Submit.Invoke(this, EventArgs.Empty);
		}
		protected bool Call(action action, bool closeAfterAction = true)
		{
			if (Invoker.CallProgress(action))
			{
				if (closeAfterAction)
				{
					DialogResult = DialogResult.OK;
					Close();
				}
				return true;
			}
			else
				return false;
		}

		private void enableForm(bool p)
		{
			cmdOK.Enabled = p;
			cmdCancel.Enabled = p;
		}


		private void cmdOK_Click(object sender, EventArgs e)
		{
			try
			{
				OnSubmit();
			}
			catch (Exception ex)
			{
				UI.ShowError(this, ex);
			}
		}
		
	}
}
