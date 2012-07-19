using System;
using System.Text.RegularExpressions;
using System.Drawing;

namespace DecodeMultiPartMIME
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string responseFile = args[0];
			string responseOutput = args[1]; 
			string response = System.IO.File.ReadAllText(responseFile);
			
			foreach (string part in Regex.Split(response, "--AxB9sl3299asdjvbA"))
			{
				if (string.IsNullOrEmpty(part) || part.StartsWith("--")) continue;
				string filename = Regex.Split(part, "name=")[1].Split('\n')[0].Replace("\r", string.Empty).Replace("\n", string.Empty);
				
				try
				{
					byte[] bytes = Convert.FromBase64String(Regex.Split(part, "base64")[1].Replace("\n", string.Empty).Replace("\r", string.Empty));
					System.IO.File.WriteAllBytes(responseOutput + "/" +filename, bytes);
				}
				catch
				{
					continue;
				}
			}
		}	
	}
}
