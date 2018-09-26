using System.Collections.Generic;

namespace PckgInstallSequenceResolver
{
	/// <summary>
	/// Parses the input to determine the package install sequence.
	/// </summary>
	public interface IInputParser
	{
		IEnumerable<string> ParseInput(string[] input);
	}
}