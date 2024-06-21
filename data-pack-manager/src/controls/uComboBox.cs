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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;
using medea.common;
using medea.entities;

namespace medea.controls
{
	public partial class uComboBox : ComboBox
	{
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public List<object> ItemsTyped { get; set; }
		public bool AutoSizeByContent = true;
		public bool Optional { get; set; }

		public bool HasSelectedItem
		{
			get
			{
				if (Items.Count == 0)
					return false;
				if (Optional == false)
					return true;
				if (SelectedIndex > 0)
					return true;

				return false;
			}
		}


		public uComboBox()
		{
			Optional = true;
			InitializeComponent();
			ItemsTyped = new List<object>();
		}

		public void Clear()
		{
			Items.Clear();
			ItemsTyped.Clear();
		}

		/// <summary>
		/// Llena el combo con datos de un query de entidades recursivas (IRecursive).
		/// </summary>
		/// <typeparam name="T">El tipo de la entidad a llenar</typeparam>
		/// <param name="items">Los ítems traidos del query</param>
		/// <param name="Optional">Si el combo es opcional pone un ítem vacío al comienzo.</param>
		public void FillRecursive<T>(IEnumerable<T> items)
			where T : ActiveBaseEntity<T>, IIdentifiable, IRecursive<T>
		{
			Clear();

			var res = new Dictionary<int, string>();
			TreeItem<T>.MakeDictionary(items.GenerateTree(), res);
			ValueMember = "Text";
			if (Optional)
			{
				var opt = new ComboItem<T>(true);
				Items.Add(opt);
				ItemsTyped.Add(opt);
			}
			foreach (var item in res)
			{
				T tag = items.Where(x => x.Id == item.Key).First();
				string key = item.Key.ToString();
				string text = item.Value;
				AddItem<T>(tag, key, text);
			}

			//TODO: Quizás esto sea mejor siempre.
			if (Optional == false && Items.Count > 0)
				SelectedIndex = 0;

			if (AutoSizeByContent)
				SetDropDownWidth();
		}

		public ComboItem<T> AddItem<T>(T tag, string key, string text) where T : ActiveBaseEntity<T>, IIdentifiable
		{
			ComboItem<T> local = new ComboItem<T>();
			local.Key = key;
			local.Tag = tag;
			local.Text = text;
			Items.Add(local);
			ItemsTyped.Add(local);
			return local;
		}

		/// <summary>
		/// Llena un combo con datos de un query.
		/// </summary>
		/// <typeparam name="T">El tipo de la entidad a llenar</typeparam>
		/// <param name="items">Los ítems traidos del query</param>
		/// <param name="field">El campo de la entidad que va a mostrar el combo ej x => x.Name</param>
		/// <param name="Optional">Si el combo es opcional pone un ítem vacío al comienzo.</param>
		public void Fill<T>(IEnumerable<T> items, Expression<Func<T, object>> field)
			where T : ActiveBaseEntity<T>, IIdentifiable, new()
		{
			ClearItems();
			doAppend<T>(items, field, Optional);
		}

		public void ClearItems()
		{
			Items.Clear();
			ItemsTyped.Clear();
		}

		/// <summary>
		/// Llena un combo con datos de un query.
		/// </summary>
		/// <typeparam name="T">El tipo de la entidad a llenar</typeparam>
		/// <param name="items">Los ítems traidos del query</param>
		/// <param name="field">El campo de la entidad que va a mostrar el combo ej x => x.Name</param>
		/// <param name="Optional">Si el combo es opcional pone un ítem vacío al comienzo.</param>
		public void Append<T>(IEnumerable<T> items, Expression<Func<T, object>> field)
			where T : ActiveBaseEntity<T>, IIdentifiable, new()
		{
			doAppend(items, field, false);
		}

		private void doAppend<T>(IEnumerable<T> items, Expression<Func<T, object>> field, bool optional)
			where T : ActiveBaseEntity<T>, IIdentifiable, new()
		{

			ValueMember = "Text";
			if (optional)
			{
				var opt = new ComboItem<T>(true);
				Items.Add(opt);
				ItemsTyped.Add(opt);
			}

			var fname = context.Metadata<T>.GetField(field);
			foreach (var item in items)
			{
				var local = new ComboItem<T>();
				local.Key = item.Id.ToString();
				local.Text = item.GetType().GetProperty(fname).GetValue(item, new object[0]).ToString();
				local.Tag = item;
				Items.Add(local);
				ItemsTyped.Add(local);
			}

			if (Optional == false && Items.Count > 0)
				SelectedIndex = 0;
			if (AutoSizeByContent)
				SetDropDownWidth();
		}

