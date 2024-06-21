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
using medea.controls;
namespace medea.winApp
{
	partial class frmPickName
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
			this.lblFile = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.panMain.SuspendLayout();
			this.SuspendLayout();
			//
			// panButtons
			//
			this.panButtons.Location = new System.Drawing.Point(0, 137);
			this.panButtons.Size = new System.Drawing.Size(451, 55);
			//
			// panMain
			//
			this.panMain.Controls.Add(this.txtName);
			this.panMain.Controls.Add(this.lblFile);
			this.panMain.Controls.Add(this.lblCaption);
			this.panMain.Size = new System.Drawing.Size(451, 137);
			//
			// lblCaption
			//
			this.lblCaption.Location = new System.Drawing.Point(16, 26);
			this.lblCaption.Name = "lblCaption";
			this.lblCaption.Size = new System.Drawing.Size(334, 19);
			this.lblCaption.TabIndex = 0;
			this.lblCaption.Text = "Indique el nombre para la nueva obra.";
			//
			// lblFile
			//
			this.lblFile.AutoSize = true;
			this.lblFile.Location = new System.Drawing.Point(28, 70);
			this.lblFile.Name = "lblFile";
			this.lblFile.Size = new System.Drawing.Size(51, 13);
			this.lblFile.TabIndex = 1;
			this.lblFile.Text = "Nombre*:";
			//
			// txtName
			//
			this.txtName.Location = new System.Drawing.Point(118, 67);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(240, 20);
			this.txtName.TabIndex = 2;
			//
			// frmPickName
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(451, 192);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "frmPickName";
			this.ShowInTaskbar = false;
			this.Text = "Guardar como...";
			this.panMain.ResumeLayout(false);
			this.panMain.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lblCaption;
		private System.Windows.Forms.Label lblFile;
		private System.Windows.Forms.TextBox txtName;
	}
}

