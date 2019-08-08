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
﻿using System;
using System.Windows.Forms;
namespace medea.common
{
	public class KeepSelectedTree : IDisposable
	{
		TreeView _tw;
		int? id;

		public KeepSelectedTree(TreeView lw)
		{
			_tw = lw;
			if (lw.SelectedNode != null)
			{
				if (lw.SelectedNode.Tag is IIdentifiable)
					id = (lw.SelectedNode.Tag as IIdentifiable).Id;
			}
		}
		public void Dispose()
		{
			if (_tw.Nodes.Count == 0)
				return;
			if (id.HasValue)
			{
				SetSelectedRecursive(_tw.Nodes);
			}
		}

		private bool SetSelectedRecursive(TreeNodeCollection treeNodeCollection)
		{
			foreach (TreeNode item in treeNodeCollection)
			{
				if (item.Tag is IIdentifiable &&
					id.Value == (item.Tag as IIdentifiable).Id)
				{
					_tw.SelectedNode = item;
					return true;
				}
				if (SetSelectedRecursive(item.Nodes)) return true;
			}
			return false;
		}
	}
}
