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
﻿namespace medea.controls
{
	partial class frmOkCancel
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
			this.panButtons = new System.Windows.Forms.Panel();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdOK = new System.Windows.Forms.Button();
			this.panMain = new System.Windows.Forms.Panel();
			this.lblCamposObligatorios = new System.Windows.Forms.Label();
			this.panButtons.SuspendLayout();
			this.SuspendLayout();
			// 
			// panButtons
			// 
			this.panButtons.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.panButtons.Controls.Add(this.lblCamposObligatorios);
			this.panButtons.Controls.Add(this.cmdCancel);
			this.panButtons.Controls.Add(this.cmdOK);
			this.panButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panButtons.Location = new System.Drawing.Point(0, 274);
			this.panButtons.Name = "panButtons";
			this.panButtons.Size = new System.Drawing.Size(425, 55);
			this.panButtons.TabIndex = 1;
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.BackColor = System.Drawing.SystemColors.Control;
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(338, 16);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 1;
			this.cmdCancel.Text = "Cancelar";
			this.cmdCancel.UseVisualStyleBackColor = false;
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.BackColor = System.Drawing.SystemColors.Control;
			this.cmdOK.Location = new System.Drawing.Point(257, 16);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 0;
			this.cmdOK.Text = "Aceptar";
			this.cmdOK.UseVisualStyleBackColor = false;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// panMain
			// 
			this.panMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panMain.Location = new System.Drawing.Point(0, 0);
			this.panMain.Name = "panMain";
			this.panMain.Size = new System.Drawing.Size(425, 274);
			this.panMain.TabIndex = 0;
			// 
			// lblCamposObligatorios
			// 
			this.lblCamposObligatorios.AutoSize = true;
			this.lblCamposObligatorios.Location = new System.Drawing.Point(6, 22);
			this.lblCamposObligatorios.Name = "lblCamposObligatorios";
			this.lblCamposObligatorios.Size = new System.Drawing.Size(108, 13);
			this.lblCamposObligatorios.TabIndex = 2;
			this.lblCamposObligatorios.Text = "* Campos obligatorios";
			// 
			// frmOkCancel
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(425, 329);
			this.Controls.Add(this.panMain);
			this.Controls.Add(this.panButtons);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmOkCancel";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmOkCancel";
			this.panButtons.ResumeLayout(false);
			this.panButtons.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		protected System.Windows.Forms.Panel panButtons;
		private System.Windows.Forms.Button cmdCancel;
		protected System.Windows.Forms.Panel panMain;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Label lblCamposObligatorios;
	}
}

