using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wiki_getter
{
	class Barrio
	{
		public string name;
		public string url;
		public string id;
		public dynamic polygon;
		public Geometry geometry;

	}
}
