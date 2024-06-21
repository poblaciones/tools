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
using medea.common;
using medea.entities;
using System.Collections.Generic;

namespace medea.controls
{
	public static class UI
	{
		private static Dictionary<string, object> TableCache = new Dictionary<string, object>();

		public static ClippingRegionItem CurrentCountry;
		private static User currentUser;
		public static string CurrentUserEmail;

		public static User CurrentUser {
			get {
				if (currentUser == null)
				{
					using (new WaitCursor())
					{
						var q = context.Data.Session.CreateQuery("select u from User u where u.Email = :p1");
						q.SetString("p1", CurrentUserEmail);
					currentUser = q.UniqueResult<User>();
					}
				}
				return currentUser;
			}
			set { currentUser = value;
				context.Environment.CurrentUser = value;
			}
		}

		public static IQueryable<T> GetItems<T>()
		{
			using (new WaitCursor())
			{
				return context.Data.Session.Query<T>();
			}
		}

		public static T GetById<T>(int id) where T : IIdentifiable
		{
			using (new WaitCursor())
			{
				return context.Data.Session.GetById<T>(id);
			}
		}

		public static Institution GetPublicLocalInstitution()
		{
			return UI.GetItems<Institution>().Where(x => x.IsPublicDataEditor).FirstOrDefault();
		}

		public static Institution GetLocalInstitution()
		{
			return UI.GetItems<Institution>().Where(x => x.IsPublicDataEditor).FirstOrDefault();
		}

		public static void ShowError(Form f, Exception e, string url = null)
		{
			WaitCursor.Reset();
			if (e is MessageException)
			{
                Windows7Taskbar.SetStatus(f, ProgressBarState.Pause);
                ShowInfoMessage(f, e.Message);
                Windows7Taskbar.SetStatus(f, ProgressBarState.Normal);
                return;
			}
			string err = "";
			if (url != null)
				err = "URL: " + url + Environment.NewLine;
			err += "Se ha producido un error: " + e.Message;

			if (e.InnerException != null)
				err += "\n\nInnerException: " + e.InnerException.Message;
#if DEBUG

			err += "\n\n" + e.ToString();
			if (e.InnerException != null)
				err += "\n\nInnerException: " + e.InnerException.ToString();
#endif
			Windows7Taskbar.SetStatus(f, ProgressBarState.Error);
			MessageBox.Show(f, err, "Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            Windows7Taskbar.SetStatus(f, ProgressBarState.Normal);
        }

		public static IList<Source> GetSources()
		{
			var list = UI.GetItems<Source>().OrderBy(x => x.Caption).ThenBy(x => x.Version).ToList();

			return list;
		}
		public static IList<Source> GetPublicSources()
		{
			var list = UI.GetItems<Source>().OrderBy(x => x.Caption).ThenBy(x => x.Version).ToList();
			List<Source> ret = new List<Source>();
			foreach(var item in list.FindAll(x => x.IsGlobal))
			{
				ret.Add(item);
			}
			return ret;
		}

		public static bool Confirm(Form f, string text)
		{
			return (MessageBox.Show(f, text, "Atención",
				MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation)
					== DialogResult.OK);
		}

		public static bool ConfirmDelete(Form f)
		{
			return Confirm(f, "Se procederá a eliminar el elemento seleccionado.\n\n¿Está seguro de que desea hacer esto?");
		}
		public static bool ConfirmDeleteRecursive(Form f)
		{
			return Confirm(f, "Se procederá a eliminar el elemento seleccionado y todos los que dependen de éste."
				+ System.Environment.NewLine + System.Environment.NewLine
				+ "¿Está seguro de que desea hacer esto?");
		}

		public static void Delete(object item)
		{
			using (new WaitCursor())
			{
				context.Data.Session.Delete(item);
				context.Data.Session.Flush();
			}
		}

		public static void ShowInfoMessage(Form f, string p)
		{
			MessageBox.Show(f, p, "Listo",
				MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		public static void ShowMessage(Form f, string p)
		{
			MessageBox.Show(f, p, "Atención",
				MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
		}

		public static void ResetBaseDataSets()
		{
			if (TableCache.ContainsKey("dataset"))
				TableCache.Remove("dataset");
		}
		
		public static IList<Gradient> GetGradients()
		{
			return UI.GetItems<Gradient>().Where(x => x.Country == UI.CurrentCountry).OrderBy(x => x.Caption).ToList();
		}

		public static IList<Geography> GetGeographies()
		{
			return UI.GetItems<Geography>().Where(x => x.Country == UI.CurrentCountry).OrderBy(x => x.Caption).ToList();
		}
		public static IList<Institution> GetInstitutions()
		{
			return UI.GetItems<Institution>().OrderBy(x => x.Caption).ToList();
		}
		public static IList<Institution> GetPublicInstitutions()
		{
			List<Institution> ret = new List<Institution>();
			var items = UI.GetItems<Institution>().OrderBy(x => x.Caption).ToList();
			foreach (var item in items)
				ret.Add(item);
			return ret;
		}
		public static string format(int p)
		{
			return string.Format("{0:n0}", p);
		}


		public static List<ClippingRegionItem> GetCountries()
		{
			if (TableCache.ContainsKey("country") == false)
				TableCache["country"] = UI.GetItems<ClippingRegionItem>().Where(x => x.ClippingRegion.Parent == null).OrderBy(x => x.Caption).ToList();
			return TableCache["country"] as List<ClippingRegionItem>;
		}
		public static List<User> GetUsers()
		{
			if (TableCache.ContainsKey("user") == false)
				TableCache["user"] = UI.GetItems<User>().Where(x => x.Deleted == false)
															.OrderBy(x => x.Email).ToList();

			return TableCache["user"] as List<User>;
		}

		public static string GetCountLegend(ListView.ListViewItemCollection items)
		{
			if (items.Count == 1)
				return "1 item.";
			else
				return items.Count.ToString() + " ítems.";
		}

		public static string SingleLineSanitize(string cad)
		{
			if (cad == null) return null;
			return cad.Replace('\t', ' ').Replace('\r', ' ').Replace('\n', ' ');
		}
	}
}
