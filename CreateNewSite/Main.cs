using System;
using System.Xml;
using nexposesharp;

namespace CreateNewSite
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string id = "-1'";
			string template = "full-audit";
			string name = "blah" + Guid.NewGuid().ToString();
			string description = "fdsa";
			string hosts = "127.0.0.1";
			
			string siteXml = "<Site id=\"" + id + "\" name=\"" + name+ "\" description=\"" + description + "\">";
				
			siteXml = siteXml + "<Hosts>";
			
			foreach (string host in hosts.Split(','))
				siteXml = siteXml + "<host>" + host + "</host>";
			
			siteXml = siteXml + "</Hosts>" +
								 "<Credentials></Credentials>" +
								 "<Alerting></Alerting>" +
								 "<ScanConfig configID=\"" + id + "\" name=\"" + name + "\" templateID=\"" + template + "\"></ScanConfig>" +
								 "</Site>";
			
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(siteXml);
			
			using (NexposeSession session = new NexposeSession("192.168.1.148"))
			{
				session.Authenticate("nexpose", "nexpose");
				
				using (NexposeManager11 manager = new NexposeManager11(session))
				{
					XmlDocument response = manager.SaveOrUpdateSite(doc.FirstChild);
					
					Console.WriteLine(response.OuterXml);
				}
			}
		}
	}
}
