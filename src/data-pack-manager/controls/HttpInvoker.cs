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
using System.Windows.Forms;
using medea.common;
using System.Threading;
using medea.actions;
using System.Collections.Specialized;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace medea.controls
{
	public class HttpInvoker
	{
		private static CookieContainer CookieContainer = new CookieContainer();
		private static bool xdebugCookieAdded = false;

		private frmProgress panProgress;
		bool ret = false;
		private string lastUrl;
		private string startUrl;
		private string stepUrl;
		private HttpResult LastResult;
		private bool showUrlOnError;
		delegate void ProgressDelegate(int current, int total, string caption, int subCurrent, int subTotal);

		public static HttpResult CallProgress(string startUrl, string stepUrl = null, bool showUrlOnError = true)
		{
			HttpInvoker i = new HttpInvoker();
			i.startUrl = startUrl;
			i.stepUrl = stepUrl;
			i.showUrlOnError = showUrlOnError;
			i.doCallProgress();
			if (i.ret)
				return i.LastResult;
			else
				return null;
		}
		private void doCallProgress()
		{
			// Ejecuta una acción mostrando un progress...
			panProgress = new frmProgress();
			panProgress.Start();
			WaitCursor.Set();
			// llama en otro thread a la llamada
			Thread t = new Thread(new ThreadStart(invoke));
			t.Name = "MedeaInvokerThread";
			t.Start();
			if (panProgress.InvokeRequired)
			{
				 MethodInvoker methodInvokerDelegate = delegate() 
                { panProgress.ShowDialog(Form.ActiveForm); };
				panProgress.Invoke(methodInvokerDelegate);
			} 
			else 
				panProgress.ShowDialog(Form.ActiveForm);
		}

		public static string Server;
		public static string GetServer(string v = null)
		{
			string ret = v;
			if (Server == null)
			{
				return ConfigurationGet("HttpServer", v);
			}
			else
				return Server;
		}

		public void invoke()
		{
			try
			{
				HttpResult res = Navigate(startUrl);
				LastResult = res;
				action_ProgressChanged(res);
				while (res.Completed == false && stepUrl != null)
				{
					res = Navigate(stepUrl + "?k=" + res.Key);
					LastResult = res;
					action_ProgressChanged(res);
				}
				panProgress.Invoke(new EventHandler(actionCompleted), new object[] { null, null });
			}
			catch(Exception ex)
			{
				panProgress.Invoke(new EventHandler(showError), new object[] { ex, null });
			}
		}

		private HttpResult Navigate(string relativeUrl)
		{
			string httpServer = HttpInvoker.GetServer();
			if (String.IsNullOrEmpty(httpServer))
				throw new Exception("AppConfig debe incluir HttpServer");
			if (!httpServer.EndsWith("/"))
				httpServer += "/";
			string fullurl = httpServer + relativeUrl;

			string xdebug = ConfigurationGet("HttpXDebugSession");
			if (xdebug != null && xdebugCookieAdded == false)
			{
				CookieContainer.Add(new Uri(httpServer), new Cookie("XDEBUG_SESSION", xdebug));
				xdebugCookieAdded = true;
			}
			WebClientEx wc = new WebClientEx(CookieContainer);
			this.lastUrl = fullurl;
			HttpResult res = new HttpResult();
			string json = wc.DownloadString(fullurl);
			Dictionary<string, string> htmlAttributes = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

			if (htmlAttributes.ContainsKey("key")) res.Key = htmlAttributes["key"];
			if (htmlAttributes.ContainsKey("status")) res.Caption = htmlAttributes["status"];
			if (htmlAttributes.ContainsKey("totalSteps")) res.Total = int.Parse(htmlAttributes["totalSteps"]);
			if (htmlAttributes.ContainsKey("step")) res.Value = int.Parse(htmlAttributes["step"]);
			if (htmlAttributes.ContainsKey("totalSlices")) res.SubTotal = int.Parse(htmlAttributes["totalSlices"]);
			if (htmlAttributes.ContainsKey("extra")) res.Extra = JsonConvert.DeserializeObject<Dictionary<string, string>>(htmlAttributes["extra"]);
			if (htmlAttributes.ContainsKey("slice")) res.SubValue = int.Parse(htmlAttributes["slice"]);
			if (htmlAttributes.ContainsKey("done")) res.Completed = htmlAttributes["done"] == "true";
			return res;
		}
		public static string ConfigurationGet(string key, string defaultValue = null)
		{
			object config = System.Configuration.ConfigurationManager.GetSection("medea");

			if (config == null)
				return defaultValue;

			NameValueCollection properties = config as NameValueCollection;
			if (properties == null)
				return defaultValue;

			string ret = properties[key];
			if (string.IsNullOrEmpty(ret))
				return defaultValue;
			else
				return ret;
		}

		void action_ProgressChanged(HttpResult res)
		{
      while(panProgress.Created == false)
      {
          panProgress.CreateControl();
          Application.DoEvents();
      }
      panProgress.Invoke(new ProgressDelegate(updateProgress),
					new object[] { res.Value, res.Total, res.Caption, res.SubValue, res.SubTotal });
		}

		private void updateProgress(int current, int total, string caption, int subCurrent, int subTotal)
		{
			panProgress.updateProgress(current, total, caption, subCurrent, subTotal);
		}

		private void actionCompleted(object o, EventArgs args)
		{
			WaitCursor.Reset();
			ret = true;
			panProgress.Stop();
		}
		private void showError(object o, EventArgs args)
		{
			Exception e = o as Exception;
			WaitCursor.Reset();
			UI.ShowError(panProgress, e, (showUrlOnError ? lastUrl : null));
			ret = false;
			panProgress.Stop();
		}
	}
}
