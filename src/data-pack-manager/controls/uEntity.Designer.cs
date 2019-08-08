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
﻿namespace medea.controls
{
	partial class uEntity
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.lstGrid = new System.Windows.Forms.ListView();
			this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.cmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnuCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.cmsMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// lstGrid
			// 
			this.lstGrid.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colValue});
			this.lstGrid.ContextMenuStrip = this.cmsMenu;
			this.lstGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstGrid.FullRowSelect = true;
			this.lstGrid.HideSelection = false;
			this.lstGrid.Location = new System.Drawing.Point(0, 0);
			this.lstGrid.Name = "lstGrid";
			this.lstGrid.Size = new System.Drawing.Size(404, 193);
			this.lstGrid.TabIndex = 1;
			this.lstGrid.UseCompatibleStateImageBehavior = false;
			this.lstGrid.View = System.Windows.Forms.View.Details;
			// 
			// colName
			// 
			this.colName.Text = "Título";
			this.colName.Width = 97;
			// 
			// colValue
			// 
			this.colValue.Text = "Descripción";
			this.colValue.Width = 163;
			// 
			// cmsMenu
			// 
			this.cmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCopy});
			this.cmsMenu.Name = "cmsMenu";
			this.cmsMenu.Size = new System.Drawing.Size(106, 26);
			// 
			// mnuCopy
			// 
			this.mnuCopy.Name = "mnuCopy";
			this.mnuCopy.Size = new System.Drawing.Size(105, 22);
			this.mnuCopy.Text = "Copiar";
			this.mnuCopy.Click += new System.EventHandler(this.mnuCopy_Click);
			// 
			// uEntity
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.lstGrid);
			this.Name = "uEntity";
			this.Size = new System.Drawing.Size(404, 193);
			this.cmsMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView lstGrid;
		private System.Windows.Forms.ColumnHeader colName;
		private System.Windows.Forms.ColumnHeader colValue;
		private System.Windows.Forms.ContextMenuStrip cmsMenu;
		private System.Windows.Forms.ToolStripMenuItem mnuCopy;
	}
}
