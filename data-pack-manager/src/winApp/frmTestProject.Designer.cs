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
﻿namespace medea.winApp
{
	partial class frmTestProject
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
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.listBox2 = new System.Windows.Forms.ListBox();
			this.uGeometry1 = new medea.controls.uGeometry();
			this.uGeometry2 = new medea.controls.uGeometry();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listBox1
			// 
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new System.Drawing.Point(19, 39);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(177, 43);
			this.listBox1.TabIndex = 0;
			// 
			// listBox2
			// 
			this.listBox2.FormattingEnabled = true;
			this.listBox2.Location = new System.Drawing.Point(444, 39);
			this.listBox2.Name = "listBox2";
			this.listBox2.Size = new System.Drawing.Size(177, 43);
			this.listBox2.TabIndex = 0;
			// 
			// uGeometry1
			// 
			this.uGeometry1.BackColor = System.Drawing.Color.WhiteSmoke;
			this.uGeometry1.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.uGeometry1.Location = new System.Drawing.Point(12, 91);
			this.uGeometry1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.uGeometry1.Name = "uGeometry1";
			this.uGeometry1.Size = new System.Drawing.Size(400, 400);
			this.uGeometry1.TabIndex = 1;
			// 
			// uGeometry2
			// 
			this.uGeometry2.BackColor = System.Drawing.Color.WhiteSmoke;
			this.uGeometry2.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.uGeometry2.Location = new System.Drawing.Point(432, 99);
			this.uGeometry2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.uGeometry2.Name = "uGeometry2";
			this.uGeometry2.Size = new System.Drawing.Size(400, 400);
			this.uGeometry2.TabIndex = 1;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(42, 11);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(122, 17);
			this.button1.TabIndex = 2;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(170, 11);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(122, 17);
			this.button2.TabIndex = 2;
			this.button2.Text = "button1";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(298, 16);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(122, 17);
			this.button3.TabIndex = 2;
			this.button3.Text = "button1";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(327, 49);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(85, 24);
			this.button4.TabIndex = 3;
			this.button4.Text = "button4";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// frmTestProject
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(916, 585);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.uGeometry2);
			this.Controls.Add(this.uGeometry1);
			this.Controls.Add(this.listBox2);
			this.Controls.Add(this.listBox1);
			this.Name = "frmTestProject";
			this.Text = "frmTestProject";
			this.Load += new System.EventHandler(this.frmTestProject_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.ListBox listBox2;
		private controls.uGeometry uGeometry1;
		private controls.uGeometry uGeometry2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
	}
}