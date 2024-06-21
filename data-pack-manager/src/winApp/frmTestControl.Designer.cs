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
	partial class frmTestControl
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
			this.uSvgCombo1 = new medea.controls.uSvgCombo();
			this.uGeometry1 = new medea.controls.uGeometry();
			this.SuspendLayout();
			//
			// uSvgCombo1
			//
			this.uSvgCombo1.FormattingEnabled = true;
			this.uSvgCombo1.Location = new System.Drawing.Point(60, 201);
			this.uSvgCombo1.Name = "uSvgCombo1";
			this.uSvgCombo1.Size = new System.Drawing.Size(166, 21);
			this.uSvgCombo1.TabIndex = 1;
			//
			// uGeometry1
			//
			this.uGeometry1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
			this.uGeometry1.BackColor = System.Drawing.Color.WhiteSmoke;
			this.uGeometry1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.uGeometry1.Location = new System.Drawing.Point(60, 54);
			this.uGeometry1.Name = "uGeometry1";
			this.uGeometry1.Size = new System.Drawing.Size(167, 141);
			this.uGeometry1.TabIndex = 0;
			//
			// frmTestControl
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.uSvgCombo1);
			this.Controls.Add(this.uGeometry1);
			this.Name = "frmTestControl";
			this.ShowInTaskbar = false;
			this.Text = "frmTestControl";
			this.Load += new System.EventHandler(this.frmTestControl_Load);
			this.Click += new System.EventHandler(this.frmTestControl_Click);
			this.ResumeLayout(false);

		}

		#endregion

		private controls.uGeometry uGeometry1;
		private controls.uSvgCombo uSvgCombo1;
	}
}