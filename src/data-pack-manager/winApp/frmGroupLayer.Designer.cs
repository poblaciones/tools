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
	partial class frmGroupLayer
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
			this.components = new System.ComponentModel.Container();
			this.cmsItems = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnuCopyItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuDeleteItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cmsTree = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnuEditGroup = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuDeleteGroup = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.trvList = new System.Windows.Forms.TreeView();
			this.panel2 = new medea.controls.uHeader();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnNewGroup = new System.Windows.Forms.Button();
			this.btnEditGroup = new System.Windows.Forms.Button();
			this.btnDeleteGroup = new System.Windows.Forms.Button();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.panel3 = new medea.controls.uHeader();
			this.lstItems = new System.Windows.Forms.ListView();
			this.colCaption = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colRevision = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colEnvironment = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.cmsItems.SuspendLayout();
			this.cmsTree.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.SuspendLayout();
			//
			// cmsItems
			//
			this.cmsItems.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCopyItem,
            this.toolStripSeparator1,
            this.mnuDeleteItem});
			this.cmsItems.Name = "cmsTree";
			this.cmsItems.Size = new System.Drawing.Size(111, 54);
			//
			// mnuCopyItem
			//
			this.mnuCopyItem.Name = "mnuCopyItem";
			this.mnuCopyItem.Size = new System.Drawing.Size(110, 22);
			this.mnuCopyItem.Text = "Copiar";
			//
			// toolStripSeparator1
			//
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(107, 6);
			//
			// mnuDeleteItem
			//
			this.mnuDeleteItem.Name = "mnuDeleteItem";
			this.mnuDeleteItem.Size = new System.Drawing.Size(110, 22);
			this.mnuDeleteItem.Text = "Eliminar";
			//
			// cmsTree
			//
			this.cmsTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditGroup,
            this.mnuDeleteGroup});
			this.cmsTree.Name = "cmsTree";
			this.cmsTree.Size = new System.Drawing.Size(130, 48);
			//
			// mnuEditGroup
			//
			this.mnuEditGroup.Name = "mnuEditGroup";
			this.mnuEditGroup.Size = new System.Drawing.Size(129, 22);
			this.mnuEditGroup.Text = "Modificar...";
			this.mnuEditGroup.Click += new System.EventHandler(this.mnuEditGroup_Click);
			//
			// mnuDeleteGroup
			//
			this.mnuDeleteGroup.Name = "mnuDeleteGroup";
			this.mnuDeleteGroup.Size = new System.Drawing.Size(129, 22);
			this.mnuDeleteGroup.Text = "Eliminar";
			this.mnuDeleteGroup.Click += new System.EventHandler(this.mnuDeleteGroup_Click);
			//
			// splitContainer1
			//
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			//
			// splitContainer1.Panel1
			//
			this.splitContainer1.Panel1.Controls.Add(this.trvList);
			this.splitContainer1.Panel1.Controls.Add(this.panel2);
			this.splitContainer1.Panel1.Controls.Add(this.panel1);
			//
			// splitContainer1.Panel2
			//
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Size = new System.Drawing.Size(650, 456);
			this.splitContainer1.SplitterDistance = 216;
			this.splitContainer1.SplitterWidth = 6;
			this.splitContainer1.TabIndex = 2;
			//
			// trvList
			//
			this.trvList.ContextMenuStrip = this.cmsTree;
			this.trvList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.trvList.HideSelection = false;
			this.trvList.Location = new System.Drawing.Point(0, 26);
			this.trvList.Name = "trvList";
			this.trvList.Size = new System.Drawing.Size(216, 393);
			this.trvList.TabIndex = 1;
			this.trvList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvList_AfterSelect);
			this.trvList.DoubleClick += new System.EventHandler(this.trvList_DoubleClick);
			//
			// panel2
			//
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.ForeColor = System.Drawing.Color.Black;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(216, 26);
			this.panel2.TabIndex = 0;
			this.panel2.Text = "Grupos de capas";
			//
			// panel1
			//
			this.panel1.BackColor = System.Drawing.Color.Gainsboro;
			this.panel1.Controls.Add(this.btnNewGroup);
			this.panel1.Controls.Add(this.btnEditGroup);
			this.panel1.Controls.Add(this.btnDeleteGroup);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 419);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(216, 37);
			this.panel1.TabIndex = 2;
			//
			// btnNewGroup
			//
			this.btnNewGroup.BackColor = System.Drawing.SystemColors.Control;
			this.btnNewGroup.Location = new System.Drawing.Point(5, 6);
			this.btnNewGroup.Name = "btnNewGroup";
			this.btnNewGroup.Size = new System.Drawing.Size(65, 23);
			this.btnNewGroup.TabIndex = 0;
			this.btnNewGroup.Text = "Nuevo...";
			this.btnNewGroup.UseVisualStyleBackColor = false;
			this.btnNewGroup.Click += new System.EventHandler(this.btnNewGroup_Click);
			//
			// btnEditGroup
			//
			this.btnEditGroup.BackColor = System.Drawing.SystemColors.Control;
			this.btnEditGroup.Location = new System.Drawing.Point(76, 6);
			this.btnEditGroup.Name = "btnEditGroup";
			this.btnEditGroup.Size = new System.Drawing.Size(70, 23);
			this.btnEditGroup.TabIndex = 1;
			this.btnEditGroup.Text = "Modificar...";
			this.btnEditGroup.UseVisualStyleBackColor = false;
			this.btnEditGroup.Click += new System.EventHandler(this.btnEditGroup_Click);
			//
			// btnDeleteGroup
			//
			this.btnDeleteGroup.BackColor = System.Drawing.SystemColors.Control;
			this.btnDeleteGroup.Location = new System.Drawing.Point(152, 6);
			this.btnDeleteGroup.Name = "btnDeleteGroup";
			this.btnDeleteGroup.Size = new System.Drawing.Size(54, 23);
			this.btnDeleteGroup.TabIndex = 2;
			this.btnDeleteGroup.Text = "Eliminar";
			this.btnDeleteGroup.UseVisualStyleBackColor = false;
			this.btnDeleteGroup.Click += new System.EventHandler(this.btnDeleteGroup_Click);
			//
			// splitContainer2
			//
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			//
			// splitContainer2.Panel1
			//
			this.splitContainer2.Panel1.Controls.Add(this.lstItems);
			this.splitContainer2.Panel1.Controls.Add(this.panel3);
			this.splitContainer2.Panel2Collapsed = true;
			this.splitContainer2.Size = new System.Drawing.Size(428, 456);
			this.splitContainer2.SplitterDistance = 185;
			this.splitContainer2.SplitterWidth = 6;
			this.splitContainer2.TabIndex = 0;
			//
			// panel3
			//
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.ForeColor = System.Drawing.Color.Black;
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(428, 26);
			this.panel3.TabIndex = 0;
			this.panel3.Text = "Vista de capas";
			//
			// lstItems
			//
			this.lstItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colCaption,
            this.colEnvironment,
            this.colRevision});
			this.lstItems.ContextMenuStrip = this.cmsItems;
			this.lstItems.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstItems.FullRowSelect = true;
			this.lstItems.HideSelection = false;
			this.lstItems.Location = new System.Drawing.Point(0, 26);
			this.lstItems.Name = "lstItems";
			this.lstItems.Size = new System.Drawing.Size(428, 430);
			this.lstItems.TabIndex = 1;
			this.lstItems.UseCompatibleStateImageBehavior = false;
			this.lstItems.View = System.Windows.Forms.View.Details;
			//
			// colCaption
			//
			this.colCaption.Text = "Nombre";
			this.colCaption.Width = 154;
			//
			// colRevision
			//
			this.colRevision.Text = "Revisiones";
			this.colRevision.Width = 155;
			//
			// colEnvironment
			//
			this.colEnvironment.Text = "Ambiente";
			this.colEnvironment.Width = 130;
			//
			// frmGroupLayer
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(650, 456);
			this.Controls.Add(this.splitContainer1);
			this.Name = "frmGroupLayer";
			this.Text = "Grupos de capas";
			this.Load += new System.EventHandler(this.frmLayer_Load);
			this.cmsItems.ResumeLayout(false);
			this.cmsTree.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.ContextMenuStrip cmsTree;
		private System.Windows.Forms.ToolStripMenuItem mnuEditGroup;
		private System.Windows.Forms.ToolStripMenuItem mnuDeleteGroup;
		private System.Windows.Forms.ContextMenuStrip cmsItems;
		private System.Windows.Forms.ToolStripMenuItem mnuDeleteItem;
		private System.Windows.Forms.ToolStripMenuItem mnuCopyItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.TreeView trvList;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnNewGroup;
		private System.Windows.Forms.Button btnEditGroup;
		private System.Windows.Forms.Button btnDeleteGroup;
		private uHeader panel2;
		private uHeader panel3;
		private System.Windows.Forms.ListView lstItems;
		private System.Windows.Forms.ColumnHeader colCaption;
		private System.Windows.Forms.ColumnHeader colRevision;
		private System.Windows.Forms.ColumnHeader colEnvironment;
	}
}

