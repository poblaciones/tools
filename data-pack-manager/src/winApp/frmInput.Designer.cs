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
namespace medea.winApp
{
	partial class frmInput
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtCaption = new System.Windows.Forms.TextBox();
            this.panMain.SuspendLayout();
            this.SuspendLayout();
            //
            // panButtons
            //
            this.panButtons.Location = new System.Drawing.Point(0, 83);
            this.panButtons.Size = new System.Drawing.Size(398, 55);
            //
            // panMain
            //
            this.panMain.Controls.Add(this.txtCaption);
            this.panMain.Controls.Add(this.lblTitle);
            this.panMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panMain.Size = new System.Drawing.Size(398, 83);
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(23, 26);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(51, 13);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Nombre:*";
            //
            // txtCaption
            //
            this.txtCaption.Location = new System.Drawing.Point(114, 23);
            this.txtCaption.MaxLength = 255;
            this.txtCaption.Name = "txtCaption";
            this.txtCaption.Size = new System.Drawing.Size(257, 20);
            this.txtCaption.TabIndex = 1;
            //
            // frmInput
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 138);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmInput";
            this.ShowInTaskbar = false;
            this.Text = "Completar texto";
            this.panMain.ResumeLayout(false);
            this.panMain.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.TextBox txtCaption;
		private System.Windows.Forms.Label lblTitle;
	}
}

