/*
*    Poblaciones - Plataforma abierta de datos espaciales de población.
*    Copyright (C) 2018-2024. Consejo Nacional de Investigaciones Científicas y Técnicas (CONICET)
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
﻿using medea.controls;
using System.Windows.Forms;
using medea.entities;
using System.Linq;
using System.Collections.Generic;
using medea.common;

namespace medea.winApp
{
	public partial class frmPickCoverage : frmOkCancel
	{
		ClippingRegionItem current;

		public frmPickCoverage()
		{
			InitializeComponent();
		}

		public void LoadData(ClippingRegionItem coverage)
		{
			using (new WaitCursor())
			{
				current = coverage;
				// initializa el arbol
				TreeNode t = CreateNode(UI.CurrentCountry);
				AddChildren(t);
				t.Expand();
			}
		}

		private TreeNode CreateNode(ClippingRegionItem item, TreeNode parent = null)
		{
			TreeNode t = new TreeNode(item.Caption);
			t.Tag = item;

			if (parent == null)
				tvRegions.Nodes.Add(t);
			else
				parent.Nodes.Add(t);
			return t;
		}
		private TreeNode CreateNode(IIdentifiable item, TreeNode parent = null)
		{
			TreeNode t = new TreeNode(item.ToString());
			t.Tag = item;
			t.Collapse();
			if (parent == null)
				tvRegions.Nodes.Add(t);
			else
				parent.Nodes.Add(t);
			return t;
		}

		private void AddChildren(TreeNode node)
		{
			using (new WaitCursor())
			{
				if (node.Tag is ClippingRegionItem)
				{
					ClippingRegionItem clippingRegionItem = node.Tag as ClippingRegionItem;
					if (clippingRegionItem.ClippingRegion.Parent == null)
					{
						// es country
						var regions = UI.GetItems<ClippingRegion>().Where(x => x.Country == UI.CurrentCountry && x.Parent.Parent == null).OrderBy(x => x.Caption).ToList();
						foreach (var r in regions)
							CreateNode(r, node);
					}
					else
					{
						if (clippingRegionItem.ClippingRegion.Children.Count > 1)
						{
							foreach (var r in clippingRegionItem.ClippingRegion.Children)
								CreateNode(r, node);
						}
						else
						{
							foreach (var r in clippingRegionItem.Children)
								CreateNode(r, node);
						}
					}
				}
				else if (node.Tag is ClippingRegion)
				{
					ClippingRegion clippingRegion = node.Tag as ClippingRegion;
					var items = UI.GetItems<ClippingRegionItem>().Where(x => x.ClippingRegion == clippingRegion
								&& x.Parent == (node.Parent.Tag as ClippingRegionItem)).OrderBy(x => x.Caption).ToList();
					foreach (var r in items)
						CreateNode(r, node);
				}
			}
		}
		protected override void OnSubmit()
		{
			if (tvRegions.SelectedNode == null || (tvRegions.SelectedNode.Tag is ClippingRegionItem) == false)
				throw new MessageException("Debe indicar una región para continuar.");
			
			DialogResult = DialogResult.OK;
			Close();
		}

		
		public ClippingRegionItem GetSelected()
		{
			return tvRegions.SelectedNode.Tag as ClippingRegionItem;
		}

		private void tvRegions_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			foreach (TreeNode node in e.Node.Nodes)
			{
				if (node.Nodes.Count == 0)
					AddChildren(node);
			}
		}
	}
}
