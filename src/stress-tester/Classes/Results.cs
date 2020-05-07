using System;
using System.Collections.Concurrent;

namespace stress_tester
{
	[Serializable]
	public class Results
	{
		public SynchronizedInt Hits = new SynchronizedInt();
		public SynchronizedInt TotalTime = new SynchronizedInt();
		public SynchronizedInt Errors = new SynchronizedInt();
		public SynchronizedInt Retries = new SynchronizedInt();
		public SynchronizedInt Executing = new SynchronizedInt();
		public SynchronizedInt MaxExecuting = new SynchronizedInt();
		public SynchronizedInt TotalBytes = new SynchronizedInt();

		public ConcurrentBag<Response> Responses = new ConcurrentBag<Response>();
		
		public Results GetCopy()
		{
			Results ret = new Results();
			ret.Hits.Set(Hits.Get());
			ret.TotalTime.Set(TotalTime.Get());
			ret.Errors.Set(Errors.Get());
			ret.Retries.Set(Retries.Get());
			ret.Executing.Set(Executing.Get());
			ret.MaxExecuting.Set(MaxExecuting.Get());
			ret.TotalBytes.Set(TotalBytes.Get());
			foreach (var r in Responses.ToArray())
				ret.Responses.Add(r);
			Responses = new ConcurrentBag<Response>();
			return ret;
		}

		public void ExecutingIncrement()
		{
			var value = Executing.Increment();
			if (value > MaxExecuting.Get())
				MaxExecuting.Set(value);
		}
	}
}
