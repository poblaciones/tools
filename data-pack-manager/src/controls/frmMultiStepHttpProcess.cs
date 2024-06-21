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
using medea.common;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace medea.controls
{
	public partial class frmMultiStepHttpProcess : Form
	{
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    static extern IntPtr GetParent(IntPtr hWnd);
    delegate void ProgressDelegate(int current, int total);
    IntPtr? taskHwnd = null;

		public frmMultiStepHttpProcess()
		{
            InitializeComponent();
		}

		public void Start()
		{
//			Location = new Point(Parent.ClientRectangle.Width / 2 -
//				Width / 2, Parent.ClientRectangle.Height / 2 - Height / 2);
			PictureBox sel;
			if (new Random().Next(20) == 10)
				sel = pictureBox2;
			else
				sel = pictureBox1;
			HidePictures();
			setValues(sel, true);
		}
		private const int CP_NOCLOSE_BUTTON = 0x200;
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams myCp = base.CreateParams;
				myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
				return myCp;
			}
		}
		private void HidePictures()
		{
			setValues(pictureBox1, false);
			setValues(pictureBox2, false);
		}

		private void setValues(PictureBox pi, bool val)
		{
			pi.Enabled = val;
			pi.Visible = val;
		}

		internal void updateProgress(int current, int total, string caption)
		{
			caption += "...";
			if (label1.Text != caption)
			{
				label1.Text = caption;
				return;
			}
			if (progressBar1.Maximum != total)
				progressBar1.Maximum = total;
			progressBar1.Value = current;
			if (progressBar1.Visible == false)
				progressBar1.Visible = true;

            if (taskHwnd == null)
                taskHwnd = Windows7Taskbar.GetTaskBarForm(this);

            if (taskHwnd.Value != IntPtr.Zero)
                Windows7Taskbar.SetProgressValue(taskHwnd.Value, (uint) current, (uint) total);
        }
        public void Stop()
        {
            Windows7Taskbar.SetStatus(this, ProgressBarState.Normal);
            this.Close();
        }

    }
}