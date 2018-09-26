using System;
using System.Collections.Generic;

namespace PckgInstallSequenceResolver
{
	public class InstallSequenceResolver
	{
		public string GetInstallSequence(string[] input)
		{
			string result = String.Empty;
			try
			{
				IInputParser parser = new ArrayInputParser();
				IEnumerable<string> sequencedPackages = parser.ParseInput(input);

				result = string.Join(", ", sequencedPackages);

			}
			catch (InputParserException ipe)
			{
				//log the exception 
				result = ipe.Message;
			}
			catch (Exception e)
			{
				//log the exception 
				result = ErrorMessages.GENERIC_ERROR_MESSAGE;
			}

			return result;
		}
	}
}
