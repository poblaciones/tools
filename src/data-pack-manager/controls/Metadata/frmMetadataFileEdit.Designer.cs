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
	partial class frmMetadataFileEdit
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
			this.txtUrl = new System.Windows.Forms.TextBox();
			this.uFile = new medea.controls.uFile();
			this.lblParent = new System.Windows.Forms.Label();
			this.txtCaption = new System.Windows.Forms.TextBox();
			this.lblCaption = new System.Windows.Forms.Label();
			this.panMain.SuspendLayout();
			this.grpGeneral.SuspendLayout();
			this.SuspendLayout();
			//
			// panButtons
			//
			this.panButtons.Location = new System.Drawing.Point(0, 190);
			this.panButtons.Size = new System.Drawing.Size(370, 55);
			//
			// panMain
			//
			this.panMain.Controls.Add(this.grpGeneral);
			this.panMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.panMain.Size = new System.Drawing.Size(370, 190);
			//
			// grpGeneral
			//
			this.grpGeneral.Controls.Add(this.txtUrl);
			this.grpGeneral.Controls.Add(this.uFile);
			this.grpGeneral.Controls.Add(this.lblParent);
			this.grpGeneral.Controls.Add(this.txtCaption);
			this.grpGeneral.Controls.Add(this.lblCaption);
			this.grpGeneral.Location = new System.Drawing.Point(12, 18);
			this.grpGeneral.Name = "grpGeneral";
			this.grpGeneral.Size = new System.Drawing.Size(346, 158);
			this.grpGeneral.TabIndex = 0;
			this.grpGeneral.TabStop = false;
			this.grpGeneral.Text = "General";
			//
			// txtUrl
			//
			this.txtUrl.Location = new System.Drawing.Point(108, 62);
			this.txtUrl.MaxLength = 255;
			this.txtUrl.Name = "txtUrl";
			this.txtUrl.Size = new System.Drawing.Size(220, 20);
			this.txtUrl.TabIndex = 4;
			//
			// uFile
			//
			this.uFile.BackColor = System.Drawing.SystemColors.Control;
			this.uFile.EnabledButtons = true;
			this.uFile.FileAdded = false;
			this.uFile.FileDeleted = false;
			this.uFile.Location = new System.Drawing.Point(8, 88);
			this.uFile.Name = "uFile";
			this.uFile.SelectedFile = null;
			this.uFile.Size = new System.Drawing.Size(274, 55);
			this.uFile.TabIndex = 3;
			//
			// lblParent
			//
			this.lblParent.AutoSize = true;
			this.lblParent.Location = new System.Drawing.Point(11, 67);
			this.lblParent.Name = "lblParent";
			this.lblParent.Size = new System.Drawing.Size(96, 13);
			this.lblParent.TabIndex = 2;
			this.lblParent.Text = "Ubicaci�n externa:";
			//
			// txtCaption
			//
			this.txtCaption.Location = new System.Drawing.Point(108, 35);
			this.txtCaption.MaxLength = 100;
			this.txtCaption.Name = "txtCaption";
			this.txtCaption.Size = new System.Drawing.Size(179, 20);
			this.txtCaption.TabIndex = 1;
			//
			// lblCaption
			//
			this.lblCaption.AutoSize = true;
			this.lblCaption.Location = new System.Drawing.Point(11, 38);
			this.lblCaption.Name = "lblCaption";
			this.lblCaption.Size = new System.Drawing.Size(51, 13);
			this.lblCaption.TabIndex = 0;
			this.lblCaption.Text = "Nombre:*";
			//
			// frmMetadataFileEdit
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(370, 245);
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "frmMetadataFileEdit";
			this.ShowInTaskbar = false;
			this.Text = "Adjunto";
			this.panMain.ResumeLayout(false);
			this.grpGeneral.ResumeLayout(false);
			this.grpGeneral.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox grpGeneral;
		private System.Windows.Forms.TextBox txtCaption;
		private System.Windows.Forms.Label lblCaption;
		private System.Windows.Forms.Label lblParent;
		private uFile uFile;
		private System.Windows.Forms.TextBox txtUrl;
	}
}

