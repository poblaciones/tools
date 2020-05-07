using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stress_tester
{
	public partial class frmMain : Form
	{
		SynchronizedInt openClients = new SynchronizedInt();
		bool isRestoring;

		public frmMain()
		{
			InitializeComponent();
			lw.Columns[0].Width = 300;
		}

		private void FormToContext()
		{
			if (isRestoring)
				return;
			Current.Context.Clients = (int) numClients.Value;
			Current.Context.Loops = (int) numLoops.Value;
			Current.Context.ParallelRequests = (int) numParalells.Value;
			Current.Context.Retries = (int) numRetries.Value;
			Current.Context.RetryMilliseconds = (int) numRetryMs.Value;
			Current.Context.RandomOffsets = chRandom.Checked;

			Current.Context.Filter = txtFilter.Text;
			Current.Context.UseFilter = chFilter.Checked;
			if (chAutoSave.Checked)
					AutoSave();
		}

		private void ContextToForm()
		{
			isRestoring = true;
			numClients.Value = Current.Context.Clients;
			numLoops.Value = Current.Context.Loops;
			numParalells.Value = Current.Context.ParallelRequests;
			numRetries.Value = Current.Context.Retries;
			numRetryMs.Value = Current.Context.RetryMilliseconds;
			txtFilter.Text = Current.Context.Filter;
			chFilter.Checked = Current.Context.UseFilter;
			chRandom.Checked = Current.Context.RandomOffsets;
			isRestoring = false;
		}

		private void ResultsToForm()
		{
			Results r = Current.Context.Results.GetCopy();
			lblHits.Text = r.Hits.Get().ToString();
			lblErrors.Text = r.Errors.Get().ToString();
			lblRetries.Text = r.Retries.Get().ToString();
			lblTotalTime.Text = formatSecs(r.TotalTime.Get());
			lblExecuting.Text = r.Executing.Get().ToString();
			lblMaxExecuting.Text = r.MaxExecuting.Get().ToString();
			lblTotalBytes.Text = r.TotalBytes.Get().ToString();
			if (r.Hits.Get() > 0)
			{
				lblAvgTime.Text = ((int)(r.TotalTime.Get() / r.Hits.Get())).ToString() + " ms";
			}
			else
			{
				lblAvgTime.Text = "0";
			}
			ProcessResponsesToListView(r.Responses);
		}

		private string formatSecs(double ms)
		{
			return (((int)(ms / 100)) / 10F).ToString() + " sec";
		}

		private void ClearListview()
		{
			foreach(ListViewItem item in lw.Items)
			{
				UpdateListViewItem(item, "", "", "", "");
			}
		}
		private void ProcessResponsesToListView(ConcurrentBag<Response> responses)
		{
			foreach (var r in responses.ToArray())
			{
				foreach(ListViewItem item in lw.Items)
				{
					if ((item.Tag as Request).Id == r.Id)
					{
						UpdateListViewItem(item, formatMs(r.StartTime), ((int)r.Ellapsed).ToString(), r.Code.ToString(), r.Length.ToString() );
					}
				}
			}
		}

		private void UpdateListViewItem(ListViewItem item, string start, string ellapsed, string code, string length)
		{
			// effective
			item.SubItems[2].Text = start;
			// ellapsed
			item.SubItems[3].Text = ellapsed;
			// response
			item.SubItems[4].Text = code;
			// length
			item.SubItems[5].Text = length;
		}

		private void numClients_ValueChanged(object sender, EventArgs e)
		{
			FormToContext();
		}

		private void numParalells_ValueChanged(object sender, EventArgs e)
		{
			FormToContext();
		}

		private void numLoops_ValueChanged(object sender, EventArgs e)
		{
			FormToContext();
		}

		private void nmRetries_ValueChanged(object sender, EventArgs e)
		{
			FormToContext();
		}

		private void cmdRun_Click(object sender, EventArgs e)
		{
			// Inicializa
			Current.Context.Results = new Results();
			ResultsToForm();
			List<Client> clients = new List<Client>();
			for (int n = 0; n < Current.Context.Clients; n++)
				clients.Add(new Client());
			openClients.Set(clients.Count);
			ClearListview();
			// Los completa
			foreach (var client in clients)
			{
				client.Initialize(Current.Context.FilteredList);
				if (Current.Context.RandomOffsets)
				{
					double offset = new Random().NextDouble() * Current.Context.ScriptTime;
					client.startDateOffset = TimeSpan.FromMilliseconds(offset);
				}
				client.Finished += Client_Finished;
			}
			// Los inicia
			foreach (var client in clients)
				client.Start();
			// Listo
			EnableButtons(false);
		}

		private void Client_Finished(object sender, EventArgs e)
		{
			openClients.Decrement();
			if (openClients.Get() == 0)
			{
				if (this.InvokeRequired)
					this.Invoke(new MethodInvoker(() => { Finish(); }));
				else
					Finish();
			}
		}
		private void Finish()
		{
			EnableButtons(true);
			ResultsToForm();
			if (chAutoSave.Checked)
				AutoSave();
		}

		private void EnableButtons(bool v)
		{
			cmdOpen.Enabled = v;
			cmdSave.Enabled = v;
			cmdRun.Enabled = v;
			grpInput.Enabled = v;
			btnDelete.Enabled = v;
			timRefresh.Enabled = !v;
		}

		private void txtFilter_TextChanged(object sender, EventArgs e)
		{
			FormToContext();
			Current.Context.UpdateStart();
			ListToListView();
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			FormToContext();
			Current.Context.UpdateStart();
			ListToListView();
		}

		private void btnImport_Click(object sender, EventArgs e)
		{
			OpenFileDialog od = new OpenFileDialog();
			od.DefaultExt = "har";
			od.Filter = "Har files (*.har)|*.har";

			if (od.ShowDialog(this) == DialogResult.Cancel)
				return;
			Current.Context.List = HarParser.ParseRequests(od.FileName);
			Current.Context.UpdateStart();

			ListToListView();
			if (chAutoSave.Checked)
				AutoSave();
		}

		private void ListToListView()
		{
			// agrega items
			lw.Items.Clear();
			foreach(var r in Current.Context.FilteredList)
			{
				string f;
				if (Current.Context.UseFilter)
					f = r.Url.Replace(Current.Context.Filter, "");
				else
					f = r.Url;
				ListViewItem li = new ListViewItem(f);
				li.Tag = r;
				li.SubItems.Add(formatMs(r.MillisecondsFromStart));
				li.SubItems.Add(""); 
				li.SubItems.Add(""); 
				li.SubItems.Add(""); 
				li.SubItems.Add(""); 
				lw.Items.Add(li);
			}
			// actualiza totales
			lblScriptTime.Text = formatMs(Current.Context.ScriptTime);
			lblCount.Text = Current.Context.FilteredList.Count.ToString();
		}

		private string formatMs(double ms)
		{
			return TimeSpan.FromMilliseconds(((int)(ms / 100)) * 100).ToString(@"hh\:mm\:ss");
			//+ "." + ((int) ((ms % 1000) / 10));
		}

		private void cmdOpen_Click(object sender, EventArgs e)
		{
			OpenFileDialog od = new OpenFileDialog();
			od.DefaultExt = "stress";
			od.Filter = "Stress tests (*.stress)|*.stress";
			if (od.ShowDialog(this) == DialogResult.Cancel)
				return;
			LoadContext(od.FileName);

			if (chAutoSave.Checked)
				AutoSave();
		}

		private void LoadContext(string filename)
		{
			Current.Context = Serializator.FromFile<Context>(filename);
			ContextToForm();
			ListToListView();
		}
		

		private void SaveContext(string filename)
		{
			Serializator.ToFile(filename, Current.Context);
		}

		private void AutoSave()
		{
			SaveContext(getDefaultInputName());
		}
		
		private void AutoLoad()
		{
			if (File.Exists(getDefaultInputName()))
				LoadContext(getDefaultInputName());
		}
		private string getDefaultInputName()
		{
			return Assembly.GetExecutingAssembly().Location + ".input";
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			if (chAutoSave.Checked)
				AutoLoad();
			
			FormToContext();		
		}

		private void cmdSave_Click(object sender, EventArgs e)
		{
			SaveFileDialog od = new SaveFileDialog();
			od.DefaultExt = "stress";
			od.Filter = "Stress tests (*.stress)|*.stress";
			if (od.ShowDialog(this) == DialogResult.Cancel)
				return;
			SaveContext(od.FileName);
		}

		private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			Application.Exit();
		}

		private void timRefresh_Tick(object sender, EventArgs e)
		{
			ResultsToForm();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem lwItem in lw.SelectedItems)
				Current.Context.List.Remove((Request) lwItem.Tag);
			Current.Context.UpdateStart();

			ListToListView();
			if (chAutoSave.Checked)
				AutoSave();
		}

		private void lw_KeyPress(object sender, KeyPressEventArgs e)
		{
		}

		private void lw_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete && btnDelete.Enabled)
				btnDelete.PerformClick();
		}

		private void chRandom_CheckedChanged(object sender, EventArgs e)
		{
			FormToContext();
		}
	}
}
