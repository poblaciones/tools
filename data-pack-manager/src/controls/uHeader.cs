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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace medea.controls
{
	public partial class uHeader : UserControl
	{
		public uHeader()
		{
			InitializeComponent();
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Bindable(false)]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Bindable(false)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				label1.Text = value;
				base.Text = value;
				Invalidate();
			}
		}

		private void uHeader_Resize(object sender, EventArgs e)
		{
			Height = 26;
			Invalidate();
		}
		

		private void uHeader_Paint(object sender, PaintEventArgs e)
		{
			//Rectangle recTitleBar = new Rectangle(new Point(0, 0), new Size(this.ClientRectangle.Width, 20));
			Color c2 = Color.LightGray;
			//Color c1 = Color.White;
			Color c1 = Color.WhiteSmoke;
			Color c3 = Color.FromArgb(180, 180, 150);
			Graphics g = e.Graphics;
			
			g.FillRectangle(SystemBrushes.Control, ClientRectangle);

			var cr = new Rectangle(0, 3, ClientRectangle.Width - 1,
																ClientRectangle.Height - 7);
			
			LinearGradientBrush lgbTitleBar = new LinearGradientBrush(new Point(0, 0),
					new Point(cr.Width, 0), c2, c1);
			g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
			Font fontTitleBar = new Font("Arial", 9.5F, FontStyle.Regular);

			g.FillRectangle(lgbTitleBar, cr);

			g.DrawRectangle(new Pen(c3), cr);
			g.DrawString(Text, fontTitleBar, Brushes.Black, new PointF(3, 6));
		}
	}
}
