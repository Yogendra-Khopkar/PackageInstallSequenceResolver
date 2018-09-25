using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PckgInstallSequenceResolver
{
	public class InputParserException : Exception
	{
		private InputParserException(string message) : base(message)
		{
		}

		public static InputParserException InvalidInputException(string message)
		{
			return new InputParserException(message);
		}
	}
}
