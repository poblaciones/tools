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
namespace medea.controls
{
	partial class frmPickSource
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.cmbSources = new medea.controls.uComboBox();
			this.label11 = new System.Windows.Forms.Label();
			this.panMain.SuspendLayout();
			this.SuspendLayout();
			//
			// panButtons
			//
			this.panButtons.Location = new System.Drawing.Point(0, 139);
			this.panButtons.Size = new System.Drawing.Size(441, 55);
			//
			// panMain
			//
			this.panMain.Controls.Add(this.cmbSources);
			this.panMain.Controls.Add(this.label11);
			this.panMain.Size = new System.Drawing.Size(441, 139);
			//
			// cmbSources
			//
			this.cmbSources.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbSources.FormattingEnabled = true;
			this.cmbSources.Location = new System.Drawing.Point(76, 57);
			this.cmbSources.Name = "cmbSources";
			this.cmbSources.Optional = true;
			this.cmbSources.Size = new System.Drawing.Size(343, 21);
			this.cmbSources.TabIndex = 58;
			//
			// label11
			//
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(27, 60);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(43, 13);
			this.label11.TabIndex = 57;
			this.label11.Text = "Fuente:";
			//
			// frmMetadataFileEdit
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(441, 194);
			this.Name = "frmMetadataFileEdit";
			this.Text = "Agregar fuente";
			this.panMain.ResumeLayout(false);
			this.panMain.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private uComboBox cmbSources;
		private System.Windows.Forms.Label label11;
	}
}

