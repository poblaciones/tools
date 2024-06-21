/*
*    Poblaciones - Plataforma abierta de datos espaciales de poblaciÃ³n.
*    Copyright (C) 2018-2019. Consejo Nacional de Investigaciones CientÃ­ficas y TÃ©cnicas (CONICET)
*		 y Universidad CatÃ³lica Argentina (UCA).
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
	partial class frmEditSource
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
			this.panDetails = new System.Windows.Forms.GroupBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.txtVersion = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.txtWeb = new System.Windows.Forms.TextBox();
			this.uInstitution = new medea.controls.uInstitution();
			this.uContact = new medea.controls.uContact();
			this.lblAuthor = new System.Windows.Forms.Label();
			this.txtAuthors = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.txtWiki = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.txtCaption2 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.panMain.SuspendLayout();
			this.panDetails.SuspendLayout();
			this.SuspendLayout();
			//
			// panButtons
			//
			this.panButtons.Location = new System.Drawing.Point(0, 434);
			this.panButtons.Size = new System.Drawing.Size(698, 55);
			//
			// panMain
			//
			this.panMain.Controls.Add(this.panDetails);
			this.panMain.Size = new System.Drawing.Size(698, 434);
			//
			// panDetails
			//
			this.panDetails.Controls.Add(this.label10);
			this.panDetails.Controls.Add(this.label5);
			this.panDetails.Controls.Add(this.label9);
			this.panDetails.Controls.Add(this.txtVersion);
			this.panDetails.Controls.Add(this.label4);
			this.panDetails.Controls.Add(this.label8);
			this.panDetails.Controls.Add(this.txtWeb);
			this.panDetails.Controls.Add(this.uInstitution);
			this.panDetails.Controls.Add(this.uContact);
			this.panDetails.Controls.Add(this.lblAuthor);
			this.panDetails.Controls.Add(this.txtAuthors);
			this.panDetails.Controls.Add(this.label6);
			this.panDetails.Controls.Add(this.txtWiki);
			this.panDetails.Controls.Add(this.label7);
			this.panDetails.Controls.Add(this.txtCaption2);
			this.panDetails.Controls.Add(this.label2);
			this.panDetails.Location = new System.Drawing.Point(8, 8);
			this.panDetails.Name = "panDetails";
			this.panDetails.Size = new System.Drawing.Size(680, 417);
			this.panDetails.TabIndex = 4;
			this.panDetails.TabStop = false;
			this.panDetails.Text = "Detalle";
			//
			// label10
			//
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(357, 79);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(162, 13);
			this.label10.TabIndex = 32;
			this.label10.Text = "Ej. Ana Giliberti y Pedro Echevez";
			//
			// label5
			//
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(192, 51);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(46, 13);
			this.label5.TabIndex = 31;
			this.label5.Text = "Ej. 2010";
			//
			// label9
			//
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(16, 51);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(49, 13);
			this.label9.TabIndex = 4;
			this.label9.Text = "Edición*:";
			//
			// txtVersion
			//
			this.txtVersion.Location = new System.Drawing.Point(96, 47);
			this.txtVersion.MaxLength = 10;
			this.txtVersion.Name = "txtVersion";
			this.txtVersion.Size = new System.Drawing.Size(91, 20);
			this.txtVersion.TabIndex = 5;
			//
			// label4
			//
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(359, 106);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(442, 13);
			this.label4.TabIndex = 28;
			this.label4.Text = "Ej. https://www.indec.gov.ar/nivel4_default.asp?id_tema_1=2&id_tema_2=41&id_tema_" +
    "3=135";
			//
			// label8
			//
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(16, 103);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(33, 13);
			this.label8.TabIndex = 8;
			this.label8.Text = "Web:";
			//
			// txtWeb
			//
			this.txtWeb.Location = new System.Drawing.Point(96, 99);
			this.txtWeb.MaxLength = 250;
			this.txtWeb.Name = "txtWeb";
			this.txtWeb.Size = new System.Drawing.Size(257, 20);
			this.txtWeb.TabIndex = 9;
			//
			// uInstitution
			//
			this.uInstitution.Location = new System.Drawing.Point(6, 169);
			this.uInstitution.Name = "uInstitution";
			this.uInstitution.Size = new System.Drawing.Size(331, 222);
			this.uInstitution.TabIndex = 12;
			//
			// uContact
			//
			this.uContact.Location = new System.Drawing.Point(343, 169);
			this.uContact.Name = "uContact";
			this.uContact.Size = new System.Drawing.Size(325, 189);
			this.uContact.TabIndex = 13;
			//
			// lblAuthor
			//
			this.lblAuthor.AutoSize = true;
			this.lblAuthor.Location = new System.Drawing.Point(16, 76);
			this.lblAuthor.Name = "lblAuthor";
			this.lblAuthor.Size = new System.Drawing.Size(46, 13);
			this.lblAuthor.TabIndex = 6;
			this.lblAuthor.Text = "Autores:";
			//
			// txtAuthors
			//
			this.txtAuthors.Location = new System.Drawing.Point(96, 73);
			this.txtAuthors.MaxLength = 255;
			this.txtAuthors.Name = "txtAuthors";
			this.txtAuthors.Size = new System.Drawing.Size(257, 20);
			this.txtAuthors.TabIndex = 7;
			//
			// label6
			//
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(16, 129);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(31, 13);
			this.label6.TabIndex = 10;
			this.label6.Text = "Wiki:";
			//
			// txtWiki
			//
			this.txtWiki.Location = new System.Drawing.Point(96, 125);
			this.txtWiki.MaxLength = 250;
			this.txtWiki.Name = "txtWiki";
			this.txtWiki.Size = new System.Drawing.Size(257, 20);
			this.txtWiki.TabIndex = 11;
			//
			// label7
			//
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(359, 25);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(298, 13);
			this.label7.TabIndex = 18;
			this.label7.Text = "Ej. Censo Nacional de Población, Hogares y Viviendas (2010)";
			//
			// txtCaption2
			//
			this.txtCaption2.Location = new System.Drawing.Point(96, 20);
			this.txtCaption2.MaxLength = 200;
			this.txtCaption2.Name = "txtCaption2";
			this.txtCaption2.Size = new System.Drawing.Size(257, 20);
			this.txtCaption2.TabIndex = 1;
			//
			// label2
			//
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(16, 25);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(70, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Descripción*:";
			//
			// frmEditSource
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(698, 489);
			this.Name = "frmEditSource";
			this.Text = "Fuente";
			this.panMain.ResumeLayout(false);
			this.panDetails.ResumeLayout(false);
			this.panDetails.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox panDetails;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox txtVersion;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox txtWeb;
		private uInstitution uInstitution;
		private uContact uContact;
		private System.Windows.Forms.Label lblAuthor;
		private System.Windows.Forms.TextBox txtAuthors;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtWiki;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtCaption2;
	}
}

