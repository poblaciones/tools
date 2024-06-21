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
	partial class uContact
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
			this.txtPhone = new System.Windows.Forms.TextBox();
			this.lblPerson = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.txtEmail = new System.Windows.Forms.TextBox();
			this.lblContact = new System.Windows.Forms.Label();
			this.txtPerson = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			//
			// groupBox1
			//
			this.groupBox1.Controls.Add(this.txtPhone);
			this.groupBox1.Controls.Add(this.lblPerson);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.txtEmail);
			this.groupBox1.Controls.Add(this.lblContact);
			this.groupBox1.Controls.Add(this.txtPerson);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(377, 169);
			this.groupBox1.TabIndex = 26;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Contacto";
			//
			// txtPhone
			//
			this.txtPhone.Location = new System.Drawing.Point(108, 82);
			this.txtPhone.MaxLength = 255;
			this.txtPhone.Name = "txtPhone";
			this.txtPhone.Size = new System.Drawing.Size(171, 20);
			this.txtPhone.TabIndex = 31;
			//
			// lblPerson
			//
			this.lblPerson.AutoSize = true;
			this.lblPerson.Location = new System.Drawing.Point(11, 32);
			this.lblPerson.Name = "lblPerson";
			this.lblPerson.Size = new System.Drawing.Size(51, 13);
			this.lblPerson.TabIndex = 26;
			this.lblPerson.Text = "Nombre:";
			//
			// label8
			//
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(11, 85);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(52, 13);
			this.label8.TabIndex = 30;
			this.label8.Text = "Teléfono:";
			//
			// txtEmail
			//
			this.txtEmail.Location = new System.Drawing.Point(108, 56);
			this.txtEmail.MaxLength = 255;
			this.txtEmail.Name = "txtEmail";
			this.txtEmail.Size = new System.Drawing.Size(171, 20);
			this.txtEmail.TabIndex = 29;
			//
			// lblContact
			//
			this.lblContact.AutoSize = true;
			this.lblContact.Location = new System.Drawing.Point(11, 59);
			this.lblContact.Name = "lblContact";
			this.lblContact.Size = new System.Drawing.Size(90, 13);
			this.lblContact.TabIndex = 28;
			this.lblContact.Text = "Correo electrónico:";
			//
			// txtPerson
			//
			this.txtPerson.Location = new System.Drawing.Point(108, 29);
			this.txtPerson.MaxLength = 255;
			this.txtPerson.Name = "txtPerson";
			this.txtPerson.Size = new System.Drawing.Size(203, 20);
			this.txtPerson.TabIndex = 27;
			//
			// uContact
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.Name = "uContact";
			this.Size = new System.Drawing.Size(377, 169);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtPhone;
		private System.Windows.Forms.Label lblPerson;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox txtEmail;
		private System.Windows.Forms.Label lblContact;
		private System.Windows.Forms.TextBox txtPerson;
	}
}
