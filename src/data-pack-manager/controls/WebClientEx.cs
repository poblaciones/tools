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
using System.Windows.Forms;
using medea.common;
using System.Threading;
using medea.actions;
using System.Collections.Specialized;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace medea.controls
{
public class WebClientEx : WebClient
{
    public WebClientEx(CookieContainer container)
    {
        this.container = container;
    }

    public CookieContainer CookieContainer
        {
            get { return container; }
            set { container= value; }
        }

    private CookieContainer container = new CookieContainer();

    protected override WebRequest GetWebRequest(Uri address)
    {
        WebRequest r = base.GetWebRequest(address);
        var request = r as HttpWebRequest;
        if (request != null)
        {
            request.CookieContainer = container;
        }
        return r;
    }

    protected override WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
    {
        WebResponse response = base.GetWebResponse(request, result);
        ReadCookies(response);
        return response;
    }

    protected override WebResponse GetWebResponse(WebRequest request)
    {
				try
			{
		
        WebResponse response = base.GetWebResponse(request);
        ReadCookies(response);
        return response;
    	}
			catch(WebException ex)
			{
				if (ex.Response != null)
				{
					using (var reader = new StreamReader(ex.Response.GetResponseStream()))
					{
						string result = reader.ReadToEnd();
						throw new Exception(result);
					}
				}
				throw ex;
			}
		}

    private void ReadCookies(WebResponse r)
    {
        var response = r as HttpWebResponse;
        if (response != null)
        {
            CookieCollection cookies = response.Cookies;
            container.Add(cookies);
        }
    }
	}
}
