using System;
using System.Xml;
using System.Net;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

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
			
			string response = this.ExecuteCommand(cmd);
			
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(response);
			
			if (doc.FirstChild.Attributes["success"].Value == "0")
				throw new Exception("Login failed. Check username/password.");
			
			this.SessionID = doc.FirstChild.Attributes["session-id"].Value;
			this.IsAuthenticated = true;
			
			return doc;
		}
		
		public XmlDocument Authenticate(string username, string password, string syncID, string siloID)
		{			
			string cmd = "<LoginRequest sync-id=\"" + syncID + "\" silo-id=\"" + siloID + "\" user-id=\"" + username + "\" password=\"" + password + "\" />";
			
			string response = this.ExecuteCommand(cmd);

			XmlDocument doc = new XmlDocument();
			doc.LoadXml(response);
			
			if (doc.FirstChild.Attributes["success"].Value == "0")
				throw new Exception("Login failed. Check username/password.");
			
			this.SessionID = doc.FirstChild.Attributes["session-id"].Value;
			this.IsAuthenticated = true;
			
			return doc;
		}
		
		public XmlDocument Logout()
		{
			string cmd = "<LogoutRequest session-id=\"" + this.SessionID + "\" />";
			
			string response = this.ExecuteCommand(cmd);
			
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(response);
			
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
		public string ExecuteCommand(string commandXml)
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
					throw new Exception("unknown api version.");
			}
			
			HttpWebRequest request = WebRequest.Create("https://" + this.NexposeHost + ":" + this.NexposePort.ToString() + uri) as HttpWebRequest;
			
			ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true; //anonymous lamda expressions ftw!
			
			request.KeepAlive = true;
			request.ProtocolVersion = HttpVersion.Version10;
            request.Method = "POST";
			request.ContentType = "text/xml";
			
            byte[] byteArray = Encoding.ASCII.GetBytes(commandXml);
            
            request.ContentLength = byteArray.Length;
			
            using (Stream dataStream = request.GetRequestStream())
            	dataStream.Write(byteArray, 0, byteArray.Length);
			
			XmlDocument response = new XmlDocument();
			string xml = string.Empty;
            using (HttpWebResponse r = request.GetResponse() as HttpWebResponse)
                using (StreamReader reader = new StreamReader(r.GetResponseStream()))
					xml = reader.ReadToEnd();
			
			string boundary = "--AxB9sl3299asdjvbA";
			
			if (xml.StartsWith(boundary))
			{
				string[] tmp = Regex.Split(xml, boundary);
				
				tmp = Regex.Split(tmp[2], "base64");
				
				string report = tmp[1].Replace("\r\n", string.Empty);
				
				//The following lines are a shim to get around an issue with the base64 encoded report nexpose returns.
				string t = report.Remove(0, report.Length - 4);
				
				if (t == "DQo=")
					report = report.Remove(report.Length - 4);
			
				report = Base64Decode(report);
				
				return report;
			}
			else
			{
				response.LoadXml(xml);
			
				if (response.FirstChild.FirstChild != null && response.FirstChild.FirstChild.Name == "Failure")	
					throw new Exception(response.FirstChild.FirstChild.FirstChild.InnerText);
			}
			
			return response.OuterXml; //silly, I know, but remediates not knowing if what is returned is really xml or not...
		}
		
		public string Base64Decode(string data)
		{
		    try
		    {
		        System.Text.ASCIIEncoding encoder = new System.Text.ASCIIEncoding();  
		        System.Text.Decoder utf8Decode = encoder.GetDecoder();
		    
		        byte[] todecode_byte = Convert.FromBase64String(data);
		        int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);    
		        char[] decoded_char = new char[charCount];
		        utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);                   
		        string result = new String(decoded_char);
		        return result;
		    }
		    catch(Exception e)
		    {
		        throw new Exception("Error in base64Decode: " + e.Message);
		    }
		}
	
		public void Dispose()
		{
			if (this.IsAuthenticated)
				this.Logout();
		}
	}
}

