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
using System;

namespace medea.winApp
{
	partial class frmGeographyTuple
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
			this.lstTupleGeography = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.panel1 = new System.Windows.Forms.Panel();
			this.button1 = new System.Windows.Forms.Button();
			this.chkItems = new System.Windows.Forms.CheckBox();
			this.uHeader1 = new medea.controls.uHeader();
			this.pnlContainer = new System.Windows.Forms.SplitContainer();
			this.lstItems = new System.Windows.Forms.ListView();
			this.col0 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.col1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lstCount = new System.Windows.Forms.Label();
			this.uHeader2 = new medea.controls.uHeader();
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
			this.cmsList.Size = new System.Drawing.Size(68, 26);
			// 
			// mnuDelete
			// 
			this.mnuDelete.Name = "mnuDelete";
			this.mnuDelete.Size = new System.Drawing.Size(67, 22);
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
			this.splitContainer2.Panel1.Controls.Add(this.lstTupleGeography);
			this.splitContainer2.Panel1.Controls.Add(this.panel1);
			this.splitContainer2.Panel1.Controls.Add(this.uHeader1);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.pnlContainer);
			this.splitContainer2.Panel2.Controls.Add(this.uHeader2);
			this.splitContainer2.Size = new System.Drawing.Size(680, 408);
			this.splitContainer2.SplitterDistance = 252;
			this.splitContainer2.SplitterWidth = 6;
			this.splitContainer2.TabIndex = 0;
			// 
			// lstTupleGeography
			// 
			this.lstTupleGeography.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lstTupleGeography.CheckBoxes = true;
			this.lstTupleGeography.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
			this.lstTupleGeography.ContextMenuStrip = this.cmsList;
			this.lstTupleGeography.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstTupleGeography.FullRowSelect = true;
			this.lstTupleGeography.HideSelection = false;
			this.lstTupleGeography.Location = new System.Drawing.Point(0, 26);
			this.lstTupleGeography.MultiSelect = false;
			this.lstTupleGeography.Name = "lstTupleGeography";
			this.lstTupleGeography.Size = new System.Drawing.Size(680, 172);
			this.lstTupleGeography.TabIndex = 1;
			this.lstTupleGeography.UseCompatibleStateImageBehavior = false;
			this.lstTupleGeography.View = System.Windows.Forms.View.Details;
			this.lstTupleGeography.SelectedIndexChanged += new System.EventHandler(this.lstClippingGeography_SelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Tipo";
			this.columnHeader1.Width = 167;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Desde";
			this.columnHeader2.Width = 135;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Hacia";
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Inferior";
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.Silver;
			this.panel1.Controls.Add(this.button1);
			this.panel1.Controls.Add(this.chkItems);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 198);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(680, 54);
			this.panel1.TabIndex = 4;
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.SystemColors.Control;
			this.button1.Location = new System.Drawing.Point(3, 6);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 6;
			this.button1.Text = "&Regenerar";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.btnRegenerate_Click);
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
			// uHeader1
			// 
			this.uHeader1.Dock = System.Windows.Forms.DockStyle.Top;
			this.uHeader1.ForeColor = System.Drawing.Color.Black;
			this.uHeader1.Location = new System.Drawing.Point(0, 0);
			this.uHeader1.Margin = new System.Windows.Forms.Padding(6);
			this.uHeader1.Name = "uHeader1";
			this.uHeader1.Size = new System.Drawing.Size(680, 26);
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
			this.pnlContainer.Size = new System.Drawing.Size(680, 124);
			this.pnlContainer.SplitterDistance = 300;
			this.pnlContainer.SplitterWidth = 5;
			this.pnlContainer.TabIndex = 1;
			// 
			// lstItems
			// 
			this.lstItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col0,
            this.col1,
            this.columnHeader5});
			this.lstItems.ContextMenuStrip = this.cmsItems;
			this.lstItems.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstItems.FullRowSelect = true;
			this.lstItems.HideSelection = false;
			this.lstItems.Location = new System.Drawing.Point(0, 0);
			this.lstItems.Name = "lstItems";
			this.lstItems.Size = new System.Drawing.Size(680, 105);
			this.lstItems.TabIndex = 0;
			this.lstItems.UseCompatibleStateImageBehavior = false;
			this.lstItems.View = System.Windows.Forms.View.Details;
			// 
			// col0
			// 
			this.col0.Text = "Desde";
			this.col0.Width = 143;
			// 
			// col1
			// 
			this.col1.Text = "Hacia";
			this.col1.Width = 146;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "GeographyId";
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
			this.uHeader2.Size = new System.Drawing.Size(680, 26);
			this.uHeader2.TabIndex = 0;
			this.uHeader2.Text = "Items";
			// 
			// frmGeographyTuple
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(680, 408);
			this.Controls.Add(this.splitContainer2);
			this.Name = "frmGeographyTuple";
			this.ShowInTaskbar = false;
			this.Text = "Tuplas de geografías";
			this.Load += new System.EventHandler(this.frmGeographyTuple_Load);
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
			this.ResumeLayout(false);

		}

        private void frmGeographyTuple_Load(object sender, EventArgs e)
        {
            LoadList();
        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer2;
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
		private System.Windows.Forms.ListView lstTupleGeography;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.CheckBox chkItems;
		private System.Windows.Forms.Label lstCount;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
    }
}

