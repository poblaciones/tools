using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wiki_getter
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			var i = new InfoGetter();
			i.Go(); 
			var s = new InfoSummarize();
			s.Go(); 

			Console.ReadLine();
		}
	}
}
