using System.Collections;
using System.Collections.Generic;

namespace PckgInstallSequenceResolver
{
	public interface IInputParser
	{
		IEnumerable<string> ParseInput(string[] input);
	}
}