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
	partial class frmClippingGeography
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
			this.cmsList = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.lstClippingGeography = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnDelete = new System.Windows.Forms.Button();
			this.chkItems = new System.Windows.Forms.CheckBox();
			this.btnNew = new System.Windows.Forms.Button();
			this.uHeader1 = new medea.controls.uHeader();
			this.pnlContainer = new System.Windows.Forms.SplitContainer();
			this.lstItems = new System.Windows.Forms.ListView();
			this.col0 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.col1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lstCount = new System.Windows.Forms.Label();
			this.uHeader2 = new medea.controls.uHeader();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.trvList = new System.Windows.Forms.TreeView();
			this.pnlRegions = new medea.controls.uHeader();
			this.cmsItems.SuspendLayout();
			this.cmsList.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlContainer)).BeginInit();
			this.pnlContainer.Panel1.SuspendLayout();
			this.pnlContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmsItems
			// 
			this.cmsItems.ImageScalingSize = new System.Drawing.Size(28, 28);
			this.cmsItems.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCopyItem});
			this.cmsItems.Name = "cmsTree";
			this.cmsItems.Size = new System.Drawing.Size(110, 26);
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
			this.toolStripSeparator1.Size = new System.Drawing.Size(114, 6);
			// 
			// cmsList
			// 
			this.cmsList.ImageScalingSize = new System.Drawing.Size(28, 28);
			this.cmsList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDelete});
			this.cmsList.Name = "cmsTree";
			this.cmsList.Size = new System.Drawing.Size(118, 26);
			// 
			// mnuDelete
			// 
			this.mnuDelete.Name = "mnuDelete";
			this.mnuDelete.Size = new System.Drawing.Size(117, 22);
			this.mnuDelete.Text = "&Eliminar";
			this.mnuDelete.Click += new System.EventHandler(this.mnuDelete_Click);
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
			this.splitContainer2.Panel1.Controls.Add(this.lstClippingGeography);
			this.splitContainer2.Panel1.Controls.Add(this.panel1);
			this.splitContainer2.Panel1.Controls.Add(this.uHeader1);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.pnlContainer);
			this.splitContainer2.Panel2.Controls.Add(this.uHeader2);
			this.splitContainer2.Size = new System.Drawing.Size(478, 408);
			this.splitContainer2.SplitterDistance = 252;
			this.splitContainer2.SplitterWidth = 6;
			this.splitContainer2.TabIndex = 0;
			// 
			// lstClippingGeography
			// 
			this.lstClippingGeography.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lstClippingGeography.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
			this.lstClippingGeography.ContextMenuStrip = this.cmsList;
			this.lstClippingGeography.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstClippingGeography.FullRowSelect = true;
			this.lstClippingGeography.HideSelection = false;
			this.lstClippingGeography.Location = new System.Drawing.Point(0, 26);
			this.lstClippingGeography.MultiSelect = false;
			this.lstClippingGeography.Name = "lstClippingGeography";
			this.lstClippingGeography.Size = new System.Drawing.Size(478, 172);
			this.lstClippingGeography.TabIndex = 1;
			this.lstClippingGeography.UseCompatibleStateImageBehavior = false;
			this.lstClippingGeography.View = System.Windows.Forms.View.Details;
			this.lstClippingGeography.SelectedIndexChanged += new System.EventHandler(this.lstClippingGeography_SelectedIndexChanged);
			this.lstClippingGeography.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstClippingGeography_KeyDown);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Región";
			this.columnHeader1.Width = 167;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Geografía";
			this.columnHeader2.Width = 135;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.Silver;
			this.panel1.Controls.Add(this.btnDelete);
			this.panel1.Controls.Add(this.chkItems);
			this.panel1.Controls.Add(this.btnNew);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 198);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(478, 54);
			this.panel1.TabIndex = 4;
			// 
			// btnDelete
			// 
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDelete.BackColor = System.Drawing.SystemColors.Control;
			this.btnDelete.Location = new System.Drawing.Point(400, 6);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(75, 23);
			this.btnDelete.TabIndex = 2;
			this.btnDelete.Text = "&Eliminar";
			this.btnDelete.UseVisualStyleBackColor = false;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// chkItems
			// 
			this.chkItems.AutoSize = true;
			this.chkItems.BackColor = System.Drawing.Color.Silver;
			this.chkItems.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.chkItems.Location = new System.Drawing.Point(2, 34);
			this.chkItems.Margin = new System.Windows.Forms.Padding(2);
			this.chkItems.Name = "chkItems";
			this.chkItems.Size = new System.Drawing.Size(88, 17);
			this.chkItems.TabIndex = 4;
			this.chkItems.Text = "Mostrar ítems";
			this.chkItems.UseVisualStyleBackColor = false;
			this.chkItems.CheckedChanged += new System.EventHandler(this.chkItems_CheckedChanged);
			// 
			// btnNew
			// 
			this.btnNew.BackColor = System.Drawing.SystemColors.Control;
			this.btnNew.Location = new System.Drawing.Point(3, 6);
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(75, 23);
			this.btnNew.TabIndex = 0;
			this.btnNew.Text = "Nueva...";
			this.btnNew.UseVisualStyleBackColor = false;
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// uHeader1
			// 
			this.uHeader1.Dock = System.Windows.Forms.DockStyle.Top;
			this.uHeader1.ForeColor = System.Drawing.Color.Black;
			this.uHeader1.Location = new System.Drawing.Point(0, 0);
			this.uHeader1.Margin = new System.Windows.Forms.Padding(6);
			this.uHeader1.Name = "uHeader1";
			this.uHeader1.Size = new System.Drawing.Size(478, 26);
			this.uHeader1.TabIndex = 0;
			this.uHeader1.Text = "Geografías";
			// 
			// pnlContainer
			// 
			this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlContainer.Location = new System.Drawing.Point(0, 26);
			this.pnlContainer.Name = "pnlContainer";
			// 
			// pnlContainer.Panel1
			// 
			this.pnlContainer.Panel1.Controls.Add(this.lstItems);
			this.pnlContainer.Panel1.Controls.Add(this.lstCount);
			this.pnlContainer.Panel1MinSize = 300;
			this.pnlContainer.Panel2Collapsed = true;
			this.pnlContainer.Size = new System.Drawing.Size(478, 124);
			this.pnlContainer.SplitterDistance = 300;
			this.pnlContainer.SplitterWidth = 5;
			this.pnlContainer.TabIndex = 1;
			// 
			// lstItems
			// 
			this.lstItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col0,
            this.col1});
			this.lstItems.ContextMenuStrip = this.cmsItems;
			this.lstItems.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstItems.FullRowSelect = true;
			this.lstItems.HideSelection = false;
			this.lstItems.Location = new System.Drawing.Point(0, 0);
			this.lstItems.Name = "lstItems";
			this.lstItems.Size = new System.Drawing.Size(478, 105);
			this.lstItems.TabIndex = 0;
			this.lstItems.UseCompatibleStateImageBehavior = false;
			this.lstItems.View = System.Windows.Forms.View.Details;
			// 
			// col0
			// 
			this.col0.Text = "Región";
			this.col0.Width = 143;
			// 
			// col1
			// 
			this.col1.Text = "Geografía";
			this.col1.Width = 146;
			// 
			// lstCount
			// 
			this.lstCount.AutoSize = true;
			this.lstCount.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.lstCount.Location = new System.Drawing.Point(0, 105);
			this.lstCount.Name = "lstCount";
			this.lstCount.Padding = new System.Windows.Forms.Padding(3);
			this.lstCount.Size = new System.Drawing.Size(22, 19);
			this.lstCount.TabIndex = 2;
			this.lstCount.Text = "0.";
			// 
			// uHeader2
			// 
			this.uHeader2.Dock = System.Windows.Forms.DockStyle.Top;
			this.uHeader2.ForeColor = System.Drawing.Color.Black;
			this.uHeader2.Location = new System.Drawing.Point(0, 0);
			this.uHeader2.Margin = new System.Windows.Forms.Padding(6);
			this.uHeader2.Name = "uHeader2";
			this.uHeader2.Size = new System.Drawing.Size(478, 26);
			this.uHeader2.TabIndex = 0;
			this.uHeader2.Text = "Items";
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
			this.splitContainer1.Panel1.Controls.Add(this.pnlRegions);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Size = new System.Drawing.Size(680, 408);
			this.splitContainer1.SplitterDistance = 196;
			this.splitContainer1.SplitterWidth = 6;
			this.splitContainer1.TabIndex = 3;
			// 
			// trvList
			// 
			this.trvList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.trvList.HideSelection = false;
			this.trvList.Location = new System.Drawing.Point(0, 26);
			this.trvList.Name = "trvList";
			this.trvList.Size = new System.Drawing.Size(196, 382);
			this.trvList.TabIndex = 1;
			this.trvList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvList_AfterSelect);
			this.trvList.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvList_NodeMouseDoubleClick);
			this.trvList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.trvList_KeyDown);
			// 
			// pnlRegions
			// 
			this.pnlRegions.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlRegions.ForeColor = System.Drawing.Color.Black;
			this.pnlRegions.Location = new System.Drawing.Point(0, 0);
			this.pnlRegions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.pnlRegions.Name = "pnlRegions";
			this.pnlRegions.Size = new System.Drawing.Size(196, 26);
			this.pnlRegions.TabIndex = 0;
			this.pnlRegions.Text = "Regiones x Geografía";
			// 
			// frmClippingGeography
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(680, 408);
			this.Controls.Add(this.splitContainer1);
			this.Name = "frmClippingGeography";
			this.ShowInTaskbar = false;
			this.Text = "Regiones x Geografía";
			this.Load += new System.EventHandler(this.frmClipping_Load);
			this.cmsItems.ResumeLayout(false);
			this.cmsList.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.pnlContainer.Panel1.ResumeLayout(false);
			this.pnlContainer.Panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlContainer)).EndInit();
			this.pnlContainer.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.Button btnNew;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.ContextMenuStrip cmsList;
		private System.Windows.Forms.ToolStripMenuItem mnuDelete;
		private System.Windows.Forms.ContextMenuStrip cmsItems;
		private System.Windows.Forms.ToolStripMenuItem mnuCopyItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private uHeader uHeader1;
		private System.Windows.Forms.SplitContainer pnlContainer;
		private System.Windows.Forms.ListView lstItems;
		private System.Windows.Forms.ColumnHeader col0;
		private System.Windows.Forms.ColumnHeader col1;
		private uHeader uHeader2;
		private System.Windows.Forms.ListView lstClippingGeography;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TreeView trvList;
		private uHeader pnlRegions;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.CheckBox chkItems;
		private System.Windows.Forms.Label lstCount;
	}
}

