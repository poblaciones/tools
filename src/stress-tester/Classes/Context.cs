using System;
using System.Collections.Generic;

namespace stress_tester
{
	[Serializable]
	public class Context
	{
		public int Clients { get; set; }
		public int ParallelRequests { get; set; }
		public int Loops { get; set; }
		public int Retries { get; set; }
		public int RetryMilliseconds { get; set; }
		public List<Request> List = new List<Request>();
		public string Filter { get; set; }
		public bool UseFilter { get; set; }
		public bool RandomOffsets { get; set; }

		public List<Request> FilteredList
		{
			get
			{
				List<Request> ret = new List<Request>();
				foreach(var r in List)
				{
					if (!UseFilter || r.Url.Contains(Filter))
						ret.Add(r);
				}
				return ret;
			}
		}

		public double ScriptTime
		{
			get
			{
			double maxMs = 0;
			foreach (var r in FilteredList)
				if (r.MillisecondsFromStart > maxMs)
					maxMs = r.MillisecondsFromStart;
				return maxMs;
			}
		}

		public Results Results = new Results();

		public void UpdateStart()
		{
			var min = DateTime.MaxValue;
			foreach (var r in FilteredList)
			{
				if (r.Date < min) min = r.Date;
			}
			foreach (var r in FilteredList)
			{
				r.UpdateStart(min);
			}
		}
	}
}
