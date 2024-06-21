using System;

namespace stress_tester
{
	[Serializable]
	public class SynchronizedInt : Synchronized<int>
	{
		public int Increment(int n = 1)
		{
			lock(Lock)
			{
				this._value += n;
				return this._value;
			}
		}
		public int Decrement(int n = 1)
		{
			lock(Lock)
			{
				this._value -= n;
				return this._value;
			}
		}
	}
}
