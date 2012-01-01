using System;
using System.Xml;
using System.Net;
using System.Text;
using System.IO;

namespace nexposesharp
{
	public enum NexposeAPIVersion
	{
		v11 = 1,
		v12 = 2,
	}
	
	public class NexposeSession : IDisposable
	{
		public NexposeSession (string host)
		{
			this.NexposeHost = host;
			
			this.NexposePort = 3780; //default port
			
			this.APIVersion = NexposeAPIVersion.v11; //default
		}
		
		
		public string NexposeHost { get; set; }
		
		public int NexposePort { get; set; }
		
		public bool IsAuthenticated { get; set; }
		
		public string SessionID { get; set; }
		
		public NexposeAPIVersion APIVersion { get; set; }
		
		public XmlDocument Authenticate(string username, string password)
		{
			string cmd = "<LoginRequest user-id=\"" + username + "\" password=\"" + password + "\" />";
			
			XmlDocument doc = this.ExecuteCommand(cmd);
			
			if (doc.Attributes["success"].Value == "0")
				throw new Exception("Login failed. Check username/password.");
			
			this.SessionID = doc.Attributes["session-id"].Value;
			this.IsAuthenticated = true;
			
			return doc;
		}
		
		public XmlDocument Authenticate(string username, string password, string syncID, string siloID)
		{			
			string cmd = "<LoginRequest sync-id=\"" + syncID + "\" silo-id=\"" + siloID + "\" user-id=\"" + username + "\" password=\"" + password + "\" />";
			
			XmlDocument doc = this.ExecuteCommand(cmd);
			
			if (doc.Attributes["success"].Value == "0")
				throw new Exception("Login failed. Check username/password.");
			
			this.SessionID = doc.Attributes["session-id"].Value;
			this.IsAuthenticated = true;
			
			return doc;
		}
		
		public XmlDocument Logout()
		{
			string cmd = "<LogoutRequest session-id=\"" + this.SessionID + "\" />";
			
			XmlDocument doc = this.ExecuteCommand(cmd);
			
			this.IsAuthenticated = false;
			this.SessionID = string.Empty;
			 
			return doc;
		}
		
		/// <summary>
		/// Executes the command.
		/// </summary>
		/// <returns>
		/// The command results.
		/// </returns>
		/// <param name='commandXml'>
		/// Command xml.
		/// </param>
		public XmlDocument ExecuteCommand(string commandXml)
		{
			string uri = string.Empty;
			
		    switch (this.APIVersion)
			{
				case NexposeAPIVersion.v11:
					uri = "/api/1.1/xml";
					break;
				case NexposeAPIVersion.v12:
					uri = "/api/1.2/xml";
					break;
				default:
					break;
			}
			
			HttpWebRequest request = WebRequest.Create("https://" + this.NexposeHost + ":" + this.NexposePort.ToString() + uri) as HttpWebRequest;
			
			ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true; //anonymous delegates ftw!
			
			request.KeepAlive = true;
			request.ProtocolVersion = HttpVersion.Version10;
            request.Method = "POST";
			request.ContentType = "text/xml";
			
            byte[] byteArray = Encoding.ASCII.GetBytes(commandXml);
            
            request.ContentLength = byteArray.Length;
			
            using (Stream dataStream = request.GetRequestStream())
            	dataStream.Write(byteArray, 0, byteArray.Length);
			
			XmlDocument response = new XmlDocument();
			
            using (HttpWebResponse r = request.GetResponse() as HttpWebResponse)
                using (Stream responseStream = r.GetResponseStream())
					response.Load(responseStream);
			
			return response;	
			
			
		}
		
		public void Dispose()
		{
			if (this.IsAuthenticated)
				this.Logout();
		}
	}
}

