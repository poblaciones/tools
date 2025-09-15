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
﻿using System.Collections.Generic;
using System.Windows.Forms;

namespace medea.common
{
	public class TreeItem<T> where T: ActiveBaseEntity<T>, IIdentifiable, IRecursive<T>
	{
		public T Item { get; set; }
		public IEnumerable<TreeItem<T>> Children { get; set; }

		public static void MakeNodeTree(IEnumerable<TreeItem<T>> items, TreeNode node, Dictionary<int?, int> relationsCount = null)
		{
			foreach (var item in items)
			{
				string itemText  = item.Item.GetCaption();
				var version = item.Item as IVersionable;
				if (version != null)
				{
					if (version.Version != null)
						itemText += " [" + version.Version + "]";
				}

				var reg = item.Item as IIdentifiable;
				if (relationsCount != null && reg != null && reg.Id.HasValue)
				{
					if (relationsCount.ContainsKey(reg.Id.Value))
						itemText += " (" + relationsCount[reg.Id.Value].ToString() + ")";
				}
				var childNode = new TreeNode(itemText);
				childNode.Tag = item.Item;


				node.Nodes.Add(childNode);
				MakeNodeTree(item.Children, childNode, relationsCount);
			}
		}

		public static void MakeDictionary(IEnumerable<TreeItem<T>> items, Dictionary<int, string> list, int deep = 0)
		{
			foreach (var item in items)
			{
				string text = item.Item.GetCaption();
				IVersionable v = item.Item as IVersionable;
				if (v != null && v.Version != null)
				{
					text += " [" + v.Version + "]";
				}
				list.Add(item.Item.Id.GetValueOrDefault(), new string(' ', deep * 3) + text);
				MakeDictionary(item.Children, list, deep + 1);
			}
		}
	}
	
}