		/// <summary>
		/// Devuelve el valor del combo cuando NO fue llenado con entidades ActiveBaseEntity.
		/// </summary>
		/// <returns>El texto del ítem seleccionado o ""</returns>
		public string GetValue()
		{
			if (HasSelectedItem)
				return SelectedItem.ToString();

			return "";
		}

		/// <summary>
		/// Devuelve el objeto tipado seleccionado del combo o null.
		/// El combo debe haber sido llenado con Fill o FillRecursive.
		/// </summary>
		/// <typeparam name="T">El tipo de objetos con que se llenó el combo.</typeparam>
		/// <returns>El objeto seleccionado.</returns>
		public T GetSelectedItem<T>()
			where T : ActiveBaseEntity<T>
		{
			if (HasSelectedItem == false)
				return null;
			return ((ComboItem<T>)ItemsTyped[SelectedIndex]).Tag;
		}

		public ComboItem<T> GetSelectedComboItem<T>()
			where T : ActiveBaseEntity<T>
		{
			if (HasSelectedItem == false)
				return null;
			return (ComboItem<T>)ItemsTyped[SelectedIndex];
		}

		/// <summary>
		/// Selecciona el ítem que contenga el texto (case insensitive).
		/// </summary>
		/// <param name="text">El texto a comparar</param>
		public bool SelectContainsText(string text)
		{
			if (text == null)
				text = "";

			//Busca en ítems agregados con Fill...
			var index = ItemsTyped.FindIndex(x => x.ToString().ToLower().Contains(text.ToLower()));

			if (index == -1)
			{
				//Busca en ítems agregados normalmente
				for (int i = 0; i < Items.Count; i++)
				{
					if (Items[i].ToString().ToLower().Contains(text.ToLower()))
					{
						index = i;
						break;
					}
				}
			}

			if (index > -1)
			{
				SelectedIndex = index;
				return true;
			}
			else
				return false;
		}
		/// <summary>
		/// Selecciona el ítems tipado si el combo se llenó con Fill o FillRecursive.
		/// </summary>
		public void SelectByText<T>(string text)
			where T : ActiveBaseEntity<T>, IIdentifiable
		{
			var index = -1;
			if (text != null)
				index = ItemsTyped.FindIndex(x => ((ComboItem<T>)x).Text == text);

			if (index > -1)
				SelectedIndex = index;
		}
		/// <summary>
		/// Selecciona el ítems tipado si el combo se llenó con Fill o FillRecursive.
		/// </summary>
		public void SelectItem<T>(T entity)
			where T : ActiveBaseEntity<T>, IIdentifiable
		{
			var index = -1;
			if (entity != null)
				index = ItemsTyped.FindIndex(x => AreEqual((ComboItem<T>)x, entity));

			if (index > -1)
				SelectedIndex = index;
		}

		private static bool AreEqual<T>(ComboItem<T> item, T entity)
			where T : ActiveBaseEntity<T>, IIdentifiable
		{
			if (item.Tag == null && entity == null)
				return true;
			else if (item.Tag != null && entity == null)
				return false;
			else if (item.Tag == null && entity != null)
				return false;
			else if (item.Tag.Id == entity.Id)
				return true;
			else
				return false;
		}

		/// <summary>
		/// Setea el ancho de la caja de dropdown para que entre el ítem con texto más largo.
		/// </summary>
		public void SetDropDownWidth()
		{
			int max = 0;
			foreach (var obj in Items)
			{
				int temp = TextRenderer.MeasureText(obj.ToString(), Font).Width;
				if (temp > max)
					max = temp;
			}
			if (Width < max)
				DropDownWidth = max;
		}

		public void RemoveItem<T>(T dataset) where T : ActiveBaseEntity<T>, IIdentifiable
		{
			for(int n = 0; n < Items.Count; n++)
				if (AreEqual((ComboItem<T>) Items[n], dataset))
				{
					Items.RemoveAt(n);
					return;
				}
		}
	}
}
