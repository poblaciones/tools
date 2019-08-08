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
using System.Windows.Forms;
using medea.common;
using System.Threading;
using medea.actions;

namespace medea.controls
{
	public class Invoker
	{
		private action currentAction;
		private frmProgress panProgress;
		bool ret = false;
		delegate void ProgressDelegate(long current, long total, string caption);

		public static bool CallProgress(action action)
		{
			Invoker i = new Invoker();
			i.doCallProgress(action);
			return i.ret;
		}
		private void doCallProgress(action action)
		{
			currentAction = action;
			action.Progress.ProgressChanged += new EventHandler(action_ProgressChanged);
			// Ejecuta una acción mostrando un progress...
			panProgress = new frmProgress();
			panProgress.Start();
			WaitCursor.Set();
			// llama en otro thread a la llamada
			Thread t = new Thread(invokeAction);
			t.Name = "MedeaInvokerThread";
			t.Start();
			panProgress.ShowDialog(Form.ActiveForm);
		}

		void action_ProgressChanged(object sender, EventArgs e)
		{
            while(panProgress.Created == false)
            {
                panProgress.CreateControl();
                Application.DoEvents();
            }
            panProgress.Invoke(new ProgressDelegate(updateProgress),
				new object[] { currentAction.Progress.Value, currentAction.Progress.Total, currentAction.Progress.Caption});
		}

		private void updateProgress(long current, long total, string caption)
		{
			panProgress.updateProgress(current, total, caption);
		}
		private void invokeAction()
		{
			try
			{
				context.Data.CheckSession();
				if (!(currentAction is INonTransactionAction))
					context.Data.Session.BeginTransaction();
				currentAction.Call();
				if (!(currentAction is INonTransactionAction))
					context.Data.Session.Commit();
				panProgress.Invoke(new EventHandler(actionCompleted), null, null);
			}
			catch (Exception e)
			{
				try
				{
					if (context.Data.Session.IsInTransaction)
					{
						context.Data.Session.Rollback();
						context.Data.Session.Clear();
					}
				} catch {}
					panProgress.Invoke(new EventHandler(showError), e, null);
			}
		}
		private void actionCompleted(object o, EventArgs args)
		{
			WaitCursor.Reset();
			ret = true;
			panProgress.Stop();
		}
		private void showError(object o, EventArgs args)
		{
			Exception e = o as Exception;
			WaitCursor.Reset();
			UI.ShowError(panProgress, e);
			ret = false;
			panProgress.Stop();
		}

	}
}
