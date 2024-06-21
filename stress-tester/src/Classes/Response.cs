using System;

namespace stress_tester
{
	[Serializable]
	public class Response
	{
		public Guid Id { get; set; }
		public double Ellapsed { get; set; }
		public double StartTime { get; set; }
		public int Length { get; set; }
		public string Code { get; set; }
	}

}
