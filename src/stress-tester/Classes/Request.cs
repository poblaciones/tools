using System;

namespace stress_tester
{
	[Serializable]
	public class Request
	{
		public bool IsRetry {  get { return RetryAttemps > 0; } }
		public int RetryAttemps { get; set; }

		public Guid Id { get; set; }
		public string Url { get; set; }
		public DateTime Date { get; set; }

		public double MillisecondsFromStart { get; set; }

		public Request(string url, DateTime date, Guid id)
		{
			Url = url;
			Date = date;
			Id = id;
		}
		public void UpdateStart(DateTime start)
		{
			MillisecondsFromStart = Date.Subtract(start).TotalMilliseconds;
		}

	}

}
