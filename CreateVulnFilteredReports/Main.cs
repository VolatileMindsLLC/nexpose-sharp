using System;
using nexposesharp;
using System.Xml;
using System.Collections.Generic;

namespace CreateVulnFilteredReports
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			bool generateNow = true;
			
			using (NexposeSession session = new NexposeSession("127.0.0.1"))
			{
				session.Authenticate("username", "password");
				
				using (NexposeManager12 manager = new NexposeManager12(session))
				{
					ReportConfig config = new ReportConfig();
					
					config.Name = Guid.NewGuid().ToString();
					config.Format = NexposeReportFormat.RawXMLv2;
					config.TemplateID = "audit-report";
					
					Dictionary<NexposeReportFilterType, string> filters = new Dictionary<NexposeReportFilterType, string>();
					
					filters.Add(NexposeReportFilterType.VulnerabilityCategories, "include:adobe");
					filters.Add(NexposeReportFilterType.Site, "1");
					
					config.Filters = filters;
					
					XmlNode configNode = config.GenerateXML();
					
					XmlDocument response = manager.SaveOrUpdateReport(configNode, generateNow);
					
					filters = new Dictionary<NexposeReportFilterType, string>();
					filters.Add(NexposeReportFilterType.VulnerabilityCategories, "include:Adobe");
					filters.Add(NexposeReportFilterType.Site, "1");
					
					config.Filters = filters;
					config.Name = Guid.NewGuid().ToString();
					
					configNode = config.GenerateXML();
					
					response = manager.SaveOrUpdateReport(configNode, generateNow);
				}
			}
		}
	}
}
