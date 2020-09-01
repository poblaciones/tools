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
using medea.common;
using medea.entities;

namespace medea.actions
{
	public class MetadataSave : action
	{
		private Metadata current;

		public MetadataSave(Metadata current)
		{
			this.current = current;
		}
		public override void Call()
		{
			Progress.Caption = "Actualizando metadatos";
			Progress.Total = 1;

			PublicId.SetId(current.Contact);
			PublicId.SetId(current.Institution);
			PublicId.SetId(current);

			context.Data.Session.SaveOrUpdate(current.Contact);
			context.Data.Session.SaveOrUpdate(current.Institution);


			if (current.MetadataSources != null)
			{
				foreach(var src in current.MetadataSources)
				{
					PublicId.SetId(src.Source.Contact);
					PublicId.SetId(src);
					PublicId.SetId(src.Source.Institution);
					PublicId.SetId(src.Source);

					context.Data.Session.SaveOrUpdate(src.Source.Contact);

					context.Data.Session.SaveOrUpdate(src.Source.Institution);

					context.Data.Session.SaveOrUpdate(src.Source);

					context.Data.Session.SaveOrUpdate(src);
				}
			}
			context.Data.Session.SaveOrUpdate(current);

			foreach(var file in current.Files)
				if (file.FileAdded && file.File != null)
			{
				PublicId.SetId(file);
				FileSave fs = new FileSave(this, file.File);
				fs.Call();
			}
		}
	}
}
