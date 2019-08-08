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
	partial class uMetadataFiles
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
			this.btnDown = new System.Windows.Forms.Button();
			this.btnUp = new System.Windows.Forms.Button();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.panel3 = new System.Windows.Forms.Panel();
			this.btnNewFile = new System.Windows.Forms.Button();
			this.btnEditFile = new System.Windows.Forms.Button();
			this.btnDeleteFile = new System.Windows.Forms.Button();
			this.lstFiles = new System.Windows.Forms.ListView();
			this.colCaption = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colRevision = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colDataset = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.groupBox6.SuspendLayout();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			//
			// btnDown
			//
			this.btnDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDown.Location = new System.Drawing.Point(668, 179);
			this.btnDown.Name = "btnDown";
			this.btnDown.Size = new System.Drawing.Size(16, 19);
			this.btnDown.TabIndex = 29;
			this.btnDown.Text = "▼";
			this.btnDown.UseVisualStyleBackColor = true;
			this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
			//
			// btnUp
			//
			this.btnUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnUp.Location = new System.Drawing.Point(668, 162);
			this.btnUp.Name = "btnUp";
			this.btnUp.Size = new System.Drawing.Size(16, 19);
			this.btnUp.TabIndex = 28;
			this.btnUp.Text = "▲";
			this.btnUp.UseVisualStyleBackColor = true;
			this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
			//
			// groupBox6
			//
			this.groupBox6.Controls.Add(this.panel3);
			this.groupBox6.Controls.Add(this.lstFiles);
			this.groupBox6.Location = new System.Drawing.Point(3, 3);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(662, 233);
			this.groupBox6.TabIndex = 27;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Adjuntos";
			//
			// panel3
			//
			this.panel3.Controls.Add(this.btnNewFile);
			this.panel3.Controls.Add(this.btnEditFile);
			this.panel3.Controls.Add(this.btnDeleteFile);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel3.Location = new System.Drawing.Point(3, 197);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(656, 33);
			this.panel3.TabIndex = 4;
			//
			// btnNewFile
			//
			this.btnNewFile.Location = new System.Drawing.Point(9, 6);
			this.btnNewFile.Name = "btnNewFile";
			this.btnNewFile.Size = new System.Drawing.Size(75, 23);
			this.btnNewFile.TabIndex = 0;
			this.btnNewFile.Text = "&Nuevo...";
			this.btnNewFile.UseVisualStyleBackColor = true;
			this.btnNewFile.Click += new System.EventHandler(this.btnNewFile_Click);
			//
			// btnEditFile
			//
			this.btnEditFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnEditFile.Location = new System.Drawing.Point(495, 5);
			this.btnEditFile.Margin = new System.Windows.Forms.Padding(2);
			this.btnEditFile.Name = "btnEditFile";
			this.btnEditFile.Size = new System.Drawing.Size(75, 23);
			this.btnEditFile.TabIndex = 2;
			this.btnEditFile.Text = "&Modificar...";
			this.btnEditFile.UseVisualStyleBackColor = true;
			this.btnEditFile.Click += new System.EventHandler(this.btnEditFile_Click);
			//
			// btnDeleteFile
			//
			this.btnDeleteFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDeleteFile.Location = new System.Drawing.Point(574, 5);
			this.btnDeleteFile.Margin = new System.Windows.Forms.Padding(2);
			this.btnDeleteFile.Name = "btnDeleteFile";
			this.btnDeleteFile.Size = new System.Drawing.Size(75, 23);
			this.btnDeleteFile.TabIndex = 3;
			this.btnDeleteFile.Text = "&Eliminar";
			this.btnDeleteFile.UseVisualStyleBackColor = true;
			this.btnDeleteFile.Click += new System.EventHandler(this.btnDeleteFile_Click);
			//
			// lstFiles
			//
			this.lstFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colCaption,
            this.colRevision,
            this.colDataset});
			this.lstFiles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstFiles.FullRowSelect = true;
			this.lstFiles.HideSelection = false;
			this.lstFiles.Location = new System.Drawing.Point(3, 16);
			this.lstFiles.Name = "lstFiles";
			this.lstFiles.Size = new System.Drawing.Size(656, 214);
			this.lstFiles.TabIndex = 3;
			this.lstFiles.UseCompatibleStateImageBehavior = false;
			this.lstFiles.View = System.Windows.Forms.View.Details;
			this.lstFiles.DoubleClick += new System.EventHandler(this.lstFiles_DoubleClick);
			//
			// colCaption
			//
			this.colCaption.Text = "Nombre";
			this.colCaption.Width = 136;
			//
			// colRevision
			//
			this.colRevision.Text = "Archivo";
			this.colRevision.Width = 98;
			//
			// colDataset
			//
			this.colDataset.Text = "Ruta";
			this.colDataset.Width = 369;
			//
			// uMetadataFiles
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnDown);
			this.Controls.Add(this.btnUp);
			this.Controls.Add(this.groupBox6);
			this.Name = "uMetadataFiles";
			this.Size = new System.Drawing.Size(687, 287);
			this.groupBox6.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnDown;
		private System.Windows.Forms.Button btnUp;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Button btnNewFile;
		private System.Windows.Forms.Button btnEditFile;
		private System.Windows.Forms.Button btnDeleteFile;
		private System.Windows.Forms.ListView lstFiles;
		private System.Windows.Forms.ColumnHeader colCaption;
		private System.Windows.Forms.ColumnHeader colRevision;
		private System.Windows.Forms.ColumnHeader colDataset;
	}
}
