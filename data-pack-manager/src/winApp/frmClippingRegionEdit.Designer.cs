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
	partial class frmClippingRegionEdit
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
			this.txtPriority = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtCaption = new System.Windows.Forms.TextBox();
			this.lblCaption = new System.Windows.Forms.Label();
			this.lblParent = new System.Windows.Forms.Label();
			this.cmbParent = new medea.controls.uComboBox();
			this.grpItems = new System.Windows.Forms.GroupBox();
			this.uFile = new medea.controls.uFile();
			this.grpCampos = new System.Windows.Forms.GroupBox();
			this.lblFieldCaptionName = new System.Windows.Forms.Label();
			this.lblFieldCodeName = new System.Windows.Forms.Label();
			this.lblParentItem = new System.Windows.Forms.Label();
			this.cmbFieldCaptionName = new medea.controls.uComboBox();
			this.cmbFieldCodeName = new medea.controls.uComboBox();
			this.cmbParentItem = new medea.controls.uComboBox();
			this.lblRecords = new System.Windows.Forms.Label();
			this.lblShapeType = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label10 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.chEnableLabels = new System.Windows.Forms.CheckBox();
			this.txtMinZoom = new System.Windows.Forms.TextBox();
			this.txtSymbol = new System.Windows.Forms.TextBox();
			this.txtMaxZoom = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btnEditMetadata = new System.Windows.Forms.Button();
			this.chIndexCodes = new System.Windows.Forms.CheckBox();
			this.panMain.SuspendLayout();
			this.grpGeneral.SuspendLayout();
			this.grpItems.SuspendLayout();
			this.grpCampos.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panButtons
			// 
			this.panButtons.Location = new System.Drawing.Point(0, 380);
			this.panButtons.Size = new System.Drawing.Size(532, 55);
			// 
			// panMain
			// 
			this.panMain.Controls.Add(this.groupBox2);
			this.panMain.Controls.Add(this.groupBox1);
			this.panMain.Controls.Add(this.lblShapeType);
			this.panMain.Controls.Add(this.lblRecords);
			this.panMain.Controls.Add(this.grpGeneral);
			this.panMain.Controls.Add(this.grpItems);
			this.panMain.Margin = new System.Windows.Forms.Padding(6);
			this.panMain.Size = new System.Drawing.Size(532, 380);
			// 
			// grpGeneral
			// 
			this.grpGeneral.Controls.Add(this.txtPriority);
			this.grpGeneral.Controls.Add(this.label3);
			this.grpGeneral.Controls.Add(this.label2);
			this.grpGeneral.Controls.Add(this.txtCaption);
			this.grpGeneral.Controls.Add(this.lblCaption);
			this.grpGeneral.Controls.Add(this.lblParent);
			this.grpGeneral.Controls.Add(this.cmbParent);
			this.grpGeneral.Location = new System.Drawing.Point(12, 18);
			this.grpGeneral.Name = "grpGeneral";
			this.grpGeneral.Size = new System.Drawing.Size(250, 124);
			this.grpGeneral.TabIndex = 0;
			this.grpGeneral.TabStop = false;
			this.grpGeneral.Text = "General";
			// 
			// txtPriority
			// 
			this.txtPriority.Location = new System.Drawing.Point(84, 79);
			this.txtPriority.MaxLength = 100;
			this.txtPriority.Name = "txtPriority";
			this.txtPriority.Size = new System.Drawing.Size(56, 20);
			this.txtPriority.TabIndex = 9;
			this.txtPriority.Text = "0";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(81, 100);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(144, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "De 0=mínima a 100=máxima.";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(15, 82);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(55, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "Prioridad*:";
			// 
			// txtCaption
			// 
			this.txtCaption.Location = new System.Drawing.Point(84, 29);
			this.txtCaption.MaxLength = 100;
			this.txtCaption.Name = "txtCaption";
			this.txtCaption.Size = new System.Drawing.Size(160, 20);
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
			// lblParent
			// 
			this.lblParent.AutoSize = true;
			this.lblParent.Location = new System.Drawing.Point(15, 55);
			this.lblParent.Name = "lblParent";
			this.lblParent.Size = new System.Drawing.Size(42, 13);
			this.lblParent.TabIndex = 4;
			this.lblParent.Text = "Padre*:";
			// 
			// cmbParent
			// 
			this.cmbParent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbParent.FormattingEnabled = true;
			this.cmbParent.Location = new System.Drawing.Point(84, 53);
			this.cmbParent.Name = "cmbParent";
			this.cmbParent.Optional = true;
			this.cmbParent.Size = new System.Drawing.Size(160, 21);
			this.cmbParent.TabIndex = 5;
			this.cmbParent.SelectedIndexChanged += new System.EventHandler(this.cmbParent_SelectedIndexChanged);
			// 
			// grpItems
			// 
			this.grpItems.Controls.Add(this.uFile);
			this.grpItems.Controls.Add(this.grpCampos);
			this.grpItems.Location = new System.Drawing.Point(268, 94);
			this.grpItems.Name = "grpItems";
			this.grpItems.Size = new System.Drawing.Size(250, 223);
			this.grpItems.TabIndex = 2;
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
			this.uFile.Margin = new System.Windows.Forms.Padding(6);
			this.uFile.Name = "uFile";
			this.uFile.SelectedFile = null;
			this.uFile.Size = new System.Drawing.Size(215, 83);
			this.uFile.TabIndex = 0;
			// 
			// grpCampos
			// 
			this.grpCampos.Controls.Add(this.lblFieldCaptionName);
			this.grpCampos.Controls.Add(this.lblFieldCodeName);
			this.grpCampos.Controls.Add(this.lblParentItem);
			this.grpCampos.Controls.Add(this.cmbFieldCaptionName);
			this.grpCampos.Controls.Add(this.cmbFieldCodeName);
			this.grpCampos.Controls.Add(this.cmbParentItem);
			this.grpCampos.Location = new System.Drawing.Point(18, 108);
			this.grpCampos.Name = "grpCampos";
			this.grpCampos.Size = new System.Drawing.Size(215, 105);
			this.grpCampos.TabIndex = 1;
			this.grpCampos.TabStop = false;
			this.grpCampos.Text = "Campos";
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
			// lblFieldCodeName
			// 
			this.lblFieldCodeName.AutoSize = true;
			this.lblFieldCodeName.Location = new System.Drawing.Point(14, 49);
			this.lblFieldCodeName.Name = "lblFieldCodeName";
			this.lblFieldCodeName.Size = new System.Drawing.Size(43, 13);
			this.lblFieldCodeName.TabIndex = 2;
			this.lblFieldCodeName.Text = "Código:";
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
			// cmbFieldCaptionName
			// 
			this.cmbFieldCaptionName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbFieldCaptionName.FormattingEnabled = true;
			this.cmbFieldCaptionName.Location = new System.Drawing.Point(81, 72);
			this.cmbFieldCaptionName.Name = "cmbFieldCaptionName";
			this.cmbFieldCaptionName.Optional = true;
			this.cmbFieldCaptionName.Size = new System.Drawing.Size(121, 21);
			this.cmbFieldCaptionName.TabIndex = 5;
			// 
			// cmbFieldCodeName
			// 
			this.cmbFieldCodeName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbFieldCodeName.FormattingEnabled = true;
			this.cmbFieldCodeName.Location = new System.Drawing.Point(81, 46);
			this.cmbFieldCodeName.Name = "cmbFieldCodeName";
			this.cmbFieldCodeName.Optional = true;
			this.cmbFieldCodeName.Size = new System.Drawing.Size(121, 21);
			this.cmbFieldCodeName.TabIndex = 3;
			// 
			// cmbParentItem
			// 
			this.cmbParentItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbParentItem.FormattingEnabled = true;
			this.cmbParentItem.Location = new System.Drawing.Point(81, 19);
			this.cmbParentItem.Name = "cmbParentItem";
			this.cmbParentItem.Optional = true;
			this.cmbParentItem.Size = new System.Drawing.Size(121, 21);
			this.cmbParentItem.TabIndex = 1;
			// 
			// lblRecords
			// 
			this.lblRecords.AutoSize = true;
			this.lblRecords.Location = new System.Drawing.Point(271, 332);
			this.lblRecords.Name = "lblRecords";
			this.lblRecords.Size = new System.Drawing.Size(72, 13);
			this.lblRecords.TabIndex = 8;
			this.lblRecords.Text = "Registros dbf:";
			this.lblRecords.Visible = false;
			// 
			// lblShapeType
			// 
			this.lblShapeType.AutoSize = true;
			this.lblShapeType.Location = new System.Drawing.Point(271, 350);
			this.lblShapeType.Name = "lblShapeType";
			this.lblShapeType.Size = new System.Drawing.Size(51, 13);
			this.lblShapeType.TabIndex = 8;
			this.lblShapeType.Text = "Tipo shp:";
			this.lblShapeType.Visible = false;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.chIndexCodes);
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.chEnableLabels);
			this.groupBox1.Controls.Add(this.txtMinZoom);
			this.groupBox1.Controls.Add(this.txtSymbol);
			this.groupBox1.Controls.Add(this.txtMaxZoom);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Location = new System.Drawing.Point(12, 148);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(250, 218);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Etiquetas y búsqueda";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(15, 162);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(49, 13);
			this.label10.TabIndex = 21;
			this.label10.Text = "Símbolo:";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(11, 24);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(233, 41);
			this.label8.TabIndex = 11;
			this.label8.Text = "Indique si estas regiones de clipping deben ofrecerse en la búsqueda y el armado " +
    "de etiquetas del mapa.";
			// 
			// chEnableLabels
			// 
			this.chEnableLabels.AutoSize = true;
			this.chEnableLabels.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chEnableLabels.Location = new System.Drawing.Point(14, 78);
			this.chEnableLabels.Margin = new System.Windows.Forms.Padding(2);
			this.chEnableLabels.Name = "chEnableLabels";
			this.chEnableLabels.Size = new System.Drawing.Size(95, 17);
			this.chEnableLabels.TabIndex = 10;
			this.chEnableLabels.Text = "Activar:           ";
			this.chEnableLabels.UseVisualStyleBackColor = true;
			this.chEnableLabels.CheckedChanged += new System.EventHandler(this.chEnableLabels_CheckedChanged);
			// 
			// txtMinZoom
			// 
			this.txtMinZoom.Location = new System.Drawing.Point(94, 100);
			this.txtMinZoom.MaxLength = 100;
			this.txtMinZoom.Name = "txtMinZoom";
			this.txtMinZoom.Size = new System.Drawing.Size(46, 20);
			this.txtMinZoom.TabIndex = 12;
			this.txtMinZoom.Text = "0";
			// 
			// txtSymbol
			// 
			this.txtSymbol.Location = new System.Drawing.Point(94, 159);
			this.txtSymbol.MaxLength = 100;
			this.txtSymbol.Name = "txtSymbol";
			this.txtSymbol.Size = new System.Drawing.Size(131, 20);
			this.txtSymbol.TabIndex = 14;
			// 
			// txtMaxZoom
			// 
			this.txtMaxZoom.Location = new System.Drawing.Point(94, 127);
			this.txtMaxZoom.MaxLength = 100;
			this.txtMaxZoom.Name = "txtMaxZoom";
			this.txtMaxZoom.Size = new System.Drawing.Size(46, 20);
			this.txtMaxZoom.TabIndex = 14;
			this.txtMaxZoom.Text = "0";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(16, 104);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(78, 13);
			this.label6.TabIndex = 11;
			this.label6.Text = "Zoom mínimo*:";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(16, 131);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(79, 13);
			this.label7.TabIndex = 13;
			this.label7.Text = "Zoom máximo*:";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.btnEditMetadata);
			this.groupBox2.Location = new System.Drawing.Point(268, 18);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(250, 68);
			this.groupBox2.TabIndex = 12;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Metadatos";
			// 
			// btnEditMetadata
			// 
			this.btnEditMetadata.Location = new System.Drawing.Point(75, 19);
			this.btnEditMetadata.Name = "btnEditMetadata";
			this.btnEditMetadata.Size = new System.Drawing.Size(120, 39);
			this.btnEditMetadata.TabIndex = 0;
			this.btnEditMetadata.Text = "Editar Metadatos...";
			this.btnEditMetadata.UseVisualStyleBackColor = true;
			this.btnEditMetadata.Click += new System.EventHandler(this.btnEditMetadata_Click);
			// 
			// chIndexCodes
			// 
			this.chIndexCodes.AutoSize = true;
			this.chIndexCodes.Location = new System.Drawing.Point(19, 186);
			this.chIndexCodes.Margin = new System.Windows.Forms.Padding(2);
			this.chIndexCodes.Name = "chIndexCodes";
			this.chIndexCodes.Size = new System.Drawing.Size(101, 17);
			this.chIndexCodes.TabIndex = 22;
			this.chIndexCodes.Text = "Indexar códigos";
			this.chIndexCodes.UseVisualStyleBackColor = true;
			// 
			// frmClippingRegionEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(532, 435);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(6);
			this.Name = "frmClippingRegionEdit";
			this.ShowInTaskbar = false;
			this.Text = "Región";
			this.panMain.ResumeLayout(false);
			this.panMain.PerformLayout();
			this.grpGeneral.ResumeLayout(false);
			this.grpGeneral.PerformLayout();
			this.grpItems.ResumeLayout(false);
			this.grpCampos.ResumeLayout(false);
			this.grpCampos.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
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
		private System.Windows.Forms.Label lblRecords;
		private System.Windows.Forms.Label lblShapeType;
		private System.Windows.Forms.TextBox txtPriority;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.CheckBox chEnableLabels;
		private System.Windows.Forms.TextBox txtMinZoom;
		private System.Windows.Forms.TextBox txtMaxZoom;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btnEditMetadata;
		private System.Windows.Forms.TextBox txtSymbol;
		private System.Windows.Forms.CheckBox chIndexCodes;
	}
}

