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
	partial class frmGradient
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
			this.cmsTree = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.chkItems = new System.Windows.Forms.CheckBox();
			this.lstContainer = new System.Windows.Forms.SplitContainer();
			this.lstItems = new System.Windows.Forms.ListView();
			this.colX = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colY = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colZ = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lstCount = new System.Windows.Forms.Label();
			this.pnlActions = new System.Windows.Forms.Panel();
			this.btnNew = new System.Windows.Forms.Button();
			this.btnEdit = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.panel2 = new medea.controls.uHeader();
			this.uEntity = new medea.controls.uEntity();
			this.uHeader1 = new medea.controls.uHeader();
			this.uGeometry1 = new medea.controls.uGeometry();
			this.uHeader2 = new medea.controls.uHeader();
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
			this.cmsItems.Size = new System.Drawing.Size(110, 54);
			// 
			// mnuCopyItem
			// 
			this.mnuCopyItem.Name = "mnuCopyItem";
			this.mnuCopyItem.Size = new System.Drawing.Size(109, 22);
			this.mnuCopyItem.Text = "Copiar";
			this.mnuCopyItem.Click += new System.EventHandler(this.mnuCopyItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(106, 6);
			// 
			// mnuDeleteItem
			// 
			this.mnuDeleteItem.Name = "mnuDeleteItem";
			this.mnuDeleteItem.Size = new System.Drawing.Size(109, 22);
			// 
			// listView
			// 
			this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView.HideSelection = false;
			this.listView.Location = new System.Drawing.Point(0, 26);
			this.listView.Name = "listView";
			this.listView.Size = new System.Drawing.Size(184, 256);
			this.listView.TabIndex = 0;
			this.listView.UseCompatibleStateImageBehavior = false;
			this.listView.View = System.Windows.Forms.View.Details;
			this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
			this.listView.DoubleClick += new System.EventHandler(this.listView_DoubleClick);
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
			this.splitContainer1.Size = new System.Drawing.Size(561, 282);
			this.splitContainer1.SplitterDistance = 184;
			this.splitContainer1.SplitterWidth = 6;
			this.splitContainer1.TabIndex = 2;
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
			this.splitContainer2.Panel2.Controls.Add(this.uHeader2);
			this.splitContainer2.Size = new System.Drawing.Size(371, 282);
			this.splitContainer2.SplitterDistance = 163;
			this.splitContainer2.SplitterWidth = 6;
			this.splitContainer2.TabIndex = 0;
			// 
			// chkItems
			// 
			this.chkItems.AutoSize = true;
			this.chkItems.BackColor = System.Drawing.Color.Silver;
			this.chkItems.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.chkItems.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.chkItems.Location = new System.Drawing.Point(0, 141);
			this.chkItems.Margin = new System.Windows.Forms.Padding(2);
			this.chkItems.Name = "chkItems";
			this.chkItems.Padding = new System.Windows.Forms.Padding(1, 3, 3, 2);
			this.chkItems.Size = new System.Drawing.Size(371, 22);
			this.chkItems.TabIndex = 2;
			this.chkItems.Text = "Mostrar ítems";
			this.chkItems.UseVisualStyleBackColor = false;
			this.chkItems.Visible = false;
			this.chkItems.CheckedChanged += new System.EventHandler(this.chkItems_CheckedChanged);
			// 
			// lstContainer
			// 
			this.lstContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstContainer.Location = new System.Drawing.Point(0, 26);
			this.lstContainer.Name = "lstContainer";
			// 
			// lstContainer.Panel1
			// 
			this.lstContainer.Panel1.Controls.Add(this.lstItems);
			this.lstContainer.Panel1.Controls.Add(this.lstCount);
			this.lstContainer.Panel1MinSize = 300;
			// 
			// lstContainer.Panel2
			// 
			this.lstContainer.Panel2.Controls.Add(this.uGeometry1);
			this.lstContainer.Panel2Collapsed = true;
			this.lstContainer.Size = new System.Drawing.Size(371, 87);
			this.lstContainer.SplitterDistance = 300;
			this.lstContainer.SplitterWidth = 5;
			this.lstContainer.TabIndex = 1;
			// 
			// lstItems
			// 
			this.lstItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colX,
            this.colY,
            this.colZ});
			this.lstItems.ContextMenuStrip = this.cmsItems;
			this.lstItems.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstItems.FullRowSelect = true;
			this.lstItems.HideSelection = false;
			this.lstItems.Location = new System.Drawing.Point(0, 0);
			this.lstItems.Name = "lstItems";
			this.lstItems.Size = new System.Drawing.Size(371, 68);
			this.lstItems.TabIndex = 0;
			this.lstItems.UseCompatibleStateImageBehavior = false;
			this.lstItems.View = System.Windows.Forms.View.Details;
			this.lstItems.SelectedIndexChanged += new System.EventHandler(this.lstItems_SelectedIndexChanged);
			// 
			// colX
			// 
			this.colX.Text = "X";
			this.colX.Width = 93;
			// 
			// colY
			// 
			this.colY.Text = "Y";
			this.colY.Width = 171;
			// 
			// colZ
			// 
			this.colZ.Text = "Z";
			this.colZ.Width = 155;
			// 
			// lstCount
			// 
			this.lstCount.AutoSize = true;
			this.lstCount.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.lstCount.Location = new System.Drawing.Point(0, 68);
			this.lstCount.Name = "lstCount";
			this.lstCount.Padding = new System.Windows.Forms.Padding(3);
			this.lstCount.Size = new System.Drawing.Size(22, 19);
			this.lstCount.TabIndex = 2;
			this.lstCount.Text = "0.";
			// 
			// pnlActions
			// 
			this.pnlActions.Controls.Add(this.btnNew);
			this.pnlActions.Controls.Add(this.btnEdit);
			this.pnlActions.Controls.Add(this.btnDelete);
			this.pnlActions.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlActions.Location = new System.Drawing.Point(0, 282);
			this.pnlActions.Name = "pnlActions";
			this.pnlActions.Size = new System.Drawing.Size(561, 35);
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
			this.btnEdit.Location = new System.Drawing.Point(393, 6);
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
			this.btnDelete.Location = new System.Drawing.Point(474, 6);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(75, 23);
			this.btnDelete.TabIndex = 3;
			this.btnDelete.Text = "&Eliminar";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Nombre";
			this.columnHeader1.Width = 150;
			// 
			// panel2
			// 
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.ForeColor = System.Drawing.Color.Black;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(184, 26);
			this.panel2.TabIndex = 0;
			this.panel2.Text = "Gradientes";
			// 
			// uEntity
			// 
			this.uEntity.BackColor = System.Drawing.SystemColors.Control;
			this.uEntity.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uEntity.Location = new System.Drawing.Point(0, 26);
			this.uEntity.Margin = new System.Windows.Forms.Padding(6);
			this.uEntity.Name = "uEntity";
			this.uEntity.Size = new System.Drawing.Size(371, 115);
			this.uEntity.TabIndex = 1;
			// 
			// uHeader1
			// 
			this.uHeader1.Dock = System.Windows.Forms.DockStyle.Top;
			this.uHeader1.ForeColor = System.Drawing.Color.Black;
			this.uHeader1.Location = new System.Drawing.Point(0, 0);
			this.uHeader1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.uHeader1.Name = "uHeader1";
			this.uHeader1.Size = new System.Drawing.Size(371, 26);
			this.uHeader1.TabIndex = 0;
			this.uHeader1.Text = "Detalle";
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
			// uHeader2
			// 
			this.uHeader2.Dock = System.Windows.Forms.DockStyle.Top;
			this.uHeader2.ForeColor = System.Drawing.Color.Black;
			this.uHeader2.Location = new System.Drawing.Point(0, 0);
			this.uHeader2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.uHeader2.Name = "uHeader2";
			this.uHeader2.Size = new System.Drawing.Size(371, 26);
			this.uHeader2.TabIndex = 0;
			this.uHeader2.Text = "Items";
			// 
			// frmGradient
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(561, 317);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.pnlActions);
			this.Name = "frmGradient";
			this.ShowInTaskbar = false;
			this.Text = "Gradientes";
			this.Load += new System.EventHandler(this.frmGradient_Load);
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
		private System.Windows.Forms.ListView lstItems;
		private System.Windows.Forms.ColumnHeader colX;
		private System.Windows.Forms.ColumnHeader colY;
		private System.Windows.Forms.ColumnHeader colZ;
		private uGeometry uGeometry1;
		private uHeader uHeader2;
		private System.Windows.Forms.CheckBox chkItems;
		private System.Windows.Forms.Label lstCount;
		private System.Windows.Forms.ColumnHeader columnHeader1;
	}
}

