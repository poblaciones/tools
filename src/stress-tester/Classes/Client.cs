using System;
using System.Collections.Generic;
using System.Threading;

namespace stress_tester
{
	public class Client
	{
		public object PulseLock = new object();

		private DateTime startDate;
		private List<Request> pending;
		private List<Request> requests;
		private int currentLoop = 0;
		private List<Caller> callers;

		public event EventHandler Finished;
		public string IsRetry { get; set; }
		public TimeSpan startDateOffset = TimeSpan.FromMilliseconds(0);

		public void Initialize(List<Request> requests)
		{
			this.requests = Serializator.Clone(requests);
			pending = new List<Request>();

			callers = new List<Caller>();
			for (int n = 0; n < Current.Context.ParallelRequests; n++)
				callers.Add(new Caller(this));
		}

		public void Start()
		{
			startDate = DateTime.Now + startDateOffset;
			this.pending.AddRange(this.requests);
			SortList();
			Pulse();
		}

		public void Pulse()
		{
			lock (PulseLock)
			{
				if (pending.Count == 0)
				{
					// Se fija si tiene que recomenzar
					currentLoop++;
					if (currentLoop < Current.Context.Loops)
					{
						Start();
					}
					else
					{
						foreach (var caller in callers)
							if (caller.isCalling.Get())
								return;
						// tiene todo libre... terminó
						Finished(null, null);
					}
					return;
				}
				Request r = pending[0];
				double relativeTime = GetRelativeTime();
				// Se fija si es futuro o pasado
				if (r.MillisecondsFromStart <= relativeTime || Current.Context.NonStop)
				{
					// Lo trata de encolar...
					foreach (var caller in callers)
						if (caller.TryCall(r))
						{
							// Lo remueve de la cola
							pending.RemoveAt(0);
							// genera otro pulso por 
							// si eran varios los pendientes
							// o para esperar al próximo activamente
							Pulse();
							break;
						}
				}
				else
				{
					// Pone un pulso para el próximo momento
					ScheduleAsyncPulse(r.MillisecondsFromStart - relativeTime);
				}
			}
		}

		public double GetRelativeTime()
		{
			DateTime now = DateTime.Now;
			return now.Subtract(startDate).TotalMilliseconds;
		}

		public void ScheduleAsyncRequestRetry(Request r)
		{
			lock (PulseLock)
			{
				r.RetryAttemps++;
				r.MillisecondsFromStart = GetRelativeTime() + Current.Context.RetryMilliseconds;
				this.pending.Add(r);
				SortList();
				Pulse();
			}
		}
		private void ScheduleAsyncPulse(double ms)
		{
			Thread t = new Thread(new ParameterizedThreadStart(DelayedPulse));
			t.Start(ms);
		}
		private void DelayedPulse(object ms)
		{
			Thread.Sleep((int) (double) ms);
			Pulse();
		}

		private void SortList()
		{
			this.pending.Sort((p, q) => p.MillisecondsFromStart.CompareTo(q.MillisecondsFromStart));
		}
	}
}
