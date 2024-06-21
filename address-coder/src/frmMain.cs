using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace addressCoder
{
	public partial class frmMain : Form
	{
		public frmMain()
		{
			InitializeComponent();
			txtKey.Text = ConfigurationManager.AppSettings["GoogleGeocodingKey"] as string;
		}

		private void cmdStart_Click(object sender, EventArgs e)
		{
			try
			{
				geoCoder geoCoder = new geoCoder();
				geoCoder.API_KEY = txtKey.Text;
				if (txtKey.Text.Length == 0)
				{
					MessageBox.Show(this, "Debe indicar una key de geocoding");
					return;
				}
				int n = 0;
				foreach (ListViewItem i in lstView.Items)
				{
					n++;
					this.Text = ((int)(n * 100 / lstView.Items.Count)).ToString() + "%";
					Application.DoEvents();
					string address = i.Text;
					dynamic res = geoCoder.Get(address);
					if (res.status == "OK" && res.results.Count > 0)
					{
						var first = res.results[0];
						var typeit = first.types.ToString();
						if (res.results.Count > 1 && !typeit.Contains("street_address") && !typeit.Contains("premise")
										&& !typeit.Contains("intersection"))
						{
							first = res.results[1];
							typeit = first.types.ToString();
						}
						var text = i.Text;
						i.SubItems.Clear();
						i.Text = text;
						if (typeit.Contains("street_address") || typeit.Contains("premise")
										|| typeit.Contains("intersection"))
						{
							i.SubItems.Add(first.geometry.location.lat.ToString());
							i.SubItems.Add(first.geometry.location.lng.ToString());
							i.SubItems.Add(first.formatted_address.ToString());
						}

						i.EnsureVisible();
					}
				}
			}
			catch (Exception ex)
			{
				if (ex.Message == "You have exceeded your rate-limit for this API.")
				{
					timer1.Enabled = true;
					this.Text = "Waiting...";
				}
				else
					MessageBox.Show(this, ex.Message);
			}
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void btnPaste_Click(object sender, EventArgs e)
		{
			lstView.Items.Clear();
			string allText = Clipboard.GetText();
			string[] args = new string[] { Environment.NewLine };
			string[] lines = allText.Split(args, StringSplitOptions.RemoveEmptyEntries);
			foreach (string line in lines)
				lstView.Items.Add(line);
		}

		private void btnCopy_Click(object sender, EventArgs e)
		{
			CopyListView(lstView);
		}

		public void CopyListView(ListView list)
    {
        StringBuilder sb = new StringBuilder();
				foreach(ColumnHeader col in list.Columns)
					sb.Append(col.Text +"\t");
        sb.AppendLine();
        foreach (var item in list.Items)
        {
            ListViewItem l = item as ListViewItem;
            if (l != null)
                foreach (ListViewItem.ListViewSubItem sub in l.SubItems)
                    sb.Append(sub.Text+"\t");
            sb.AppendLine();
        }
			Clipboard.Clear();
        Clipboard.SetDataObject(sb.ToString());
    }

		private void timer1_Tick(object sender, EventArgs e)
		{
			System.Threading.Thread.Sleep(5000);
			timer1.Enabled = false;

			cmdStart.PerformClick();
		}
	}
}
