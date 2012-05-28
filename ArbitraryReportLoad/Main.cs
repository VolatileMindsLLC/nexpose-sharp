using System;
using System.Text.RegularExpressions;

namespace ArbitraryReportLoad
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string response = System.IO.File.ReadAllText("/home/bperry/Desktop/response.txt");
			
			string[] tmp = Regex.Split(response, "--AxB9sl3299asdjvbA");
			
			tmp = Regex.Split(tmp[2], "base64");
			
			string report = tmp[1].Replace("\r\n", string.Empty);
		
			report = Base64Decode(report);
			
			Console.WriteLine(report);
		}
		
		private static string Base64Decode(string data)
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
		        throw new Exception("Error in base64Decode" + e.Message);
		    }
		}
	}
}
