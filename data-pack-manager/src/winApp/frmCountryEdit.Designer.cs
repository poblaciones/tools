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
using medea.controls;
namespace medea.winApp
{
	partial class frmCountryEdit
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
            this.lblCaption = new System.Windows.Forms.Label();
            this.lblDatasetColumn = new System.Windows.Forms.Label();
            this.cmbGeography = new medea.controls.uComboBox();
            this.lblFile = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.panMain.SuspendLayout();
            this.SuspendLayout();
            //
            // panButtons
            //
            this.panButtons.Location = new System.Drawing.Point(0, 181);
            this.panButtons.Size = new System.Drawing.Size(379, 55);
            //
            // panMain
            //
            this.panMain.Controls.Add(this.txtDescription);
            this.panMain.Controls.Add(this.lblFile);
            this.panMain.Controls.Add(this.lblDatasetColumn);
            this.panMain.Controls.Add(this.cmbGeography);
            this.panMain.Controls.Add(this.lblCaption);
            this.panMain.Size = new System.Drawing.Size(379, 181);
            //
            // lblCaption
            //
            this.lblCaption.Location = new System.Drawing.Point(12, 32);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(334, 31);
            this.lblCaption.TabIndex = 0;
            this.lblCaption.Text = "Indique los atributos para el país seleccionado:";
            //
            // lblDatasetColumn
            //
            this.lblDatasetColumn.AutoSize = true;
            this.lblDatasetColumn.Location = new System.Drawing.Point(24, 96);
            this.lblDatasetColumn.Name = "lblDatasetColumn";
            this.lblDatasetColumn.Size = new System.Drawing.Size(98, 13);
            this.lblDatasetColumn.TabIndex = 3;
            this.lblDatasetColumn.Text = "Cartografia mínima:";
            //
            // cmbGeography
            //
            this.cmbGeography.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGeography.FormattingEnabled = true;
            this.cmbGeography.Location = new System.Drawing.Point(145, 93);
            this.cmbGeography.Name = "cmbGeography";
            this.cmbGeography.Optional = true;
            this.cmbGeography.Size = new System.Drawing.Size(160, 21);
            this.cmbGeography.TabIndex = 4;
            //
            // lblFile
            //
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(24, 69);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(51, 13);
            this.lblFile.TabIndex = 1;
            this.lblFile.Text = "Nombre*:";
            //
            // txtDescription
            //
            this.txtDescription.Location = new System.Drawing.Point(145, 66);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(160, 20);
            this.txtDescription.TabIndex = 2;
            //
            // frmCountryEdit
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 236);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmCountryEdit";
            this.ShowInTaskbar = false;
            this.Text = "Detalle de país";
            this.panMain.ResumeLayout(false);
            this.panMain.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lblCaption;
		private System.Windows.Forms.Label lblFile;
		private System.Windows.Forms.Label lblDatasetColumn;
		private controls.uComboBox cmbGeography;
		private System.Windows.Forms.TextBox txtDescription;
	}
}

