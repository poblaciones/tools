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
	partial class uMetadata
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

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblAuthor = new System.Windows.Forms.Label();
			this.txtAuthor = new System.Windows.Forms.TextBox();
			this.radMapping = new System.Windows.Forms.RadioButton();
			this.radResearch = new System.Windows.Forms.RadioButton();
			this.radPublic = new System.Windows.Forms.RadioButton();
			this.lblType = new System.Windows.Forms.Label();
			this.lblTitle = new System.Windows.Forms.Label();
			this.txtTitle = new System.Windows.Forms.TextBox();
			this.lblDescription = new System.Windows.Forms.Label();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.panel1 = new System.Windows.Forms.Panel();
			this.radCompleto = new System.Windows.Forms.RadioButton();
			this.radParcial = new System.Windows.Forms.RadioButton();
			this.radBorrador = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtFrequency = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.lblPerson = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txtPeriod = new System.Windows.Forms.TextBox();
			this.lblContact = new System.Windows.Forms.Label();
			this.txtCoverage = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtReleaseDate = new System.Windows.Forms.TextBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.txtLicense = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.uInstitution = new medea.controls.uInstitution();
			this.uContact = new medea.controls.uContact();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.uSource = new medea.controls.uSource();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.uFiles = new medea.controls.uMetadataFiles();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.SuspendLayout();
			//
			// lblAuthor
			//
			this.lblAuthor.AutoSize = true;
			this.lblAuthor.Location = new System.Drawing.Point(18, 23);
			this.lblAuthor.Name = "lblAuthor";
			this.lblAuthor.Size = new System.Drawing.Size(46, 13);
			this.lblAuthor.TabIndex = 8;
			this.lblAuthor.Text = "Autores:";
			//
			// txtAuthor
			//
			this.txtAuthor.Location = new System.Drawing.Point(96, 20);
			this.txtAuthor.MaxLength = 255;
			this.txtAuthor.Name = "txtAuthor";
			this.txtAuthor.Size = new System.Drawing.Size(257, 20);
			this.txtAuthor.TabIndex = 9;
			//
			// radMapping
			//
			this.radMapping.AutoSize = true;
			this.radMapping.Location = new System.Drawing.Point(337, 66);
			this.radMapping.Name = "radMapping";
			this.radMapping.Size = new System.Drawing.Size(58, 17);
			this.radMapping.TabIndex = 5;
			this.radMapping.Text = "Mapeo";
			this.radMapping.UseVisualStyleBackColor = true;
			this.radMapping.Visible = false;
			//
			// radResearch
			//
			this.radResearch.AutoSize = true;
			this.radResearch.Location = new System.Drawing.Point(243, 66);
			this.radResearch.Name = "radResearch";
			this.radResearch.Size = new System.Drawing.Size(88, 17);
			this.radResearch.TabIndex = 4;
			this.radResearch.Text = "Investigación";
			this.radResearch.UseVisualStyleBackColor = true;
			this.radResearch.Visible = false;
			//
			// radPublic
			//
			this.radPublic.AutoSize = true;
			this.radPublic.Checked = true;
			this.radPublic.Location = new System.Drawing.Point(142, 66);
			this.radPublic.Name = "radPublic";
			this.radPublic.Size = new System.Drawing.Size(95, 17);
			this.radPublic.TabIndex = 3;
			this.radPublic.TabStop = true;
			this.radPublic.Text = "Datos públicos";
			this.radPublic.UseVisualStyleBackColor = true;
			this.radPublic.Visible = false;
			//
			// lblType
			//
			this.lblType.AutoSize = true;
			this.lblType.Location = new System.Drawing.Point(20, 70);
			this.lblType.Name = "lblType";
			this.lblType.Size = new System.Drawing.Size(35, 13);
			this.lblType.TabIndex = 2;
			this.lblType.Text = "Tipo:*";
			this.lblType.Visible = false;
			//
			// lblTitle
			//
			this.lblTitle.AutoSize = true;
			this.lblTitle.Location = new System.Drawing.Point(20, 17);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(42, 13);
			this.lblTitle.TabIndex = 0;
			this.lblTitle.Text = "Título:*";
			//
			// txtTitle
			//
			this.txtTitle.Location = new System.Drawing.Point(142, 14);
			this.txtTitle.MaxLength = 255;
			this.txtTitle.Name = "txtTitle";
			this.txtTitle.Size = new System.Drawing.Size(257, 20);
			this.txtTitle.TabIndex = 1;
			//
			// lblDescription
			//
			this.lblDescription.AutoSize = true;
			this.lblDescription.Location = new System.Drawing.Point(20, 118);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(59, 13);
			this.lblDescription.TabIndex = 6;
			this.lblDescription.Text = "Resumen:*";
			//
			// txtDescription
			//
			this.txtDescription.Location = new System.Drawing.Point(142, 115);
			this.txtDescription.MaxLength = 4000;
			this.txtDescription.Multiline = true;
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = new System.Drawing.Size(257, 114);
			this.txtDescription.TabIndex = 7;
			//
			// tabControl1
			//
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(722, 441);
			this.tabControl1.TabIndex = 4;
			//
			// tabPage1
			//
			this.tabPage1.Controls.Add(this.panel1);
			this.tabPage1.Controls.Add(this.groupBox1);
			this.tabPage1.Controls.Add(this.label3);
			this.tabPage1.Controls.Add(this.textBox1);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Controls.Add(this.radMapping);
			this.tabPage1.Controls.Add(this.label2);
			this.tabPage1.Controls.Add(this.lblTitle);
			this.tabPage1.Controls.Add(this.radResearch);
			this.tabPage1.Controls.Add(this.txtDescription);
			this.tabPage1.Controls.Add(this.radPublic);
			this.tabPage1.Controls.Add(this.lblDescription);
			this.tabPage1.Controls.Add(this.lblType);
			this.tabPage1.Controls.Add(this.txtReleaseDate);
			this.tabPage1.Controls.Add(this.txtTitle);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(714, 415);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "General";
			this.tabPage1.UseVisualStyleBackColor = true;
			//
			// panel1
			//
			this.panel1.Controls.Add(this.radCompleto);
			this.panel1.Controls.Add(this.radParcial);
			this.panel1.Controls.Add(this.radBorrador);
			this.panel1.Location = new System.Drawing.Point(137, 87);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(264, 22);
			this.panel1.TabIndex = 28;
			//
			// radCompleto
			//
			this.radCompleto.AutoSize = true;
			this.radCompleto.Checked = true;
			this.radCompleto.Location = new System.Drawing.Point(5, 3);
			this.radCompleto.Name = "radCompleto";
			this.radCompleto.Size = new System.Drawing.Size(69, 17);
			this.radCompleto.TabIndex = 3;
			this.radCompleto.TabStop = true;
			this.radCompleto.Text = "Completo";
			this.radCompleto.UseVisualStyleBackColor = true;
			//
			// radParcial
			//
			this.radParcial.AutoSize = true;
			this.radParcial.Location = new System.Drawing.Point(80, 3);
			this.radParcial.Name = "radParcial";
			this.radParcial.Size = new System.Drawing.Size(57, 17);
			this.radParcial.TabIndex = 4;
			this.radParcial.Text = "Parcial";
			this.radParcial.UseVisualStyleBackColor = true;
			//
			// radBorrador
			//
			this.radBorrador.AutoSize = true;
			this.radBorrador.Location = new System.Drawing.Point(143, 3);
			this.radBorrador.Name = "radBorrador";
			this.radBorrador.Size = new System.Drawing.Size(65, 17);
			this.radBorrador.TabIndex = 5;
			this.radBorrador.Text = "Borrador";
			this.radBorrador.UseVisualStyleBackColor = true;
			//
			// groupBox1
			//
			this.groupBox1.Controls.Add(this.txtFrequency);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.lblPerson);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.txtPeriod);
			this.groupBox1.Controls.Add(this.lblContact);
			this.groupBox1.Controls.Add(this.txtCoverage);
			this.groupBox1.Location = new System.Drawing.Point(414, 110);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(279, 169);
			this.groupBox1.TabIndex = 27;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Cobertura";
			//
			// txtFrequency
			//
			this.txtFrequency.Location = new System.Drawing.Point(85, 109);
			this.txtFrequency.MaxLength = 255;
			this.txtFrequency.Name = "txtFrequency";
			this.txtFrequency.Size = new System.Drawing.Size(103, 20);
			this.txtFrequency.TabIndex = 31;
			//
			// label7
			//
			this.label7.Location = new System.Drawing.Point(82, 133);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(191, 31);
			this.label7.TabIndex = 26;
			this.label7.Text = "Frecuencia de actualización. Ej. Anual, Trimestral, Según necesidad.";
			//
			// label6
			//
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(82, 93);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(106, 13);
			this.label6.TabIndex = 26;
			this.label6.Text = "Ej. 2010, 2003-2007.";
			//
			// label5
			//
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(82, 52);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(165, 13);
			this.label5.TabIndex = 26;
			this.label5.Text = "Ej. Total país, Provincia de Salta.";
			//
			// lblPerson
			//
			this.lblPerson.AutoSize = true;
			this.lblPerson.Location = new System.Drawing.Point(11, 32);
			this.lblPerson.Name = "lblPerson";
			this.lblPerson.Size = new System.Drawing.Size(60, 13);
			this.lblPerson.TabIndex = 26;
			this.lblPerson.Text = "Cobertura:*";
			//
			// label4
			//
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(11, 112);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(63, 13);
			this.label4.TabIndex = 30;
			this.label4.Text = "Frecuencia:";
			//
			// txtPeriod
			//
			this.txtPeriod.Location = new System.Drawing.Point(85, 69);
			this.txtPeriod.MaxLength = 255;
			this.txtPeriod.Name = "txtPeriod";
			this.txtPeriod.Size = new System.Drawing.Size(129, 20);
			this.txtPeriod.TabIndex = 29;
			//
			// lblContact
			//
			this.lblContact.AutoSize = true;
			this.lblContact.Location = new System.Drawing.Point(11, 72);
			this.lblContact.Name = "lblContact";
			this.lblContact.Size = new System.Drawing.Size(48, 13);
			this.lblContact.TabIndex = 28;
			this.lblContact.Text = "Período:";
			//
			// txtCoverage
			//
			this.txtCoverage.Location = new System.Drawing.Point(85, 29);
			this.txtCoverage.MaxLength = 255;
			this.txtCoverage.Name = "txtCoverage";
			this.txtCoverage.Size = new System.Drawing.Size(129, 20);
			this.txtCoverage.TabIndex = 27;
			//
			// label3
			//
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(20, 93);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(47, 13);
			this.label3.TabIndex = 10;
			this.label3.Text = "Estado:*";
			//
			// textBox1
			//
			this.textBox1.Location = new System.Drawing.Point(142, 240);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(257, 114);
			this.textBox1.TabIndex = 9;
			//
			// label1
			//
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(20, 244);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(92, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "Descripción larga:";
			//
			// label2
			//
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(20, 43);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(116, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Fecha de publicación:*";
			//
			// txtReleaseDate
			//
			this.txtReleaseDate.Location = new System.Drawing.Point(142, 40);
			this.txtReleaseDate.MaxLength = 10;
			this.txtReleaseDate.Name = "txtReleaseDate";
			this.txtReleaseDate.Size = new System.Drawing.Size(82, 20);
			this.txtReleaseDate.TabIndex = 1;
			//
			// tabPage2
			//
			this.tabPage2.Controls.Add(this.txtLicense);
			this.tabPage2.Controls.Add(this.label8);
			this.tabPage2.Controls.Add(this.uInstitution);
			this.tabPage2.Controls.Add(this.uContact);
			this.tabPage2.Controls.Add(this.lblAuthor);
			this.tabPage2.Controls.Add(this.txtAuthor);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(714, 415);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Atribución";
			this.tabPage2.UseVisualStyleBackColor = true;
			//
			// txtLicense
			//
			this.txtLicense.Location = new System.Drawing.Point(96, 49);
			this.txtLicense.MaxLength = 255;
			this.txtLicense.Name = "txtLicense";
			this.txtLicense.Size = new System.Drawing.Size(257, 20);
			this.txtLicense.TabIndex = 29;
			//
			// label8
			//
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(18, 52);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(50, 13);
			this.label8.TabIndex = 28;
			this.label8.Text = "Licencia:";
			//
			// uInstitution
			//
			this.uInstitution.Location = new System.Drawing.Point(15, 83);
			this.uInstitution.Name = "uInstitution";
			this.uInstitution.Size = new System.Drawing.Size(331, 222);
			this.uInstitution.TabIndex = 27;
			//
			// uContact
			//
			this.uContact.Location = new System.Drawing.Point(361, 83);
			this.uContact.Name = "uContact";
			this.uContact.Size = new System.Drawing.Size(325, 189);
			this.uContact.TabIndex = 26;
			//
			// tabPage3
			//
			this.tabPage3.Controls.Add(this.uSource);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(714, 415);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Fuentes";
			this.tabPage3.UseVisualStyleBackColor = true;
			//
			// uSource
			//
			this.uSource.BackColor = System.Drawing.SystemColors.Control;
			this.uSource.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uSource.Location = new System.Drawing.Point(3, 3);
			this.uSource.Name = "uSource";
			this.uSource.Size = new System.Drawing.Size(708, 409);
			this.uSource.TabIndex = 0;
			//
			// tabPage4
			//
			this.tabPage4.Controls.Add(this.uFiles);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(714, 415);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Archivos adjuntos";
			this.tabPage4.UseVisualStyleBackColor = true;
			//
			// uFiles
			//
			this.uFiles.BackColor = System.Drawing.SystemColors.Control;
			this.uFiles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uFiles.Location = new System.Drawing.Point(0, 0);
			this.uFiles.Name = "uFiles";
			this.uFiles.Size = new System.Drawing.Size(714, 415);
			this.uFiles.TabIndex = 0;
			//
			// uMetadata
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabControl1);
			this.Name = "uMetadata";
			this.Size = new System.Drawing.Size(722, 441);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.tabPage3.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label lblAuthor;
		private System.Windows.Forms.TextBox txtAuthor;
		private System.Windows.Forms.RadioButton radMapping;
		private System.Windows.Forms.RadioButton radResearch;
		private System.Windows.Forms.RadioButton radPublic;
		private System.Windows.Forms.Label lblType;
		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.TextBox txtTitle;
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabPage tabPage4;
		private uSource uSource;
		private uMetadataFiles uFiles;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private uInstitution uInstitution;
		private uContact uContact;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtReleaseDate;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.RadioButton radBorrador;
		private System.Windows.Forms.RadioButton radParcial;
		private System.Windows.Forms.RadioButton radCompleto;
		private System.Windows.Forms.TextBox txtLicense;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtFrequency;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label lblPerson;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtPeriod;
		private System.Windows.Forms.Label lblContact;
		private System.Windows.Forms.TextBox txtCoverage;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Panel panel1;
	}
}
