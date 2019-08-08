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
	partial class frmPickCoverage
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
            this.lblCaption = new System.Windows.Forms.Label();
            this.lblFile = new System.Windows.Forms.Label();
            this.tvRegions = new System.Windows.Forms.TreeView();
            this.panMain.SuspendLayout();
            this.SuspendLayout();
            //
            // panButtons
            //
            this.panButtons.Location = new System.Drawing.Point(0, 290);
            this.panButtons.Size = new System.Drawing.Size(461, 55);
            //
            // panMain
            //
            this.panMain.Controls.Add(this.tvRegions);
            this.panMain.Controls.Add(this.lblFile);
            this.panMain.Controls.Add(this.lblCaption);
            this.panMain.Size = new System.Drawing.Size(461, 290);
            //
            // lblCaption
            //
            this.lblCaption.Location = new System.Drawing.Point(9, 20);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(334, 19);
            this.lblCaption.TabIndex = 0;
            this.lblCaption.Text = "Seleccione el área de cobertura del dataset:";
            //
            // lblFile
            //
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(9, 47);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(60, 13);
            this.lblFile.TabIndex = 1;
            this.lblFile.Text = "Cobertura*:";
            //
            // tvRegions
            //
            this.tvRegions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvRegions.HideSelection = false;
            this.tvRegions.Location = new System.Drawing.Point(12, 69);
            this.tvRegions.Name = "tvRegions";
            this.tvRegions.Size = new System.Drawing.Size(436, 208);
            this.tvRegions.TabIndex = 2;
            this.tvRegions.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvRegions_BeforeExpand);
            //
            // frmPickCoverage
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 345);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmPickCoverage";
            this.ShowInTaskbar = false;
            this.Text = "Cobertura";
            this.panMain.ResumeLayout(false);
            this.panMain.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lblCaption;
		private System.Windows.Forms.Label lblFile;
		private System.Windows.Forms.TreeView tvRegions;
	}
}

