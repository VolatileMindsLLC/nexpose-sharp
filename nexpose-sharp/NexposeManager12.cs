using System;

namespace nexposesharp
{
	public class NexposeManager12 : NexposeManager11, IDisposable
	{
		NexposeSession _session;
		
		public NexposeManager12 (NexposeSession session) : base (session)
		{
			_session = session;
		}
	}
}

