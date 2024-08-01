/*
*    Poblaciones - Plataforma abierta de datos espaciales de población.
*    Copyright (C) 2018-2024. Consejo Nacional de Investigaciones Científicas y Técnicas (CONICET)
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
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace medea.winApp
{
	class RegistryPersistence
	{
		public static void LoadUserFromRegistry(ref string User, ref string Password, ref string Server)
		{
			RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Poblaciones");
			//storing the values
			if (key != null)
			{
				Server = Encrypt.DecryptString(key.GetValue("MedeaHttpServer") as string);
				User = Encrypt.DecryptString(key.GetValue("MedeaHttpUser") as string);
				Password = Encrypt.DecryptString(key.GetValue("MedeaHttpPassword") as string);
				if (Password == "") Password = null;

				key.Close();
			}
		}
		public static void SaveUserToRegistry(string User, string Password, string Server)
		{
			RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Poblaciones");
			//storing the values
			key.SetValue("MedeaHttpServer", Encrypt.EncryptString(Server));
			key.SetValue("MedeaHttpUser", Encrypt.EncryptString(User));
			key.SetValue("MedeaHttpPassword", Encrypt.EncryptString(Password));
			key.Close();
		}
	}
}
