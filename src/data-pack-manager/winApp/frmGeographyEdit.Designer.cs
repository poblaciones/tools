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
	partial class frmGeographyEdit
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGeographyEdit));
			this.grpGeneral = new System.Windows.Forms.GroupBox();
			this.lblRevision = new System.Windows.Forms.Label();
			this.txtRevision = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.txtCaption = new System.Windows.Forms.TextBox();
			this.lblCaption = new System.Windows.Forms.Label();
			this.lblParent = new System.Windows.Forms.Label();
			this.cmbParent = new medea.controls.uComboBox();
			this.grpItems = new System.Windows.Forms.GroupBox();
			this.uFile = new medea.controls.uFile();
			this.grpCampos = new System.Windows.Forms.GroupBox();
			this.lblHouseholds = new System.Windows.Forms.Label();
			this.lblFieldCaptionName = new System.Windows.Forms.Label();
			this.lblPopulation = new System.Windows.Forms.Label();
			this.lblFieldCodeName = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.lblChildren = new System.Windows.Forms.Label();
			this.lblParentItem = new System.Windows.Forms.Label();
			this.cmbUrbanity = new medea.controls.uComboBox();
			this.cmbChildren = new medea.controls.uComboBox();
			this.cmbFieldCaptionName = new medea.controls.uComboBox();
			this.cmbHousehold = new medea.controls.uComboBox();
			this.cmbFieldCodeName = new medea.controls.uComboBox();
			this.cmbPopulation = new medea.controls.uComboBox();
			this.cmbParentItem = new medea.controls.uComboBox();
			this.lblRecords = new System.Windows.Forms.Label();
			this.lblShapeType = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.uFileMissings = new medea.controls.uFile();
			this.txtCoverage = new System.Windows.Forms.TextBox();
			this.chPartialCoverage = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.lblCoverage = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.txtMaxZoom = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnEditMetadata = new System.Windows.Forms.Button();
			this.panMain.SuspendLayout();
			this.grpGeneral.SuspendLayout();
			this.grpItems.SuspendLayout();
			this.grpCampos.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			//
			// panButtons
			//
			this.panButtons.Location = new System.Drawing.Point(0, 475);
			this.panButtons.Size = new System.Drawing.Size(813, 55);
			//
			// panMain
			//
			this.panMain.Controls.Add(this.groupBox1);
			this.panMain.Controls.Add(this.groupBox4);
			this.panMain.Controls.Add(this.groupBox2);
			this.panMain.Controls.Add(this.lblShapeType);
			this.panMain.Controls.Add(this.label4);
			this.panMain.Controls.Add(this.lblRecords);
			this.panMain.Controls.Add(this.grpGeneral);
			this.panMain.Controls.Add(this.grpItems);
			this.panMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.panMain.Size = new System.Drawing.Size(813, 475);
			//
			// grpGeneral
			//
			this.grpGeneral.Controls.Add(this.lblRevision);
			this.grpGeneral.Controls.Add(this.txtRevision);
			this.grpGeneral.Controls.Add(this.label9);
			this.grpGeneral.Controls.Add(this.txtCaption);
			this.grpGeneral.Controls.Add(this.lblCaption);
			this.grpGeneral.Controls.Add(this.lblParent);
			this.grpGeneral.Controls.Add(this.cmbParent);
			this.grpGeneral.Location = new System.Drawing.Point(12, 12);
			this.grpGeneral.Name = "grpGeneral";
			this.grpGeneral.Size = new System.Drawing.Size(373, 119);
			this.grpGeneral.TabIndex = 0;
			this.grpGeneral.TabStop = false;
			this.grpGeneral.Text = "General";
			//
			// lblRevision
			//
			this.lblRevision.AutoSize = true;
			this.lblRevision.Location = new System.Drawing.Point(15, 65);
			this.lblRevision.Name = "lblRevision";
			this.lblRevision.Size = new System.Drawing.Size(78, 13);
			this.lblRevision.TabIndex = 11;
			this.lblRevision.Text = "Revisión (año):";
			//
			// txtRevision
			//
			this.txtRevision.Location = new System.Drawing.Point(96, 61);
			this.txtRevision.MaxLength = 10;
			this.txtRevision.Name = "txtRevision";
			this.txtRevision.Size = new System.Drawing.Size(58, 20);
			this.txtRevision.TabIndex = 10;
			//
			// label9
			//
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(93, 42);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(145, 13);
			this.label9.TabIndex = 9;
			this.label9.Text = "Ej. Provincias (plural, sin año)";
			//
			// txtCaption
			//
			this.txtCaption.Location = new System.Drawing.Point(96, 19);
			this.txtCaption.MaxLength = 100;
			this.txtCaption.Name = "txtCaption";
			this.txtCaption.Size = new System.Drawing.Size(256, 20);
			this.txtCaption.TabIndex = 1;
			//
			// lblCaption
			//
			this.lblCaption.AutoSize = true;
			this.lblCaption.Location = new System.Drawing.Point(15, 23);
			this.lblCaption.Name = "lblCaption";
			this.lblCaption.Size = new System.Drawing.Size(51, 13);
			this.lblCaption.TabIndex = 0;
			this.lblCaption.Text = "Nombre:*";
			//
			// lblParent
			//
			this.lblParent.AutoSize = true;
			this.lblParent.Location = new System.Drawing.Point(15, 91);
			this.lblParent.Name = "lblParent";
			this.lblParent.Size = new System.Drawing.Size(38, 13);
			this.lblParent.TabIndex = 4;
			this.lblParent.Text = "Padre:";
			//
			// cmbParent
			//
			this.cmbParent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbParent.FormattingEnabled = true;
			this.cmbParent.Location = new System.Drawing.Point(96, 87);
			this.cmbParent.Name = "cmbParent";
			this.cmbParent.Optional = true;
			this.cmbParent.Size = new System.Drawing.Size(181, 21);
			this.cmbParent.TabIndex = 5;
			this.cmbParent.SelectedIndexChanged += new System.EventHandler(this.cmbParent_SelectedIndexChanged);
			//
			// grpItems
			//
			this.grpItems.Controls.Add(this.uFile);
			this.grpItems.Controls.Add(this.grpCampos);
			this.grpItems.Location = new System.Drawing.Point(12, 137);
			this.grpItems.Name = "grpItems";
			this.grpItems.Size = new System.Drawing.Size(293, 309);
			this.grpItems.TabIndex = 3;
			this.grpItems.TabStop = false;
			this.grpItems.Text = "Items";
			//
			// uFile
			//
			this.uFile.BackColor = System.Drawing.SystemColors.Control;
			this.uFile.EnabledButtons = true;
			this.uFile.FileAdded = false;
			this.uFile.FileDeleted = false;
			this.uFile.Location = new System.Drawing.Point(18, 19);
			this.uFile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.uFile.Name = "uFile";
			this.uFile.SelectedFile = null;
			this.uFile.Size = new System.Drawing.Size(215, 58);
			this.uFile.TabIndex = 0;
			//
			// grpCampos
			//
			this.grpCampos.Controls.Add(this.lblHouseholds);
			this.grpCampos.Controls.Add(this.lblFieldCaptionName);
			this.grpCampos.Controls.Add(this.lblPopulation);
			this.grpCampos.Controls.Add(this.lblFieldCodeName);
			this.grpCampos.Controls.Add(this.label3);
			this.grpCampos.Controls.Add(this.lblChildren);
			this.grpCampos.Controls.Add(this.lblParentItem);
			this.grpCampos.Controls.Add(this.cmbUrbanity);
			this.grpCampos.Controls.Add(this.cmbChildren);
			this.grpCampos.Controls.Add(this.cmbFieldCaptionName);
			this.grpCampos.Controls.Add(this.cmbHousehold);
			this.grpCampos.Controls.Add(this.cmbFieldCodeName);
			this.grpCampos.Controls.Add(this.cmbPopulation);
			this.grpCampos.Controls.Add(this.cmbParentItem);
			this.grpCampos.Location = new System.Drawing.Point(18, 89);
			this.grpCampos.Name = "grpCampos";
			this.grpCampos.Size = new System.Drawing.Size(259, 214);
			this.grpCampos.TabIndex = 1;
			this.grpCampos.TabStop = false;
			this.grpCampos.Text = "Campos";
			//
			// lblHouseholds
			//
			this.lblHouseholds.AutoSize = true;
			this.lblHouseholds.Location = new System.Drawing.Point(14, 129);
			this.lblHouseholds.Name = "lblHouseholds";
			this.lblHouseholds.Size = new System.Drawing.Size(54, 13);
			this.lblHouseholds.TabIndex = 8;
			this.lblHouseholds.Text = "Hogares*:";
			//
			// lblFieldCaptionName
			//
			this.lblFieldCaptionName.AutoSize = true;
			this.lblFieldCaptionName.Location = new System.Drawing.Point(14, 75);
			this.lblFieldCaptionName.Name = "lblFieldCaptionName";
			this.lblFieldCaptionName.Size = new System.Drawing.Size(66, 13);
			this.lblFieldCaptionName.TabIndex = 4;
			this.lblFieldCaptionName.Text = "Descripción:";
			//
			// lblPopulation
			//
			this.lblPopulation.AutoSize = true;
			this.lblPopulation.Location = new System.Drawing.Point(14, 102);
			this.lblPopulation.Name = "lblPopulation";
			this.lblPopulation.Size = new System.Drawing.Size(61, 13);
			this.lblPopulation.TabIndex = 6;
			this.lblPopulation.Text = "Población*:";
			//
			// lblFieldCodeName
			//
			this.lblFieldCodeName.AutoSize = true;
			this.lblFieldCodeName.Location = new System.Drawing.Point(14, 49);
			this.lblFieldCodeName.Name = "lblFieldCodeName";
			this.lblFieldCodeName.Size = new System.Drawing.Size(47, 13);
			this.lblFieldCodeName.TabIndex = 2;
			this.lblFieldCodeName.Text = "Código*:";
			//
			// label3
			//
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(14, 183);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(83, 13);
			this.label3.TabIndex = 11;
			this.label3.Text = "Urbano/Rural**:";
			//
			// lblChildren
			//
			this.lblChildren.AutoSize = true;
			this.lblChildren.Location = new System.Drawing.Point(14, 156);
			this.lblChildren.Name = "lblChildren";
			this.lblChildren.Size = new System.Drawing.Size(41, 13);
			this.lblChildren.TabIndex = 10;
			this.lblChildren.Text = "Niños*:";
			//
			// lblParentItem
			//
			this.lblParentItem.AutoSize = true;
			this.lblParentItem.Location = new System.Drawing.Point(14, 22);
			this.lblParentItem.Name = "lblParentItem";
			this.lblParentItem.Size = new System.Drawing.Size(38, 13);
			this.lblParentItem.TabIndex = 0;
			this.lblParentItem.Text = "Padre:";
			//
			// cmbUrbanity
			//
			this.cmbUrbanity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbUrbanity.FormattingEnabled = true;
			this.cmbUrbanity.Location = new System.Drawing.Point(103, 180);
			this.cmbUrbanity.Name = "cmbUrbanity";
			this.cmbUrbanity.Optional = true;
			this.cmbUrbanity.Size = new System.Drawing.Size(139, 21);
			this.cmbUrbanity.TabIndex = 12;
			//
			// cmbChildren
			//
			this.cmbChildren.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbChildren.FormattingEnabled = true;
			this.cmbChildren.Location = new System.Drawing.Point(103, 153);
			this.cmbChildren.Name = "cmbChildren";
			this.cmbChildren.Optional = true;
			this.cmbChildren.Size = new System.Drawing.Size(139, 21);
			this.cmbChildren.TabIndex = 10;
			//
			// cmbFieldCaptionName
			//
			this.cmbFieldCaptionName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbFieldCaptionName.FormattingEnabled = true;
			this.cmbFieldCaptionName.Location = new System.Drawing.Point(103, 72);
			this.cmbFieldCaptionName.Name = "cmbFieldCaptionName";
			this.cmbFieldCaptionName.Optional = true;
			this.cmbFieldCaptionName.Size = new System.Drawing.Size(139, 21);
			this.cmbFieldCaptionName.TabIndex = 5;
			//
			// cmbHousehold
			//
			this.cmbHousehold.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbHousehold.FormattingEnabled = true;
			this.cmbHousehold.Location = new System.Drawing.Point(103, 126);
			this.cmbHousehold.Name = "cmbHousehold";
			this.cmbHousehold.Optional = true;
			this.cmbHousehold.Size = new System.Drawing.Size(139, 21);
			this.cmbHousehold.TabIndex = 9;
			//
			// cmbFieldCodeName
			//
			this.cmbFieldCodeName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbFieldCodeName.FormattingEnabled = true;
			this.cmbFieldCodeName.Location = new System.Drawing.Point(103, 46);
			this.cmbFieldCodeName.Name = "cmbFieldCodeName";
			this.cmbFieldCodeName.Optional = true;
			this.cmbFieldCodeName.Size = new System.Drawing.Size(139, 21);
			this.cmbFieldCodeName.TabIndex = 3;
			//
			// cmbPopulation
			//
			this.cmbPopulation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbPopulation.FormattingEnabled = true;
			this.cmbPopulation.Location = new System.Drawing.Point(103, 99);
			this.cmbPopulation.Name = "cmbPopulation";
			this.cmbPopulation.Optional = true;
			this.cmbPopulation.Size = new System.Drawing.Size(139, 21);
			this.cmbPopulation.TabIndex = 7;
			//
			// cmbParentItem
			//
			this.cmbParentItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbParentItem.FormattingEnabled = true;
			this.cmbParentItem.Location = new System.Drawing.Point(103, 19);
			this.cmbParentItem.Name = "cmbParentItem";
			this.cmbParentItem.Optional = true;
			this.cmbParentItem.Size = new System.Drawing.Size(139, 21);
			this.cmbParentItem.TabIndex = 1;
			//
			// lblRecords
			//
			this.lblRecords.AutoSize = true;
			this.lblRecords.Location = new System.Drawing.Point(313, 376);
			this.lblRecords.Name = "lblRecords";
			this.lblRecords.Size = new System.Drawing.Size(72, 13);
			this.lblRecords.TabIndex = 8;
			this.lblRecords.Text = "Registros dbf:";
			this.lblRecords.Visible = false;
			//
			// lblShapeType
			//
			this.lblShapeType.AutoSize = true;
			this.lblShapeType.Location = new System.Drawing.Point(313, 392);
			this.lblShapeType.Name = "lblShapeType";
			this.lblShapeType.Size = new System.Drawing.Size(51, 13);
			this.lblShapeType.TabIndex = 8;
			this.lblShapeType.Text = "Tipo shp:";
			this.lblShapeType.Visible = false;
			//
			// groupBox2
			//
			this.groupBox2.Controls.Add(this.uFileMissings);
			this.groupBox2.Controls.Add(this.txtCoverage);
			this.groupBox2.Controls.Add(this.chPartialCoverage);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.lblCoverage);
			this.groupBox2.Location = new System.Drawing.Point(316, 137);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(490, 234);
			this.groupBox2.TabIndex = 4;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Cobertura parcial";
			//
			// uFileMissings
			//
			this.uFileMissings.BackColor = System.Drawing.SystemColors.Control;
			this.uFileMissings.Enabled = false;
			this.uFileMissings.EnabledButtons = true;
			this.uFileMissings.FileAdded = false;
			this.uFileMissings.FileDeleted = false;
			this.uFileMissings.Location = new System.Drawing.Point(98, 122);
			this.uFileMissings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.uFileMissings.Name = "uFileMissings";
			this.uFileMissings.SelectedFile = null;
			this.uFileMissings.Size = new System.Drawing.Size(191, 57);
			this.uFileMissings.TabIndex = 4;
			//
			// txtCoverage
			//
			this.txtCoverage.Enabled = false;
			this.txtCoverage.Location = new System.Drawing.Point(99, 46);
			this.txtCoverage.MaxLength = 500;
			this.txtCoverage.Multiline = true;
			this.txtCoverage.Name = "txtCoverage";
			this.txtCoverage.Size = new System.Drawing.Size(330, 68);
			this.txtCoverage.TabIndex = 2;
			//
			// chPartialCoverage
			//
			this.chPartialCoverage.AutoSize = true;
			this.chPartialCoverage.Location = new System.Drawing.Point(8, 23);
			this.chPartialCoverage.Name = "chPartialCoverage";
			this.chPartialCoverage.Size = new System.Drawing.Size(212, 17);
			this.chPartialCoverage.TabIndex = 0;
			this.chPartialCoverage.Text = "Ofrece solamente una cobertura parcial";
			this.chPartialCoverage.UseVisualStyleBackColor = true;
			this.chPartialCoverage.CheckedChanged += new System.EventHandler(this.chPartialCoverage_CheckedChanged);
			//
			// label2
			//
			this.label2.Location = new System.Drawing.Point(8, 184);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(313, 45);
			this.label2.TabIndex = 5;
			this.label2.Text = "*** Las áreas faltantes deben estar contenidas en un archivo dbf con igual estruc" +
    "tura de campos que el archivo de Items (iguales campos para padre, código, pobla" +
    "ción, hogares, niños).";
			//
			// label1
			//
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 125);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(92, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Áreas faltantes***:";
			//
			// lblCoverage
			//
			this.lblCoverage.AutoSize = true;
			this.lblCoverage.Location = new System.Drawing.Point(7, 49);
			this.lblCoverage.Name = "lblCoverage";
			this.lblCoverage.Size = new System.Drawing.Size(83, 13);
			this.lblCoverage.TabIndex = 1;
			this.lblCoverage.Text = "Áreas cubiertas:";
			//
			// label4
			//
			this.label4.Location = new System.Drawing.Point(315, 419);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(443, 44);
			this.label4.TabIndex = 4;
			this.label4.Text = resources.GetString("label4.Text");
			//
			// groupBox4
			//
			this.groupBox4.Controls.Add(this.txtMaxZoom);
			this.groupBox4.Controls.Add(this.label13);
			this.groupBox4.Location = new System.Drawing.Point(393, 12);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(201, 119);
			this.groupBox4.TabIndex = 11;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Selección de escala";
			//
			// txtMaxZoom
			//
			this.txtMaxZoom.Location = new System.Drawing.Point(92, 39);
			this.txtMaxZoom.MaxLength = 255;
			this.txtMaxZoom.Name = "txtMaxZoom";
			this.txtMaxZoom.Size = new System.Drawing.Size(48, 20);
			this.txtMaxZoom.TabIndex = 5;
			//
			// label13
			//
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(15, 42);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(74, 13);
			this.label13.TabIndex = 4;
			this.label13.Text = "Máximo zoom:";
			//
			// groupBox1
			//
			this.groupBox1.Controls.Add(this.btnEditMetadata);
			this.groupBox1.Location = new System.Drawing.Point(600, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(201, 119);
			this.groupBox1.TabIndex = 11;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Metadatos";
			//
			// btnEditMetadata
			//
			this.btnEditMetadata.Location = new System.Drawing.Point(38, 30);
			this.btnEditMetadata.Name = "btnEditMetadata";
			this.btnEditMetadata.Size = new System.Drawing.Size(120, 39);
			this.btnEditMetadata.TabIndex = 0;
			this.btnEditMetadata.Text = "Editar Metadatos...";
			this.btnEditMetadata.UseVisualStyleBackColor = true;
			this.btnEditMetadata.Click += new System.EventHandler(this.btnEditMetadata_Click);
			//
			// frmGeographyEdit
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(813, 530);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "frmGeographyEdit";
			this.ShowInTaskbar = false;
			this.Text = "Geografía";
			this.panMain.ResumeLayout(false);
			this.panMain.PerformLayout();
			this.grpGeneral.ResumeLayout(false);
			this.grpGeneral.PerformLayout();
			this.grpItems.ResumeLayout(false);
			this.grpCampos.ResumeLayout(false);
			this.grpCampos.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox grpGeneral;
		private System.Windows.Forms.TextBox txtCaption;
		private System.Windows.Forms.Label lblCaption;
		private System.Windows.Forms.Label lblParent;
		private controls.uComboBox cmbParent;
		private System.Windows.Forms.GroupBox grpItems;
		private System.Windows.Forms.GroupBox grpCampos;
		private System.Windows.Forms.Label lblFieldCaptionName;
		private System.Windows.Forms.Label lblFieldCodeName;
		private System.Windows.Forms.Label lblParentItem;
		private controls.uComboBox cmbFieldCaptionName;
		private controls.uComboBox cmbFieldCodeName;
		private controls.uComboBox cmbParentItem;
		private uFile uFile;
		private System.Windows.Forms.Label lblHouseholds;
		private System.Windows.Forms.Label lblPopulation;
		private System.Windows.Forms.Label lblChildren;
		private controls.uComboBox cmbChildren;
		private controls.uComboBox cmbHousehold;
		private controls.uComboBox cmbPopulation;
		private System.Windows.Forms.Label lblRecords;
		private System.Windows.Forms.Label lblShapeType;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox txtCoverage;
		private System.Windows.Forms.CheckBox chPartialCoverage;
		private System.Windows.Forms.Label lblCoverage;
		private uFile uFileMissings;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private uComboBox cmbUrbanity;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.TextBox txtMaxZoom;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnEditMetadata;
		private System.Windows.Forms.Label lblRevision;
		private System.Windows.Forms.TextBox txtRevision;
	}
}

