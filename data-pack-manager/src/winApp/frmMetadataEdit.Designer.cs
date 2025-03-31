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
namespace medea.winApp
{
	partial class frmMetadataEdit
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.uMetadata = new medea.controls.uMetadata();
			this.panMain.SuspendLayout();
			this.SuspendLayout();
			//
			// panButtons
			//
			this.panButtons.Location = new System.Drawing.Point(0, 454);
			this.panButtons.Size = new System.Drawing.Size(772, 55);
			//
			// panMain
			//
			this.panMain.Controls.Add(this.uMetadata);
			this.panMain.Padding = new System.Windows.Forms.Padding(10);
			this.panMain.Size = new System.Drawing.Size(772, 454);
			//
			// panel1
			//
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(6);
			this.panel1.Size = new System.Drawing.Size(772, 509);
			this.panel1.TabIndex = 3;
			//
			// uMetadata
			//
			this.uMetadata.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uMetadata.Location = new System.Drawing.Point(10, 10);
			this.uMetadata.Name = "uMetadata";
			this.uMetadata.Size = new System.Drawing.Size(752, 434);
			this.uMetadata.TabIndex = 30;
			this.uMetadata.Load += new System.EventHandler(this.uMetadata_Load);
			//
			// frmMetadataEdit
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(772, 509);
			this.Controls.Add(this.panel1);
			this.Name = "frmMetadataEdit";
			this.ShowInTaskbar = false;
			this.Text = "Metadatos";
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.panButtons, 0);
			this.Controls.SetChildIndex(this.panMain, 0);
			this.panMain.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Panel panel1;
		private controls.uMetadata uMetadata;
	}
}

