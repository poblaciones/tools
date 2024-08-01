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
	partial class frmClippingGeographyNew
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
			this.lblGeography = new System.Windows.Forms.Label();
			this.cmbRegion = new medea.controls.uComboBox();
			this.lblRegion = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.lstGeography = new System.Windows.Forms.CheckedListBox();
			this.panMain.SuspendLayout();
			this.SuspendLayout();
			//
			// panButtons
			//
			this.panButtons.Location = new System.Drawing.Point(0, 243);
			this.panButtons.Size = new System.Drawing.Size(380, 55);
			//
			// panMain
			//
			this.panMain.Controls.Add(this.lstGeography);
			this.panMain.Controls.Add(this.lblGeography);
			this.panMain.Controls.Add(this.cmbRegion);
			this.panMain.Controls.Add(this.label1);
			this.panMain.Controls.Add(this.lblRegion);
			this.panMain.Margin = new System.Windows.Forms.Padding(6);
			this.panMain.Size = new System.Drawing.Size(380, 243);
			//
			// lblGeography
			//
			this.lblGeography.AutoSize = true;
			this.lblGeography.Location = new System.Drawing.Point(29, 97);
			this.lblGeography.Name = "lblGeography";
			this.lblGeography.Size = new System.Drawing.Size(62, 13);
			this.lblGeography.TabIndex = 3;
			this.lblGeography.Text = "Geografía:*";
			//
			// cmbRegion
			//
			this.cmbRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbRegion.FormattingEnabled = true;
			this.cmbRegion.Location = new System.Drawing.Point(131, 67);
			this.cmbRegion.Name = "cmbRegion";
			this.cmbRegion.Optional = true;
			this.cmbRegion.Size = new System.Drawing.Size(160, 21);
			this.cmbRegion.TabIndex = 2;
			//
			// lblRegion
			//
			this.lblRegion.AutoSize = true;
			this.lblRegion.Location = new System.Drawing.Point(29, 70);
			this.lblRegion.Name = "lblRegion";
			this.lblRegion.Size = new System.Drawing.Size(48, 13);
			this.lblRegion.TabIndex = 1;
			this.lblRegion.Text = "Región:*";
			//
			// label1
			//
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 28);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(306, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Indique los niveles de región y de geografía que desea asociar:";
			//
			// lstGeography
			//
			this.lstGeography.FormattingEnabled = true;
			this.lstGeography.Location = new System.Drawing.Point(131, 97);
			this.lstGeography.Name = "lstGeography";
			this.lstGeography.Size = new System.Drawing.Size(157, 124);
			this.lstGeography.TabIndex = 5;
			//
			// frmClippingGeographyNew
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(380, 298);
			this.Margin = new System.Windows.Forms.Padding(6);
			this.Name = "frmClippingGeographyNew";
			this.ShowInTaskbar = false;
			this.Text = "Región x Geografía";
			this.panMain.ResumeLayout(false);
			this.panMain.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lblGeography;
		private System.Windows.Forms.Label lblRegion;
		private controls.uComboBox cmbRegion;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckedListBox lstGeography;
	}
}

