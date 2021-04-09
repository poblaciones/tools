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
using System;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using medea.controls;
using medea.actions;
using medea.entities;
using medea.common;
using System.Diagnostics;
using System.Web;
using medea.Data;

namespace medea.winApp
{
	public partial class frmMain : Form
	{
		private Form mainControl;
		public Panel MainPanel;
		private string User = null;
		private string Password = null;
		private string Server = null;

		public frmMain()
		{
			Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

			InitializeComponent();

			MainPanel = panMain;

			RegistryPersistence.LoadUserFromRegistry(ref User, ref Password, ref Server);
			var f = new frmLogin();
			f.User = User;
			f.Password = Password;
			f.Server = Server;
			while (true)
			{
				if (f.ShowDialog() == DialogResult.Cancel)
				{
					UI.ShowMessage(this, "No ha sido posible iniciar una sesi�n en el servidor. \n\nServidor: " + HttpInvoker.GetServer("No indicado"));
					Process.GetCurrentProcess().Kill();
					return;
				}
				else
				{
					this.User = f.User;
					this.Password = f.Password;
					HttpInvoker.Server = f.Server;
				}
				if (Login()) break;
			}
		}

		private bool Login()
		{
			try
			{
				var start = ResolveStartUrl();
				HttpResult res = null;
				this.DialogResult = DialogResult.None;
				while (res == null || res.Completed == false)
				{
					res = HttpInvoker.CallProgress(start + "&r=1", null, false);
					if (res == null)
						return false;
				}
				NHibernateSession.DbSettings["Server"] = res.Extra["Server"];
				NHibernateSession.DbSettings["Database"] = res.Extra["Database"];
				NHibernateSession.DbSettings["DatabaseType"] = res.Extra["DatabaseType"];
				NHibernateSession.DbSettings["User"] = res.Extra["User"];
				NHibernateSession.DbSettings["SslMode"] = res.Extra["SslMode"];
				NHibernateSession.DbSettings["PasswordPlain"] = res.Extra["PasswordPlain"];
				NHibernateSession.DbSettings["SslMode"] = "Preferred";
				UI.CurrentUserEmail = getCurrentUser();
				lblUser.Text = getCurrentUser();
				return true;
			}
			catch (Exception e)
			{
				MessageBox.Show(this, e.ToString());
				return false;
			}
		}
		private string getCurrentUser()
		{
			return HttpInvoker.ConfigurationGet("HttpUser");
		}
		private string ResolveStartUrl()
		{
			string start = "authenticate/winLogin";
			string u;
			string p;
			if (this.User != null)
				u = this.User;
			else
				u = getCurrentUser();

			if (this.Password != null)
				p = this.Password;
			else
				p = HttpInvoker.ConfigurationGet("HttpPasswordPlain");;

			start += "?u=" + HttpUtility.UrlEncode(u);
			start += "&p=" + HttpUtility.UrlEncode(p);
			return start;
		}

		public void Display(Form f)
		{
			mainControl = f;
			MainPanel.Controls.Clear();
			f.FormBorderStyle = FormBorderStyle.None;
			f.TopLevel = false;
			MainPanel.Controls.Add(f);
			f.Left = -f.Width;
			MainPanel.FindForm().Text = f.Text + " - " + GetServer();
			f.Dock = DockStyle.Fill;
			f.Visible = true;
		}

		private string GetServer()
		{
			return medea.context.Data.Session.ConnectionSettings.Database.ToUpper() + " (" + medea.context.Data.Session.ConnectionSettings.Server + ")";
		}

		void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			context.Data.CheckSession();

			UI.ShowError(this, e.Exception);
		}

		void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			UI.ShowError(this, e.ExceptionObject as Exception);
			Thread.CurrentThread.Suspend();
		}



		private void menGeography_Click(object sender, EventArgs e)
		{
			Display(new frmGeography());
		}

		public void ReloadControl()
		{
			if (mainControl == null) return;
			Form newForm = Activator.CreateInstance(mainControl.GetType()) as Form;
			Display(newForm);
		}

		private void menCheckNH_Click(object sender, EventArgs e)
		{
			if (Invoker.CallProgress(new DbCheck()))
				UI.ShowInfoMessage(this, "Los mapeos se han verificado correctamente.");
		}

		private void menClipping_Click(object sender, EventArgs e)
		{
			Display(new frmClippingRegion());
		}


		private void menClippingGeography_Click(object sender, EventArgs e)
		{
			Display(new frmClippingGeography());
		}


		private void frmMain_Load(object sender, EventArgs e)
		{
			using (new WaitCursor())
			{
				var countries = UI.GetCountries();
				cmbCountry.Fill(countries, x => x.Caption);
				cmbCountry.SelectByText<ClippingRegionItem>("Argentina");
			}
		}

		private void cmbCountry_SelectedIndexChanged(object sender, EventArgs e)
		{
			UI.CurrentCountry = cmbCountry.GetSelectedItem<ClippingRegionItem>();
			ReloadControl();
		}

		private void btnCountryEdit_Click(object sender, EventArgs e)
		{
			frmCountryEdit f = new frmCountryEdit();
			f.LoadData(cmbCountry.GetSelectedItem<ClippingRegionItem>());
			if (f.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				var sel = cmbCountry.SelectedItem as ComboItem<ClippingRegionItem>;
				sel.Text = sel.Tag.Caption;
				int n = cmbCountry.SelectedIndex;
				cmbCountry.Items.Insert(cmbCountry.SelectedIndex, sel);
				cmbCountry.SelectedIndex = n;
				cmbCountry.Items.RemoveAt(cmbCountry.SelectedIndex + 1);
				cmbCountry.Focus();
			}
		}

		private void btnNewMain_Click(object sender, EventArgs e)
		{
			frmMain n = new frmMain();
			n.HideGlobalPanel();
			n.Show();
		}

		private void HideGlobalPanel()
		{
			panGlobalSettings.Visible = false;
		}


		private void btnSimplify_Click(object sender, EventArgs e)
		{
			var f = new frmsimplify();
			f.ShowDialog(this);
		}

		private void btnGradient_Click(object sender, EventArgs e)
		{
			Display(new frmGradient());
		}

		private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			Application.Exit();
		}

		private void btnBoundary_Click(object sender, EventArgs e)
		{
			Display(new frmBoundary());
		}
	}
}