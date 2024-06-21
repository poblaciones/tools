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
	partial class uFile
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
			this.lblFile = new System.Windows.Forms.Label();
			this.btnEdit = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.lnkFile = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			//
			// lblFile
			//
			this.lblFile.AutoSize = true;
			this.lblFile.Location = new System.Drawing.Point(5, 3);
			this.lblFile.Name = "lblFile";
			this.lblFile.Size = new System.Drawing.Size(50, 13);
			this.lblFile.TabIndex = 0;
			this.lblFile.Text = "Archivo:*";
			//
			// btnEdit
			//
			this.btnEdit.Location = new System.Drawing.Point(2, 28);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(75, 23);
			this.btnEdit.TabIndex = 2;
			this.btnEdit.Text = "Examinar...";
			this.btnEdit.UseVisualStyleBackColor = true;
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			//
			// btnDelete
			//
			this.btnDelete.Location = new System.Drawing.Point(83, 28);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(75, 23);
			this.btnDelete.TabIndex = 3;
			this.btnDelete.Text = "&Eliminar";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			//
			// lnkFile
			//
			this.lnkFile.AutoSize = true;
			this.lnkFile.Location = new System.Drawing.Point(61, 3);
			this.lnkFile.Name = "lnkFile";
			this.lnkFile.Size = new System.Drawing.Size(42, 13);
			this.lnkFile.TabIndex = 1;
			this.lnkFile.TabStop = true;
			this.lnkFile.Text = "archivo";
			this.lnkFile.Visible = false;
			this.lnkFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkFile_LinkClicked);
			//
			// uFile
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.lnkFile);
			this.Controls.Add(this.lblFile);
			this.Controls.Add(this.btnEdit);
			this.Controls.Add(this.btnDelete);
			this.Name = "uFile";
			this.Size = new System.Drawing.Size(165, 55);
			this.Load += new System.EventHandler(this.uFile_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label lblFile;
		private System.Windows.Forms.Button btnEdit;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.LinkLabel lnkFile;
	}
}
