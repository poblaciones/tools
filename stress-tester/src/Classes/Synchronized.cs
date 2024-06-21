using System;

namespace stress_tester
{
	[Serializable]
	public class Synchronized<T>
	{
		protected object Lock = new object();

		protected T _value;

		public Synchronized()
		{
		}
		public Synchronized(object Lock)
		{
			this.Lock = Lock;
		}

		public void Set(T value)
		{
			lock(Lock)
			{
				this._value = value;
			}
		}
		public T Get()
		{
			lock(Lock)
			{
				return this._value;
			}
		}
	}
}
