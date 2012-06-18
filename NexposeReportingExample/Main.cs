using System;
using nexposesharp;
using System.Xml;
using System.Collections.Generic;

namespace NexposeReportingExample
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			using (NexposeSession session = new NexposeSession("127.0.0.1"))
			{
				session.Authenticate("v4test"/*user*/, "buynexpose"/*password*/);
				
				using (NexposeManager11 manager = new NexposeManager11(session))
				{
					XmlDocument sites = manager.GetSiteListing();
					
					Console.WriteLine("Which site do you want a scan report for?");
					
					foreach (XmlNode site in sites.FirstChild.ChildNodes)
						Console.WriteLine(String.Format("{0}. {1}", site.Attributes["id"].Value, site.Attributes["name"].Value));
					
					string id = Console.ReadLine();
					
					XmlDocument templates = manager.GetReportTemplateListing();
					
					string templateID = string.Empty;
					
					Console.WriteLine("Which template do you want to use?");
					foreach (XmlNode template in templates.FirstChild.ChildNodes)
						Console.WriteLine(template.Attributes["template-id"].Value);
			
					templateID = Console.ReadLine();
					
					Dictionary<NexposeReportFilterType, string> filters = new Dictionary<NexposeReportFilterType, string>();
					
					//add a filter for a specific "site" with and id of "id"
					filters.Add(NexposeReportFilterType.Site, id);
					
					byte[] report = manager.GenerateAdHocReport(NexposeUtil.GenerateAdHocReportConfig(templateID, NexposeReportFormat.RawXML, filters));
					Console.WriteLine(report);
				}
			}
		}
	}
}
