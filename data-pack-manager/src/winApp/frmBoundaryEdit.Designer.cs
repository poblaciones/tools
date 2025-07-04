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
	partial class frmBoundaryEdit
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
			this.chVisible = new System.Windows.Forms.CheckBox();
			this.cmbGroup = new medea.controls.uComboBox();
			this.txtOrden = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtCaption = new System.Windows.Forms.TextBox();
			this.lblCaption = new System.Windows.Forms.Label();
			this.panMain.SuspendLayout();
			this.grpGeneral.SuspendLayout();
			this.SuspendLayout();
			// 
			// panButtons
			// 
			this.panButtons.Location = new System.Drawing.Point(0, 258);
			this.panButtons.Size = new System.Drawing.Size(480, 55);
			// 
			// panMain
			// 
			this.panMain.Controls.Add(this.grpGeneral);
			this.panMain.Margin = new System.Windows.Forms.Padding(6);
			this.panMain.Size = new System.Drawing.Size(480, 258);
			// 
			// grpGeneral
			// 
			this.grpGeneral.Controls.Add(this.chVisible);
			this.grpGeneral.Controls.Add(this.cmbGroup);
			this.grpGeneral.Controls.Add(this.txtOrden);
			this.grpGeneral.Controls.Add(this.label1);
			this.grpGeneral.Controls.Add(this.label2);
			this.grpGeneral.Controls.Add(this.txtCaption);
			this.grpGeneral.Controls.Add(this.lblCaption);
			this.grpGeneral.Location = new System.Drawing.Point(12, 18);
			this.grpGeneral.Name = "grpGeneral";
			this.grpGeneral.Size = new System.Drawing.Size(457, 216);
			this.grpGeneral.TabIndex = 0;
			this.grpGeneral.TabStop = false;
			this.grpGeneral.Text = "General";
			// 
			// chVisible
			// 
			this.chVisible.AutoSize = true;
			this.chVisible.Location = new System.Drawing.Point(18, 180);
			this.chVisible.Name = "chVisible";
			this.chVisible.Size = new System.Drawing.Size(56, 17);
			this.chVisible.TabIndex = 12;
			this.chVisible.Text = "Visible";
			this.chVisible.UseVisualStyleBackColor = true;
			// 
			// cmbGroup
			// 
			this.cmbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbGroup.FormattingEnabled = true;
			this.cmbGroup.Location = new System.Drawing.Point(109, 104);
			this.cmbGroup.Name = "cmbGroup";
			this.cmbGroup.Optional = false;
			this.cmbGroup.Size = new System.Drawing.Size(315, 21);
			this.cmbGroup.TabIndex = 10;
			// 
			// txtOrden
			// 
			this.txtOrden.Location = new System.Drawing.Point(109, 66);
			this.txtOrden.MaxLength = 100;
			this.txtOrden.Name = "txtOrden";
			this.txtOrden.Size = new System.Drawing.Size(56, 20);
			this.txtOrden.TabIndex = 9;
			this.txtOrden.Text = "0";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 107);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(39, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "Grupo:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(15, 69);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(39, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "Orden:";
			// 
			// txtCaption
			// 
			this.txtCaption.Location = new System.Drawing.Point(109, 29);
			this.txtCaption.MaxLength = 100;
			this.txtCaption.Name = "txtCaption";
			this.txtCaption.Size = new System.Drawing.Size(315, 20);
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
			// frmBoundaryEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(480, 313);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(6);
			this.Name = "frmBoundaryEdit";
			this.ShowInTaskbar = false;
			this.Text = "Delimitación";
			this.panMain.ResumeLayout(false);
			this.grpGeneral.ResumeLayout(false);
			this.grpGeneral.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox grpGeneral;
		private System.Windows.Forms.TextBox txtCaption;
		private System.Windows.Forms.Label lblCaption;
		private System.Windows.Forms.TextBox txtOrden;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private uComboBox cmbGroup;
		private System.Windows.Forms.CheckBox chVisible;
	}
}

