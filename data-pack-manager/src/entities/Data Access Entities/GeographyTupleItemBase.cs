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

namespace medea.entities
{
	public abstract class GeographyTupleItemBase<T> : ActiveBaseEntity<T>, IIdentifiable
	where T: ActiveBaseEntity<T>, new()
	{
		
		#region Campos privados
		
		private int? _id;
		private GeographyTuple _geographyTuple;
        private int _geographyItemId;
        private int _geographyPreviousId;
        private int _geographyPreviousItemId;
        private bool _isParcial;
        #endregion


        #region Propiedades públicas

        public virtual int? Id
		{
			get { return _id; }
			set { _id = value; }
		}
		public virtual GeographyTuple GeographyTuple
        {
			get { return _geographyTuple; }
			set { _geographyTuple = value; }
		}

        public virtual int GeographyItemId
        {
            get { return _geographyItemId; }
            set { _geographyItemId = value; }
        }

        public virtual int GeographyPreviousId
        {
            get { return _geographyPreviousId; }
            set { _geographyPreviousId = value; }
        }

        public virtual int GeographyPreviousItemId
        {
            get { return _geographyPreviousItemId; }
            set { _geographyPreviousItemId = value; }
        }


        public virtual bool IsPartial
        {
            get { return _isParcial; }
            set { _isParcial = value; }
        }
        #endregion


        #region Colecciones públicas

        #endregion


        #region Overrides

        public override int GetHashCode()
		{
			return Id.GetValueOrDefault();
		}
		
		#endregion
		
	}
}
