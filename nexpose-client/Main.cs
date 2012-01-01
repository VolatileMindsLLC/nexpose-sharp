using System;
using System.Xml;
using nexposesharp;

namespace nexposeclient
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			using (NexposeSession session = new NexposeSession("192.168.1.178"))
			{
				session.Authenticate("nexpose"/*user*/, "nexpose"/*password*/);
				
				using (NexposeManager manager = new NexposeManager(session))
				{
					XmlDocument vulns = manager.GetVulnerabilityListing();
					
					foreach (XmlNode vuln in vulns.ChildNodes)
					{
						string vulnID = vuln.Attributes["vuln-id"].Value;

						XmlDocument deets = manager.GetVulnerabilityDetails(vulnID);
						
						string title = deets.Attributes["title"].Value;
						string severity = deets.Attributes["severity"].Value;
						
						Console.WriteLine(String.Format("{0} has a severity of {1} and an id of {2}", title, severity, vulnID)); 
					}
				}
			}
		}
	}
}
