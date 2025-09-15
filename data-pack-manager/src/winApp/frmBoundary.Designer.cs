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
	partial class frmBoundary
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
			this.listView = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.cmsTree = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.panel2 = new medea.controls.uHeader();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.uEntity = new medea.controls.uEntity();
			this.chkItems = new System.Windows.Forms.CheckBox();
			this.uHeader1 = new medea.controls.uHeader();
			this.lstContainer = new System.Windows.Forms.SplitContainer();
			this.lstCount = new System.Windows.Forms.Label();
			this.uGeometry1 = new medea.controls.uGeometry();
			this.pnlActions = new System.Windows.Forms.Panel();
			this.btnNew = new System.Windows.Forms.Button();
			this.btnEdit = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnNewVersion = new System.Windows.Forms.Button();
			this.cmsItems.SuspendLayout();
			this.cmsTree.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.lstContainer)).BeginInit();
			this.lstContainer.Panel1.SuspendLayout();
			this.lstContainer.Panel2.SuspendLayout();
			this.lstContainer.SuspendLayout();
			this.pnlActions.SuspendLayout();
			this.SuspendLayout();
			//
			// cmsItems
			//
			this.cmsItems.ImageScalingSize = new System.Drawing.Size(28, 28);
			this.cmsItems.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCopyItem,
            this.toolStripSeparator1,
            this.mnuDeleteItem});
			this.cmsItems.Name = "cmsTree";
			this.cmsItems.Size = new System.Drawing.Size(68, 54);
			//
			// mnuCopyItem
			//
			this.mnuCopyItem.Name = "mnuCopyItem";
			this.mnuCopyItem.Size = new System.Drawing.Size(67, 22);
			//
			// toolStripSeparator1
			//
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(64, 6);
			//
			// mnuDeleteItem
			//
			this.mnuDeleteItem.Name = "mnuDeleteItem";
			this.mnuDeleteItem.Size = new System.Drawing.Size(67, 22);
			//
			// listView
			//
			this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
			this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView.HideSelection = false;
			this.listView.Location = new System.Drawing.Point(0, 26);
			this.listView.Name = "listView";
			this.listView.Size = new System.Drawing.Size(381, 411);
			this.listView.TabIndex = 0;
			this.listView.UseCompatibleStateImageBehavior = false;
			this.listView.View = System.Windows.Forms.View.Details;
			this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
			this.listView.DoubleClick += new System.EventHandler(this.listView_DoubleClick);
			//
			// columnHeader1
			//
			this.columnHeader1.Text = "Nombre";
			this.columnHeader1.Width = 150;
			//
			// columnHeader2
			//
			this.columnHeader2.Text = "Grupo";
			this.columnHeader2.Width = 150;
			//
			// columnHeader3
			//
			this.columnHeader3.Text = "Visible";
			//
			// cmsTree
			//
			this.cmsTree.ImageScalingSize = new System.Drawing.Size(28, 28);
			this.cmsTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEdit,
            this.mnuDelete});
			this.cmsTree.Name = "cmsTree";
			this.cmsTree.Size = new System.Drawing.Size(135, 48);
			//
			// mnuEdit
			//
			this.mnuEdit.Name = "mnuEdit";
			this.mnuEdit.Size = new System.Drawing.Size(134, 22);
			this.mnuEdit.Text = "&Modificar...";
			this.mnuEdit.Click += new System.EventHandler(this.mnuEdit_Click);
			//
			// mnuDelete
			//
			this.mnuDelete.Name = "mnuDelete";
			this.mnuDelete.Size = new System.Drawing.Size(134, 22);
			this.mnuDelete.Text = "&Eliminar";
			this.mnuDelete.Click += new System.EventHandler(this.mnuDelete_Click);
			//
			// splitContainer1
			//
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			//
			// splitContainer1.Panel1
			//
			this.splitContainer1.Panel1.Controls.Add(this.listView);
			this.splitContainer1.Panel1.Controls.Add(this.panel2);
			//
			// splitContainer1.Panel2
			//
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Size = new System.Drawing.Size(1162, 437);
			this.splitContainer1.SplitterDistance = 381;
			this.splitContainer1.SplitterWidth = 6;
			this.splitContainer1.TabIndex = 2;
			//
			// panel2
			//
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.ForeColor = System.Drawing.Color.Black;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(381, 26);
			this.panel2.TabIndex = 0;
			this.panel2.Text = "Delimitaciones";
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
			this.splitContainer2.Panel1.Controls.Add(this.uEntity);
			this.splitContainer2.Panel1.Controls.Add(this.chkItems);
			this.splitContainer2.Panel1.Controls.Add(this.uHeader1);
			//
			// splitContainer2.Panel2
			//
			this.splitContainer2.Panel2.Controls.Add(this.lstContainer);
			this.splitContainer2.Size = new System.Drawing.Size(775, 437);
			this.splitContainer2.SplitterDistance = 252;
			this.splitContainer2.SplitterWidth = 6;
			this.splitContainer2.TabIndex = 0;
			//
			// uEntity
			//
			this.uEntity.BackColor = System.Drawing.SystemColors.Control;
			this.uEntity.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uEntity.Location = new System.Drawing.Point(0, 26);
			this.uEntity.Margin = new System.Windows.Forms.Padding(6);
			this.uEntity.Name = "uEntity";
			this.uEntity.Size = new System.Drawing.Size(775, 204);
			this.uEntity.TabIndex = 1;
			//
			// chkItems
			//
			this.chkItems.AutoSize = true;
			this.chkItems.BackColor = System.Drawing.Color.Silver;
			this.chkItems.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.chkItems.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.chkItems.Location = new System.Drawing.Point(0, 230);
			this.chkItems.Margin = new System.Windows.Forms.Padding(2);
			this.chkItems.Name = "chkItems";
			this.chkItems.Padding = new System.Windows.Forms.Padding(1, 3, 3, 2);
			this.chkItems.Size = new System.Drawing.Size(775, 22);
			this.chkItems.TabIndex = 2;
			this.chkItems.Text = "Mostrar ítems";
			this.chkItems.UseVisualStyleBackColor = false;
			this.chkItems.Visible = false;
			this.chkItems.CheckedChanged += new System.EventHandler(this.chkItems_CheckedChanged);
			//
			// uHeader1
			//
			this.uHeader1.Dock = System.Windows.Forms.DockStyle.Top;
			this.uHeader1.ForeColor = System.Drawing.Color.Black;
			this.uHeader1.Location = new System.Drawing.Point(0, 0);
			this.uHeader1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.uHeader1.Name = "uHeader1";
			this.uHeader1.Size = new System.Drawing.Size(775, 26);
			this.uHeader1.TabIndex = 0;
			this.uHeader1.Text = "Detalle";
			//
			// lstContainer
			//
			this.lstContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstContainer.Location = new System.Drawing.Point(0, 0);
			this.lstContainer.Name = "lstContainer";
			//
			// lstContainer.Panel1
			//
			this.lstContainer.Panel1.Controls.Add(this.lstCount);
			this.lstContainer.Panel1MinSize = 300;
			//
			// lstContainer.Panel2
			//
			this.lstContainer.Panel2.Controls.Add(this.uGeometry1);
			this.lstContainer.Panel2Collapsed = true;
			this.lstContainer.Size = new System.Drawing.Size(775, 179);
			this.lstContainer.SplitterDistance = 300;
			this.lstContainer.SplitterWidth = 5;
			this.lstContainer.TabIndex = 1;
			//
			// lstCount
			//
			this.lstCount.AutoSize = true;
			this.lstCount.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.lstCount.Location = new System.Drawing.Point(0, 160);
			this.lstCount.Name = "lstCount";
			this.lstCount.Padding = new System.Windows.Forms.Padding(3);
			this.lstCount.Size = new System.Drawing.Size(22, 19);
			this.lstCount.TabIndex = 2;
			this.lstCount.Text = "0.";
			//
			// uGeometry1
			//
			this.uGeometry1.BackColor = System.Drawing.Color.WhiteSmoke;
			this.uGeometry1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uGeometry1.Location = new System.Drawing.Point(0, 0);
			this.uGeometry1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.uGeometry1.Name = "uGeometry1";
			this.uGeometry1.Size = new System.Drawing.Size(96, 100);
			this.uGeometry1.TabIndex = 0;
			//
			// pnlActions
			//
			this.pnlActions.Controls.Add(this.btnNewVersion);
			this.pnlActions.Controls.Add(this.btnNew);
			this.pnlActions.Controls.Add(this.btnEdit);
			this.pnlActions.Controls.Add(this.btnDelete);
			this.pnlActions.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlActions.Location = new System.Drawing.Point(0, 437);
			this.pnlActions.Name = "pnlActions";
			this.pnlActions.Size = new System.Drawing.Size(1162, 35);
			this.pnlActions.TabIndex = 0;
			//
			// btnNew
			//
			this.btnNew.Location = new System.Drawing.Point(12, 6);
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(75, 23);
			this.btnNew.TabIndex = 0;
			this.btnNew.Text = "Nuevo...";
			this.btnNew.UseVisualStyleBackColor = true;
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			//
			// btnEdit
			//
			this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnEdit.Location = new System.Drawing.Point(994, 6);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(75, 23);
			this.btnEdit.TabIndex = 2;
			this.btnEdit.Text = "&Modificar...";
			this.btnEdit.UseVisualStyleBackColor = true;
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			//
			// btnDelete
			//
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDelete.Location = new System.Drawing.Point(1075, 6);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(75, 23);
			this.btnDelete.TabIndex = 3;
			this.btnDelete.Text = "&Eliminar";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			//
			// btnNewVersion
			//
			this.btnNewVersion.Location = new System.Drawing.Point(93, 6);
			this.btnNewVersion.Name = "btnNewVersion";
			this.btnNewVersion.Size = new System.Drawing.Size(112, 23);
			this.btnNewVersion.TabIndex = 4;
			this.btnNewVersion.Text = "Nueva versión...";
			this.btnNewVersion.UseVisualStyleBackColor = true;
			this.btnNewVersion.Click += new System.EventHandler(this.btnNewVersion_Click);
			//
			// frmBoundary
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1162, 472);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.pnlActions);
			this.Name = "frmBoundary";
			this.ShowInTaskbar = false;
			this.Text = "Delimitaciones";
			this.Load += new System.EventHandler(this.frmBoundary_Load);
			this.cmsItems.ResumeLayout(false);
			this.cmsTree.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel1.PerformLayout();
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.lstContainer.Panel1.ResumeLayout(false);
			this.lstContainer.Panel1.PerformLayout();
			this.lstContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.lstContainer)).EndInit();
			this.lstContainer.ResumeLayout(false);
			this.pnlActions.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView listView;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.Panel pnlActions;
		private System.Windows.Forms.Button btnNew;
		private System.Windows.Forms.Button btnEdit;
		private System.Windows.Forms.Button btnDelete;
		private uEntity uEntity;
		private System.Windows.Forms.ContextMenuStrip cmsTree;
		private System.Windows.Forms.ToolStripMenuItem mnuEdit;
		private System.Windows.Forms.ToolStripMenuItem mnuDelete;
		private System.Windows.Forms.ContextMenuStrip cmsItems;
		private System.Windows.Forms.ToolStripMenuItem mnuDeleteItem;
		private System.Windows.Forms.ToolStripMenuItem mnuCopyItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private uHeader panel2;
		private uHeader uHeader1;
		private System.Windows.Forms.SplitContainer lstContainer;
		private uGeometry uGeometry1;
		private System.Windows.Forms.CheckBox chkItems;
		private System.Windows.Forms.Label lstCount;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Button btnNewVersion;
	}
}

