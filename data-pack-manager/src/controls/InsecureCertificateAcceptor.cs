using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace medea.controls
{
	public class InsecureCertificateAcceptor
	{
		public static void ConfigureRequest()
		{
			ServicePointManager.ServerCertificateValidationCallback =
				new RemoteCertificateValidationCallback(AcceptAllCertificates);
		}

		private static bool AcceptAllCertificates(object sender, X509Certificate certificate,
												  X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return true; // Acepta todos los certificados
		}
	}
}