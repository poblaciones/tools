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
	partial class frmGradientEdit
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
			this.grpGeneral = new System.Windows.Forms.GroupBox();
			this.radPng = new System.Windows.Forms.RadioButton();
			this.radJpg = new System.Windows.Forms.RadioButton();
			this.txtMaxZoomLevel = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtCaption = new System.Windows.Forms.TextBox();
			this.lblCaption = new System.Windows.Forms.Label();
			this.grpItems = new System.Windows.Forms.GroupBox();
			this.cmbTable = new System.Windows.Forms.ComboBox();
			this.uFile = new medea.controls.uFile();
			this.label1 = new System.Windows.Forms.Label();
			this.panMain.SuspendLayout();
			this.grpGeneral.SuspendLayout();
			this.grpItems.SuspendLayout();
			this.SuspendLayout();
			//
			// panButtons
			//
			this.panButtons.Location = new System.Drawing.Point(0, 321);
			this.panButtons.Size = new System.Drawing.Size(423, 55);
			//
			// panMain
			//
			this.panMain.Controls.Add(this.grpGeneral);
			this.panMain.Controls.Add(this.grpItems);
			this.panMain.Margin = new System.Windows.Forms.Padding(6);
			this.panMain.Size = new System.Drawing.Size(423, 321);
			//
			// grpGeneral
			//
			this.grpGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpGeneral.Controls.Add(this.radPng);
			this.grpGeneral.Controls.Add(this.radJpg);
			this.grpGeneral.Controls.Add(this.txtMaxZoomLevel);
			this.grpGeneral.Controls.Add(this.label3);
			this.grpGeneral.Controls.Add(this.label2);
			this.grpGeneral.Controls.Add(this.txtCaption);
			this.grpGeneral.Controls.Add(this.lblCaption);
			this.grpGeneral.Location = new System.Drawing.Point(12, 18);
			this.grpGeneral.Name = "grpGeneral";
			this.grpGeneral.Size = new System.Drawing.Size(399, 136);
			this.grpGeneral.TabIndex = 0;
			this.grpGeneral.TabStop = false;
			this.grpGeneral.Text = "General";
			//
			// radPng
			//
			this.radPng.AutoSize = true;
			this.radPng.Location = new System.Drawing.Point(153, 100);
			this.radPng.Name = "radPng";
			this.radPng.Size = new System.Drawing.Size(43, 17);
			this.radPng.TabIndex = 11;
			this.radPng.Text = "png";
			this.radPng.UseVisualStyleBackColor = true;
			//
			// radJpg
			//
			this.radJpg.AutoSize = true;
			this.radJpg.Checked = true;
			this.radJpg.Location = new System.Drawing.Point(62, 100);
			this.radJpg.Name = "radJpg";
			this.radJpg.Size = new System.Drawing.Size(45, 17);
			this.radJpg.TabIndex = 10;
			this.radJpg.TabStop = true;
			this.radJpg.Text = "jpeg";
			this.radJpg.UseVisualStyleBackColor = true;
			//
			// txtMaxZoomLevel
			//
			this.txtMaxZoomLevel.Location = new System.Drawing.Point(140, 66);
			this.txtMaxZoomLevel.MaxLength = 100;
			this.txtMaxZoomLevel.Name = "txtMaxZoomLevel";
			this.txtMaxZoomLevel.Size = new System.Drawing.Size(56, 20);
			this.txtMaxZoomLevel.TabIndex = 9;
			this.txtMaxZoomLevel.Text = "0";
			//
			// label3
			//
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(15, 102);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(31, 13);
			this.label3.TabIndex = 8;
			this.label3.Text = "Tipo:";
			//
			// label2
			//
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(15, 69);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(114, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "Máximo nivel de zoom:";
			//
			// txtCaption
			//
			this.txtCaption.Location = new System.Drawing.Point(73, 29);
			this.txtCaption.MaxLength = 100;
			this.txtCaption.Name = "txtCaption";
			this.txtCaption.Size = new System.Drawing.Size(245, 20);
			this.txtCaption.TabIndex = 1;
			//
			// lblCaption
			//
			this.lblCaption.AutoSize = true;
			this.lblCaption.Location = new System.Drawing.Point(15, 32);
			this.lblCaption.Name = "lblCaption";
			this.lblCaption.Size = new System.Drawing.Size(51, 13);
			this.lblCaption.TabIndex = 0;
			this.lblCaption.Text = "Nombre:*";
			//
			// grpItems
			//
			this.grpItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpItems.Controls.Add(this.cmbTable);
			this.grpItems.Controls.Add(this.uFile);
			this.grpItems.Controls.Add(this.label1);
			this.grpItems.Location = new System.Drawing.Point(12, 160);
			this.grpItems.Name = "grpItems";
			this.grpItems.Size = new System.Drawing.Size(399, 144);
			this.grpItems.TabIndex = 2;
			this.grpItems.TabStop = false;
			this.grpItems.Text = "Items";
			//
			// cmbTable
			//
			this.cmbTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTable.FormattingEnabled = true;
			this.cmbTable.Location = new System.Drawing.Point(73, 99);
			this.cmbTable.Name = "cmbTable";
			this.cmbTable.Size = new System.Drawing.Size(139, 21);
			this.cmbTable.TabIndex = 9;
			//
			// uFile
			//
			this.uFile.BackColor = System.Drawing.SystemColors.Control;
			this.uFile.EnabledButtons = true;
			this.uFile.FileAdded = false;
			this.uFile.FileDeleted = false;
			this.uFile.Location = new System.Drawing.Point(18, 19);
			this.uFile.Margin = new System.Windows.Forms.Padding(6);
			this.uFile.Name = "uFile";
			this.uFile.SelectedFile = null;
			this.uFile.Size = new System.Drawing.Size(215, 83);
			this.uFile.TabIndex = 0;
			this.uFile.Load += new System.EventHandler(this.uFile_Load);
			//
			// label1
			//
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(18, 102);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(37, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "Tabla:";
			//
			// frmGradientEdit
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(423, 376);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(6);
			this.Name = "frmGradientEdit";
			this.ShowInTaskbar = false;
			this.Text = "Gradiente";
			this.panMain.ResumeLayout(false);
			this.grpGeneral.ResumeLayout(false);
			this.grpGeneral.PerformLayout();
			this.grpItems.ResumeLayout(false);
			this.grpItems.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox grpGeneral;
		private System.Windows.Forms.TextBox txtCaption;
		private System.Windows.Forms.Label lblCaption;
		private System.Windows.Forms.GroupBox grpItems;
		private uFile uFile;
		private System.Windows.Forms.TextBox txtMaxZoomLevel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cmbTable;
		private System.Windows.Forms.RadioButton radPng;
		private System.Windows.Forms.RadioButton radJpg;
		private System.Windows.Forms.Label label3;
	}
}

