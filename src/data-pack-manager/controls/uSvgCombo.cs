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
using System.Drawing;
using Svg;

namespace medea.controls
{
	public partial class uSvgCombo : ComboBox
	{

		public uSvgCombo()
		{
			this.DrawMode = DrawMode.OwnerDrawFixed;
			this.DrawItem += USvgCombo_DrawItem;
			this.Font = new Font(this.Font.FontFamily, 11F);
		}
		public void SetSelectedByTag(string tag)
		{
			if (tag == null)
			{
				this.SelectedIndex = -1;
				return;
			}
			for(int n = 0; n < this.Items.Count; n++)
			{
				uSvgComboItem item = this.Items[n] as uSvgComboItem;
				if (item.Tag == tag)
				{
					this.SelectedIndex = n;
					return;
				}
			}
		}
		public uSvgComboItem GetSelectedItem()
		{
			if (this.SelectedIndex == -1)
				return null;
			else
				return this.Items[this.SelectedIndex] as uSvgComboItem;
		}


		public string GetSelectedItemTag()
		{
			if (this.SelectedIndex == -1)
				return null;
			else
				return (this.Items[this.SelectedIndex] as uSvgComboItem).Tag;
		}
		private void USvgCombo_DrawItem(object sender, DrawItemEventArgs e)
		{
			uSvgComboItem selected = null;
			if (e.Index != -1)
			{
				selected = this.Items[e.Index] as uSvgComboItem;
				DrawSvg(selected.Path, e, selected.Name);
			}
			else
			{
				DrawSvg(null, e, "");
			}
		}

		private void DrawSvg(string path, DrawItemEventArgs e, string name)
		{
			return;
			Brush backColor = SystemBrushes.Window;
			Pen foreColor = SystemPens.WindowText;
			Brush foreColorBrush = SystemBrushes.WindowText;
			if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
			{
				backColor = SystemBrushes.Highlight;
				foreColor = SystemPens.HighlightText;
				foreColorBrush = SystemBrushes.HighlightText;
			}
			e.Graphics.FillRectangle(backColor, e.Bounds);
			if (path != null)
			{
				string pre = "<svg xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\"><path d=\"";
				string post = "\" style=\"fill-opacity: 0.7; stroke-opacity: 0.7; stroke-width: 0; stroke:\" + ColorToHexa(foreColor.Color)  + \"; fill:\" + ColorToHexa(foreColor.Color) +\";\"/></svg>";
				string svg = pre +
					path
					+ post;
				System.Xml.XmlDocument docXml = new System.Xml.XmlDocument();
				docXml.LoadXml(svg);
				SvgDocument doc = SvgDocument.Open(docXml);
				Bitmap bit = doc.Draw(90, 90);
				e.Graphics.DrawImage(bit, e.Bounds.Left + 2, e.Bounds.Top + 1);
			}
			const int TEXTLEFTMARGIN = 21;
			const int TEXTTOPMARGIN = 3;
			e.Graphics.DrawString(name, new Font(this.Font.FontFamily, 8.5F), foreColorBrush, e.Bounds.Left + TEXTLEFTMARGIN, e.Bounds.Top + TEXTTOPMARGIN);

			if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
			{
				ControlPaint.DrawFocusRectangle(e.Graphics, e.Bounds);
			}
		}

		public string ColorToHexa(Color color)
		{
			var s = ColorTranslator.ToHtml(Color.FromArgb(color.R, color.G, color.B));
			return s;
		}
	}
}
