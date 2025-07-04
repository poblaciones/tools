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
	partial class frmBoundaryVersionEdit
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
			this.cmbParent = new medea.controls.uComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtCaption = new System.Windows.Forms.TextBox();
			this.lblCaption = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btnEditMetadata = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lstRegions = new System.Windows.Forms.CheckedListBox();
			this.lblGeography = new System.Windows.Forms.Label();
			this.lblBoundary = new System.Windows.Forms.Label();
			this.panMain.SuspendLayout();
			this.grpGeneral.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			//
			// panButtons
			//
			this.panButtons.Location = new System.Drawing.Point(0, 378);
			this.panButtons.Size = new System.Drawing.Size(692, 55);
			//
			// panMain
			//
			this.panMain.Controls.Add(this.groupBox1);
			this.panMain.Controls.Add(this.groupBox2);
			this.panMain.Controls.Add(this.grpGeneral);
			this.panMain.Margin = new System.Windows.Forms.Padding(6);
			this.panMain.Size = new System.Drawing.Size(692, 378);
			//
			// grpGeneral
			//
			this.grpGeneral.Controls.Add(this.cmbParent);
			this.grpGeneral.Controls.Add(this.label3);
			this.grpGeneral.Controls.Add(this.lblBoundary);
			this.grpGeneral.Controls.Add(this.label2);
			this.grpGeneral.Controls.Add(this.txtCaption);
			this.grpGeneral.Controls.Add(this.lblCaption);
			this.grpGeneral.Location = new System.Drawing.Point(12, 18);
			this.grpGeneral.Name = "grpGeneral";
			this.grpGeneral.Size = new System.Drawing.Size(295, 216);
			this.grpGeneral.TabIndex = 0;
			this.grpGeneral.TabStop = false;
			this.grpGeneral.Text = "General";
			//
			// cmbParent
			//
			this.cmbParent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbParent.FormattingEnabled = true;
			this.cmbParent.Location = new System.Drawing.Point(109, 109);
			this.cmbParent.Name = "cmbParent";
			this.cmbParent.Optional = true;
			this.cmbParent.Size = new System.Drawing.Size(139, 21);
			this.cmbParent.TabIndex = 11;
			this.cmbParent.SelectedIndexChanged += new System.EventHandler(this.cmbParent_SelectedIndexChanged);
			//
			// label3
			//
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(15, 112);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(58, 13);
			this.label3.TabIndex = 8;
			this.label3.Text = "Geografía:";
			this.label3.Click += new System.EventHandler(this.label3_Click);
			//
			// label2
			//
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(15, 38);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(67, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "Delimitación:";
			this.label2.Click += new System.EventHandler(this.label2_Click);
			//
			// txtCaption
			//
			this.txtCaption.Location = new System.Drawing.Point(109, 67);
			this.txtCaption.MaxLength = 100;
			this.txtCaption.Name = "txtCaption";
			this.txtCaption.Size = new System.Drawing.Size(160, 20);
			this.txtCaption.TabIndex = 1;
			this.txtCaption.TextChanged += new System.EventHandler(this.txtCaption_TextChanged);
			//
			// lblCaption
			//
			this.lblCaption.AutoSize = true;
			this.lblCaption.Location = new System.Drawing.Point(15, 70);
			this.lblCaption.Name = "lblCaption";
			this.lblCaption.Size = new System.Drawing.Size(49, 13);
			this.lblCaption.TabIndex = 0;
			this.lblCaption.Text = "Versión:*";
			this.lblCaption.Click += new System.EventHandler(this.lblCaption_Click);
			//
			// groupBox2
			//
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox2.Controls.Add(this.btnEditMetadata);
			this.groupBox2.Location = new System.Drawing.Point(12, 240);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(295, 117);
			this.groupBox2.TabIndex = 13;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Metadatos";
			//
			// btnEditMetadata
			//
			this.btnEditMetadata.Location = new System.Drawing.Point(109, 32);
			this.btnEditMetadata.Name = "btnEditMetadata";
			this.btnEditMetadata.Size = new System.Drawing.Size(156, 51);
			this.btnEditMetadata.TabIndex = 0;
			this.btnEditMetadata.Text = "Editar Metadatos...";
			this.btnEditMetadata.UseVisualStyleBackColor = true;
			this.btnEditMetadata.Click += new System.EventHandler(this.btnEditMetadata_Click);
			//
			// groupBox1
			//
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox1.Controls.Add(this.lstRegions);
			this.groupBox1.Controls.Add(this.lblGeography);
			this.groupBox1.Location = new System.Drawing.Point(313, 18);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(367, 339);
			this.groupBox1.TabIndex = 13;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Contenido";
			//
			// lstRegions
			//
			this.lstRegions.FormattingEnabled = true;
			this.lstRegions.Location = new System.Drawing.Point(16, 55);
			this.lstRegions.Name = "lstRegions";
			this.lstRegions.Size = new System.Drawing.Size(338, 259);
			this.lstRegions.TabIndex = 7;
			//
			// lblGeography
			//
			this.lblGeography.AutoSize = true;
			this.lblGeography.Location = new System.Drawing.Point(13, 32);
			this.lblGeography.Name = "lblGeography";
			this.lblGeography.Size = new System.Drawing.Size(55, 13);
			this.lblGeography.TabIndex = 6;
			this.lblGeography.Text = "Regiones:";
			//
			// lblBoundary
			//
			this.lblBoundary.AutoSize = true;
			this.lblBoundary.Location = new System.Drawing.Point(106, 38);
			this.lblBoundary.Name = "lblBoundary";
			this.lblBoundary.Size = new System.Drawing.Size(10, 13);
			this.lblBoundary.TabIndex = 8;
			this.lblBoundary.Text = "-";
			this.lblBoundary.Click += new System.EventHandler(this.lblBoundary_Click);
			//
			// frmBoundaryVersionEdit
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(692, 433);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(6);
			this.Name = "frmBoundaryVersionEdit";
			this.ShowInTaskbar = false;
			this.Text = "Delimitación";
			this.panMain.ResumeLayout(false);
			this.grpGeneral.ResumeLayout(false);
			this.grpGeneral.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox grpGeneral;
		private System.Windows.Forms.TextBox txtCaption;
		private System.Windows.Forms.Label lblCaption;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btnEditMetadata;
		private uComboBox cmbParent;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckedListBox lstRegions;
		private System.Windows.Forms.Label lblGeography;
		private System.Windows.Forms.Label lblBoundary;
	}
}

