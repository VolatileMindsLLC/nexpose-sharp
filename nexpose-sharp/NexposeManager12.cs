using System;

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
	}
}

