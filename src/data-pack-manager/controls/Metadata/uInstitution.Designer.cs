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
	partial class uInstitution
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtWeb = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtCountry = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtAddress = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtPhone = new System.Windows.Forms.TextBox();
			this.lblPerson = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txtEmail = new System.Windows.Forms.TextBox();
			this.lblContact = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.cmbInstitution = new medea.controls.uComboBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			//
			// groupBox1
			//
			this.groupBox1.Controls.Add(this.cmbInstitution);
			this.groupBox1.Controls.Add(this.txtWeb);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.txtCountry);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.txtAddress);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.txtPhone);
			this.groupBox1.Controls.Add(this.lblPerson);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.txtEmail);
			this.groupBox1.Controls.Add(this.lblContact);
			this.groupBox1.Controls.Add(this.txtName);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(398, 234);
			this.groupBox1.TabIndex = 40;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Institución";
			//
			// txtWeb
			//
			this.txtWeb.Location = new System.Drawing.Point(107, 192);
			this.txtWeb.MaxLength = 255;
			this.txtWeb.Name = "txtWeb";
			this.txtWeb.Size = new System.Drawing.Size(171, 20);
			this.txtWeb.TabIndex = 53;
			//
			// label4
			//
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(16, 195);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(66, 13);
			this.label4.TabIndex = 52;
			this.label4.Text = "Página web:";
			//
			// txtCountry
			//
			this.txtCountry.Location = new System.Drawing.Point(107, 169);
			this.txtCountry.MaxLength = 50;
			this.txtCountry.Name = "txtCountry";
			this.txtCountry.Size = new System.Drawing.Size(171, 20);
			this.txtCountry.TabIndex = 51;
			//
			// label2
			//
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(16, 172);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 13);
			this.label2.TabIndex = 50;
			this.label2.Text = "País:";
			//
			// txtAddress
			//
			this.txtAddress.Location = new System.Drawing.Point(107, 143);
			this.txtAddress.MaxLength = 255;
			this.txtAddress.Name = "txtAddress";
			this.txtAddress.Size = new System.Drawing.Size(171, 20);
			this.txtAddress.TabIndex = 49;
			//
			// label3
			//
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(16, 146);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(86, 13);
			this.label3.TabIndex = 48;
			this.label3.Text = "Dirección postal:";
			//
			// txtPhone
			//
			this.txtPhone.Location = new System.Drawing.Point(107, 116);
			this.txtPhone.MaxLength = 20;
			this.txtPhone.Name = "txtPhone";
			this.txtPhone.Size = new System.Drawing.Size(171, 20);
			this.txtPhone.TabIndex = 47;
			//
			// lblPerson
			//
			this.lblPerson.AutoSize = true;
			this.lblPerson.Location = new System.Drawing.Point(16, 66);
			this.lblPerson.Name = "lblPerson";
			this.lblPerson.Size = new System.Drawing.Size(51, 13);
			this.lblPerson.TabIndex = 42;
			this.lblPerson.Text = "Nombre:*";
			//
			// label1
			//
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 119);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(52, 13);
			this.label1.TabIndex = 46;
			this.label1.Text = "Teléfono:";
			//
			// txtEmail
			//
			this.txtEmail.Location = new System.Drawing.Point(107, 90);
			this.txtEmail.MaxLength = 50;
			this.txtEmail.Name = "txtEmail";
			this.txtEmail.Size = new System.Drawing.Size(171, 20);
			this.txtEmail.TabIndex = 45;
			//
			// lblContact
			//
			this.lblContact.AutoSize = true;
			this.lblContact.Location = new System.Drawing.Point(16, 93);
			this.lblContact.Name = "lblContact";
			this.lblContact.Size = new System.Drawing.Size(90, 13);
			this.lblContact.TabIndex = 44;
			this.lblContact.Text = "Correo elctrónico:";
			//
			// txtName
			//
			this.txtName.Location = new System.Drawing.Point(107, 63);
			this.txtName.MaxLength = 200;
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(203, 20);
			this.txtName.TabIndex = 43;
			//
			// label8
			//
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(16, 33);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(58, 13);
			this.label8.TabIndex = 40;
			this.label8.Text = "Institución:";
			//
			// cmbInstitution
			//
			this.cmbInstitution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbInstitution.FormattingEnabled = true;
			this.cmbInstitution.Location = new System.Drawing.Point(107, 30);
			this.cmbInstitution.Name = "cmbInstitution";
			this.cmbInstitution.Optional = true;
			this.cmbInstitution.Size = new System.Drawing.Size(203, 21);
			this.cmbInstitution.TabIndex = 54;
			this.cmbInstitution.SelectedIndexChanged += new System.EventHandler(this.cmbInstitution_SelectedIndexChanged);
			//
			// uInstitution
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.Name = "uInstitution";
			this.Size = new System.Drawing.Size(398, 234);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtWeb;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtCountry;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtAddress;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtPhone;
		private System.Windows.Forms.Label lblPerson;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtEmail;
		private System.Windows.Forms.Label lblContact;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label label8;
		private uComboBox cmbInstitution;
	}
}
