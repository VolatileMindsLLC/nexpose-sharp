using System;
using System.Xml;

namespace nexposesharp
{
	public class NexposeManager12 : NexposeManager11, IDisposable
	{
		NexposeSession _session;
		
		public NexposeManager12 (NexposeSession session) : base (session)
		{
			if (!session.IsAuthenticated)
				throw new Exception("Trying to create manager from unauthenticated session. Please authenticate.");
			
			_session = session;
		}
		
		public XmlDocument GetEngineConfig(string engineID)
		{
			string cmd = "<EngineConfigRequest session-id=\"" + _session.SessionID + "\" engine-id=\"" + engineID + "\" />";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument DeleteEngine(string engineID)
		{
			string cmd = "<EngineDeleteRequest session-id=\"" + _session.SessionID + "\" engine-id=\"" + engineID + "\" />";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument SaveOrUpdateEngine(XmlNode engine)
		{
			string cmd = "<EngineSaveRequest session-id=\"" + _session.SessionID + "\" >";
			
			cmd = cmd + engine.OuterXml;
			
			cmd = cmd + "</EngineSaveRequest>";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument CreateTicket(XmlNode ticket)
		{
			string cmd = "<TicketCreateRequest session-id=\"" + _session.SessionID + "\" >";
			
			cmd = cmd + ticket.OuterXml;
			cmd = cmd + "</TicketCreateRequest>";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument GetTicketListing()
		{
			string cmd = "<TicketListingRequest session-id=\"" + _session.SessionID + "\" />";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument GetTicketDetails(string ticketID)
		{
			string cmd = "<TicketDetailsRequest session-id=\"" + _session.SessionID + "\"  >";
			
			cmd = cmd + "<Ticket id=\"" + ticketID + "\" /></TicketDetailsRequest>";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument DeleteTicket(string ticketID)
		{
			string cmd = "<TicketDeleteRequest session-id=\"" + _session.SessionID + "\" >";
			cmd = cmd + "<Ticket id=\"" + ticketID + "\" /></TicketDeleteRequest>";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument GetPendingVulnExceptionCount()
		{
			string cmd = "<PendingVulnExceptionCountRequest session-id=\"" + _session.SessionID + "\" />";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument GetVulnerabilityExceptionListing()
		{
			string cmd = "<VulnerabilityExceptionListingRequest session-id=\"" + _session.SessionID + "\" />";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument CreateVulnerabilityException()
		{
			string cmd = "<VulnerabilityExceptionCreateRequest session-id=\"" + _session.SessionID + "\" />";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument ResubmitVulnerabilityException(string vulnExceptionID, string comment)
		{
			string cmd = "<VulnerabilityExceptionReSubmitRequest session-id=\"" + _session.SessionID + "\" >";
			
			cmd = cmd + "<comment>" + comment + "</comment></VulnerabilityExceptionReSubmitRequest>";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument RecallVulnerabilityException(string vulnExceptionID)
		{
			string cmd = "<VulnerabilityExceptionRecallRequest session-id=\"" + _session.SessionID + "\" exception-id=\"" + vulnExceptionID + "\" />";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument ApproveVulnerabilityException(string vulnExceptionID)
		{
			string cmd = "<VulnerabilityExceptionApproveRequest session-id=\"" + _session.SessionID + "\" exception-id=\"" + vulnExceptionID + "\" />";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument RejectVulnerabilityException(string vulnExceptionID)
		{
			string cmd = "<VulnerabilityExceptionRejectRequest session-id=\"" + _session.SessionID + "\"  exception-id=\"" + vulnExceptionID + "\" />";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument DeleteVulnerabilityException(string vulnExceptionID)
		{
			string cmd = "<VulnerabilityExceptionDeleteRequest session-id=\"" + _session.SessionID + "\"  exception-id=\"" + vulnExceptionID + "\" />";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument UpdateCommentForVulnerabilityException(string vulnExceptionID, string reviewerComment, string submitterComment)
		{
			string cmd = "<VulnerabilityExceptionUpdateCommentRequest session-id=\"" + _session.SessionID + "\" >";
			
			cmd = cmd + "<reviewer-comment>" + reviewerComment + "</reviewer-comment>";
			cmd = cmd + "<submitter-comment>" + submitterComment + "</submitter-comment>";
			cmd = cmd + "</VulnerabilityExceptionUpdateCommentRequest>";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument UpdateExpiryDateForVulnerabilityException(string vulnExceptionID, string date)
		{
			string cmd = "<VulnerabilityExceptionUpdateExpiryDateRequest session-id=\"" + _session.SessionID + "\" expiration-date=\"" + date + "\" />";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument CreateMultiTenantUser()
		{
			string cmd = "<CreateMultiTenantUserRequest session-id=\"" + _session.SessionID + "\" />";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument GetMultiTenantUserListing()
		{
			string cmd = "<MultiTenantUserListingRequest session-id=\"" + _session.SessionID + "\" />";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument UpdateMultiTenantUser(XmlNode user)
		{
			string cmd = "<MultiTenantUserUpdateRequest session-id=\"" + _session.SessionID + "\" >";
			
			cmd = cmd + user.OuterXml;
			cmd = cmd + "</MultiTenantUserUpdateRequest>";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument GetMultiTenantUserConfig(string userID)
		{
			string cmd = "<MultiTenantUserConfigRequest session-id=\"" + _session.SessionID + "\" user-id=\"" + userID + "\" />";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument DeleteMultiTenantUser(string userID)
		{
			string cmd = "<MultiTenantUserDeleteRequest session-id=\"" + _session.SessionID + "\" user-id=\"" + userID + "\"  />";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument CreateSiloProfile()
		{
			string cmd = "<SiloProfileCreateRequest session-id=\"" + _session.SessionID + "\" />";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument GetSiloProfileListing()
		{
			string cmd ="<SiloProfileListingRequest session-id=\"" + _session.SessionID + "\" />";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument UpdateSiloProfile(XmlNode siloProfile)
		{
			string cmd = "<SiloProfileUpdateRequest session-id=\"" + _session.SessionID + "\" >";
			
			cmd = cmd + siloProfile.OuterXml;
			cmd = cmd + "</SiloProfileUpdateRequest>";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument GetSiloProfileConfig(string siloProfileID)
		{
			string cmd = "<SiloProfileConfigRequest session-id=\"" + _session.SessionID + "\" silo-profile-id=\"" + siloProfileID + "\" />";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument DeleteSiloProfile(string siloProfileID)
		{
			string cmd = "<SiloProfileDeletRequest session-id=\"" + _session.SessionID + "\" silo-profile-id=\"" + siloProfileID + "\" />";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument CreateSilo()
		{
			string cmd = "<SiloCreateRequest session-id=\"" + _session.SessionID + "\" />";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument GetSiloListing()
		{
			string cmd = "<SiloListingRequest session-id=\"" + _session.SessionID + "\" />";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument GetSiloConfig(string siloID, string siloName)
		{
			string cmd = "<SiloConfigRequest session-id=\"" + _session.SessionID + "\" id=\"" + siloID + "\" name=\"" + siloName + "\" />";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument UpdateSilo(XmlNode silo)
		{
			string cmd = "<SiloUpdateRequest session-id=\"" + _session.SessionID + "\" >";
			
			cmd = cmd + silo.OuterXml;
			
			cmd = cmd + "</SiloUpdateRequest>";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument DeleteSilo(string siloID, string siloName)
		{
			string cmd = "<SiloDeleteRequest session-id=\"" + _session.SessionID + "\" silo-id=\"" + siloID + "\" silo-name=\"" + siloName + "\" />";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument CreateRole(XmlNode role)
		{
			string cmd = "<RoleCreateRequest session-id=\"" + _session.SessionID + "\" >";
			
			cmd = cmd + role.OuterXml;
			cmd = cmd + "</RoleCreateRequest>";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument GetRoleListing()
		{
			string cmd = "<RoleListingRequest session-id=\"" + _session.SessionID + "\" />";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument GetRoleDetails(string roleID)
		{
			string cmd = "<RoleDetailsRequest session-id=\"" + _session.SessionID + "\" role-id=\"" + roleID + "\" />";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument UpdateRole(XmlNode role)
		{
			string cmd = "<RoleUpdateRequest session-id=\"" + _session.SessionID + "\" >";
			
			cmd = cmd + role.OuterXml;
			
			cmd = cmd + "</RoleUpdateRequest>";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument DeleteRole(string roleID)
		{
			string cmd = "<RoleDeleteRequest session-id=\"" + _session.SessionID + "\" role-id=\"" + roleID + "\" />";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument CreateEnginePool()
		{
			string cmd = "<EnginePoolCreateRequest session-id=\"" + _session.SessionID + "\" />";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument GetEnginePoolListing()
		{
			string cmd = "<EnginePoolListingRequest session-id=\"" + _session.SessionID + "\" >";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument GetEnginePoolDetails(string enginePoolID)
		{
			string cmd = "<EnginePolleDetailsRequest session-id=\"" + _session.SessionID + "\" pool-id=\"" + enginePoolID + "\" />";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument UpdateEnginePool(XmlNode pool)
		{
			string cmd = "<EnginePoolUpdateRequest session-id=\"" + _session.SessionID + "\" >";
			
			cmd = cmd + pool.OuterXml;
			
			cmd = cmd + "</EnginePoolUpdateRequest>";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
		public XmlDocument DeleteEnginePool(string enginePoolID)
		{
			string cmd = "<EnginePoolDeleteRequest session-id=\"" + _session.SessionID + "\" pool-id=\"" + enginePoolID + "\" />";
			
			XmlDocument doc = _session.ExecuteCommand(cmd);
			
			return doc;
		}
		
	}
}

