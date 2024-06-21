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
	partial class uSource
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
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btnRemove = new System.Windows.Forms.Button();
			this.btnEdit = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.lstItems = new System.Windows.Forms.ListBox();
			this.label11 = new System.Windows.Forms.Label();
			this.btnNew = new System.Windows.Forms.Button();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			//
			// groupBox2
			//
			this.groupBox2.Controls.Add(this.btnRemove);
			this.groupBox2.Controls.Add(this.btnEdit);
			this.groupBox2.Controls.Add(this.btnNew);
			this.groupBox2.Controls.Add(this.btnAdd);
			this.groupBox2.Controls.Add(this.lstItems);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(0, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(521, 315);
			this.groupBox2.TabIndex = 34;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Fuentes";
			//
			// btnRemove
			//
			this.btnRemove.Location = new System.Drawing.Point(405, 275);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(75, 23);
			this.btnRemove.TabIndex = 60;
			this.btnRemove.Text = "Remover";
			this.btnRemove.UseVisualStyleBackColor = true;
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			//
			// btnEdit
			//
			this.btnEdit.Location = new System.Drawing.Point(324, 275);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(75, 23);
			this.btnEdit.TabIndex = 59;
			this.btnEdit.Text = "Editar...";
			this.btnEdit.UseVisualStyleBackColor = true;
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			//
			// btnAdd
			//
			this.btnAdd.Location = new System.Drawing.Point(243, 275);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(75, 23);
			this.btnAdd.TabIndex = 58;
			this.btnAdd.Text = "Agregar...";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			//
			// lstItems
			//
			this.lstItems.FormattingEnabled = true;
			this.lstItems.Location = new System.Drawing.Point(96, 19);
			this.lstItems.Name = "lstItems";
			this.lstItems.Size = new System.Drawing.Size(384, 238);
			this.lstItems.TabIndex = 57;
			//
			// label11
			//
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(16, 23);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(43, 13);
			this.label11.TabIndex = 55;
			this.label11.Text = "Fuente:";
			//
			// btnNew
			//
			this.btnNew.Location = new System.Drawing.Point(96, 275);
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(75, 23);
			this.btnNew.TabIndex = 58;
			this.btnNew.Text = "Nueva...";
			this.btnNew.UseVisualStyleBackColor = true;
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			//
			// uSource
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox2);
			this.Name = "uSource";
			this.Size = new System.Drawing.Size(521, 315);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.Button btnEdit;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.ListBox lstItems;
		private System.Windows.Forms.Button btnNew;
	}
}
