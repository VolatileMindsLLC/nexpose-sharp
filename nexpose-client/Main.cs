using System;
using System.Xml;
using nexposesharp;

namespace nexposeclient
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			using (NexposeSession session = new NexposeSession("192.168.56.101"))
			{
				session.Authenticate("nexpose"/*user*/, "nexpose"/*password*/);
				
				using (NexposeManager11 manager = new NexposeManager11(session))
				{
					XmlDocument vulns = manager.GetVulnerabilityListing();
					
					int i = 0;
					foreach (XmlNode vuln in vulns.FirstChild.ChildNodes)
					{
						string vulnID = vuln.Attributes["id"].Value;

						XmlDocument deets = manager.GetVulnerabilityDetails(vulnID);
						
						string title = deets.FirstChild.FirstChild.Attributes["title"].Value;
						string severity = deets.FirstChild.FirstChild.Attributes["severity"].Value;
						
						Console.WriteLine(String.Format("{0} has a severity of {1} and an id of {2}", title, severity, vulnID)); 
					
						i++;
					}
					
					Console.WriteLine("\n\nTotal vulnerabilities in database: " + i);
				}
			}
		}
	}
}
