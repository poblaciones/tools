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

namespace medea.common
{
	public class Progress
	{
		public event EventHandler ProgressChanged;
		public string _caption = "Procesando";
		private long _total = 100;
		private long _currentProgress = 0;

        public long Total
		{
			get { return _total; }
			set
			{
				_total = value;
				Value = 0;
			}
		}
		public long Value
		{
			get { return _currentProgress; }
			set
			{
				_currentProgress = value;

                OnProgressChanged();
			}
		}

		protected void OnProgressChanged()
		{
			if (ProgressChanged != null)
				ProgressChanged(this, EventArgs.Empty);
		}
		public string Caption
		{
			get { return _caption; }
			set
			{
				_caption = value;
				OnProgressChanged();
			}
		}
		public void Increment(int increment = 1)
		{
			if (Value < Total)
				Value += increment;
		}
	}
}
