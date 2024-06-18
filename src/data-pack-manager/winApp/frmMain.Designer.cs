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
namespace medea.winApp
{
	partial class frmMain
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnGradient = new System.Windows.Forms.Button();
			this.btnSimplify = new System.Windows.Forms.Button();
			this.menClippingGeography = new System.Windows.Forms.Button();
            this.menTupleGeography = new System.Windows.Forms.Button();
            this.menClipping = new System.Windows.Forms.Button();
			this.menGeography = new System.Windows.Forms.Button();
			this.menCheckNH = new System.Windows.Forms.Button();
			this.btnNewMain = new System.Windows.Forms.Button();
			this.panMain = new System.Windows.Forms.Panel();
			this.panGlobalSettings = new System.Windows.Forms.Panel();
			this.btnCountryEdit = new System.Windows.Forms.Button();
			this.cmbCountry = new medea.controls.uComboBox();
			this.lblUser = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnBoundary = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.panGlobalSettings.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.Silver;
			this.panel1.Controls.Add(this.btnGradient);
			this.panel1.Controls.Add(this.btnSimplify);
			this.panel1.Controls.Add(this.menCheckNH);
			this.panel1.Controls.Add(this.btnNewMain);
            this.panel1.Controls.Add(this.menTupleGeography);
            this.panel1.Controls.Add(this.btnBoundary);
			this.panel1.Controls.Add(this.menClippingGeography);
			this.panel1.Controls.Add(this.menClipping);
            this.panel1.Controls.Add(this.menGeography);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(5, 5);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(6);
			this.panel1.Size = new System.Drawing.Size(814, 40);
			this.panel1.TabIndex = 0;
			// 
			// btnGradient
			// 
			this.btnGradient.BackColor = System.Drawing.Color.Gainsboro;
			this.btnGradient.Dock = System.Windows.Forms.DockStyle.Left;
			this.btnGradient.Location = new System.Drawing.Point(413, 6);
			this.btnGradient.Name = "btnGradient";
			this.btnGradient.Size = new System.Drawing.Size(89, 28);
			this.btnGradient.TabIndex = 14;
			this.btnGradient.Text = "Gradientes";
			this.btnGradient.UseVisualStyleBackColor = false;
			this.btnGradient.Click += new System.EventHandler(this.btnGradient_Click);
			// 
			// btnSimplify
			// 
			this.btnSimplify.BackColor = System.Drawing.Color.Gainsboro;
			this.btnSimplify.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnSimplify.Location = new System.Drawing.Point(595, 6);
			this.btnSimplify.Name = "btnSimplify";
			this.btnSimplify.Size = new System.Drawing.Size(96, 28);
			this.btnSimplify.TabIndex = 13;
			this.btnSimplify.Text = "Resimplificar";
			this.btnSimplify.UseVisualStyleBackColor = false;
			this.btnSimplify.Click += new System.EventHandler(this.btnSimplify_Click);
			// 
			// menClippingGeography
			// 
			this.menClippingGeography.BackColor = System.Drawing.Color.Gainsboro;
			this.menClippingGeography.Dock = System.Windows.Forms.DockStyle.Left;
			this.menClippingGeography.Location = new System.Drawing.Point(185, 6);
			this.menClippingGeography.Name = "menClippingGeography";
			this.menClippingGeography.Size = new System.Drawing.Size(139, 28);
			this.menClippingGeography.TabIndex = 2;
			this.menClippingGeography.Text = "Regiones x Geografía";
			this.menClippingGeography.UseVisualStyleBackColor = false;
			this.menClippingGeography.Click += new System.EventHandler(this.menClippingGeography_Click);
            // 
            // menTupleGeography
            // 
            this.menTupleGeography.BackColor = System.Drawing.Color.Gainsboro;
            this.menTupleGeography.Dock = System.Windows.Forms.DockStyle.Left;
            this.menTupleGeography.Location = new System.Drawing.Point(185, 6);
            this.menTupleGeography.Name = "menTupleGeography";
            this.menTupleGeography.Size = new System.Drawing.Size(139, 28);
            this.menTupleGeography.TabIndex = 2;
            this.menTupleGeography.Text = "Tuplas de Geografía";
            this.menTupleGeography.UseVisualStyleBackColor = false;
            this.menTupleGeography.Click += new System.EventHandler(this.menTupleGeography_Click);
            // 
            // menClipping
            // 
            this.menClipping.BackColor = System.Drawing.Color.Gainsboro;
			this.menClipping.Dock = System.Windows.Forms.DockStyle.Left;
			this.menClipping.Location = new System.Drawing.Point(96, 6);
			this.menClipping.Name = "menClipping";
			this.menClipping.Size = new System.Drawing.Size(89, 28);
			this.menClipping.TabIndex = 1;
			this.menClipping.Text = "Regiones";
			this.menClipping.UseVisualStyleBackColor = false;
			this.menClipping.Click += new System.EventHandler(this.menClipping_Click);
			// 
			// menGeography
			// 
			this.menGeography.BackColor = System.Drawing.Color.Gainsboro;
			this.menGeography.Dock = System.Windows.Forms.DockStyle.Left;
			this.menGeography.Location = new System.Drawing.Point(6, 6);
			this.menGeography.Name = "menGeography";
			this.menGeography.Size = new System.Drawing.Size(90, 28);
			this.menGeography.TabIndex = 0;
			this.menGeography.Text = "Geografías";
			this.menGeography.UseVisualStyleBackColor = false;
			this.menGeography.Click += new System.EventHandler(this.menGeography_Click);
			// 
			// menCheckNH
			// 
			this.menCheckNH.BackColor = System.Drawing.Color.Gainsboro;
			this.menCheckNH.Dock = System.Windows.Forms.DockStyle.Right;
			this.menCheckNH.Location = new System.Drawing.Point(691, 6);
			this.menCheckNH.Name = "menCheckNH";
			this.menCheckNH.Size = new System.Drawing.Size(96, 28);
			this.menCheckNH.TabIndex = 8;
			this.menCheckNH.Text = "Chequear HBMs";
			this.menCheckNH.UseVisualStyleBackColor = false;
			this.menCheckNH.Click += new System.EventHandler(this.menCheckNH_Click);
			// 
			// btnNewMain
			// 
			this.btnNewMain.BackColor = System.Drawing.Color.Gainsboro;
			this.btnNewMain.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnNewMain.Location = new System.Drawing.Point(787, 6);
			this.btnNewMain.Name = "btnNewMain";
			this.btnNewMain.Size = new System.Drawing.Size(21, 28);
			this.btnNewMain.TabIndex = 11;
			this.btnNewMain.Text = "..";
			this.btnNewMain.UseVisualStyleBackColor = false;
			this.btnNewMain.Click += new System.EventHandler(this.btnNewMain_Click);
			// 
			// panMain
			// 
			this.panMain.BackColor = System.Drawing.Color.WhiteSmoke;
			this.panMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panMain.Location = new System.Drawing.Point(5, 71);
			this.panMain.Name = "panMain";
			this.panMain.Size = new System.Drawing.Size(814, 197);
			this.panMain.TabIndex = 1;
			// 
			// panGlobalSettings
			// 
			this.panGlobalSettings.Controls.Add(this.btnCountryEdit);
			this.panGlobalSettings.Controls.Add(this.cmbCountry);
			this.panGlobalSettings.Controls.Add(this.lblUser);
			this.panGlobalSettings.Controls.Add(this.label2);
			this.panGlobalSettings.Controls.Add(this.label1);
			this.panGlobalSettings.Dock = System.Windows.Forms.DockStyle.Top;
			this.panGlobalSettings.Location = new System.Drawing.Point(5, 45);
			this.panGlobalSettings.Name = "panGlobalSettings";
			this.panGlobalSettings.Size = new System.Drawing.Size(814, 26);
			this.panGlobalSettings.TabIndex = 12;
			// 
			// btnCountryEdit
			// 
			this.btnCountryEdit.Location = new System.Drawing.Point(226, 3);
			this.btnCountryEdit.Name = "btnCountryEdit";
			this.btnCountryEdit.Size = new System.Drawing.Size(25, 20);
			this.btnCountryEdit.TabIndex = 7;
			this.btnCountryEdit.Text = "...";
			this.btnCountryEdit.UseVisualStyleBackColor = true;
			this.btnCountryEdit.Click += new System.EventHandler(this.btnCountryEdit_Click);
			// 
			// cmbCountry
			// 
			this.cmbCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbCountry.FormattingEnabled = true;
			this.cmbCountry.Location = new System.Drawing.Point(52, 3);
			this.cmbCountry.Name = "cmbCountry";
			this.cmbCountry.Optional = false;
			this.cmbCountry.Size = new System.Drawing.Size(168, 21);
			this.cmbCountry.TabIndex = 5;
			this.cmbCountry.SelectedIndexChanged += new System.EventHandler(this.cmbCountry_SelectedIndexChanged);
			// 
			// lblUser
			// 
			this.lblUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblUser.AutoSize = true;
			this.lblUser.Location = new System.Drawing.Point(645, 5);
			this.lblUser.Name = "lblUser";
			this.lblUser.Size = new System.Drawing.Size(13, 13);
			this.lblUser.TabIndex = 1;
			this.lblUser.Text = "--";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(596, 5);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(46, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Usuario:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "País:";
			// 
			// btnBoundary
			// 
			this.btnBoundary.BackColor = System.Drawing.Color.Gainsboro;
			this.btnBoundary.Dock = System.Windows.Forms.DockStyle.Left;
			this.btnBoundary.Location = new System.Drawing.Point(324, 6);
			this.btnBoundary.Name = "btnBoundary";
			this.btnBoundary.Size = new System.Drawing.Size(89, 28);
			this.btnBoundary.TabIndex = 15;
			this.btnBoundary.Text = "Delimitaciones";
			this.btnBoundary.UseVisualStyleBackColor = false;
			this.btnBoundary.Click += new System.EventHandler(this.btnBoundary_Click);
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Gainsboro;
			this.ClientSize = new System.Drawing.Size(824, 273);
			this.Controls.Add(this.panMain);
			this.Controls.Add(this.panGlobalSettings);
			this.Controls.Add(this.panel1);
			this.Name = "frmMain";
			this.Padding = new System.Windows.Forms.Padding(5);
			this.Text = "Administración";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.panel1.ResumeLayout(false);
			this.panGlobalSettings.ResumeLayout(false);
			this.panGlobalSettings.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button menClippingGeography;
        private System.Windows.Forms.Button menTupleGeography;
        private System.Windows.Forms.Button menClipping;
		private System.Windows.Forms.Button menGeography;
		private System.Windows.Forms.Panel panMain;
		private System.Windows.Forms.Button menCheckNH;
		private System.Windows.Forms.Panel panGlobalSettings;
		private System.Windows.Forms.Label label1;
		private controls.uComboBox cmbCountry;
		private System.Windows.Forms.Button btnCountryEdit;
		private System.Windows.Forms.Button btnNewMain;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblUser;
		private System.Windows.Forms.Button btnSimplify;
		private System.Windows.Forms.Button btnGradient;
		private System.Windows.Forms.Button btnBoundary;
	}
}