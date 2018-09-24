using System.Collections.Generic;
using System.Linq;

namespace PckgInstallSequenceResolver
{
	public class ArrayInputParser : IInputParser
	{
		List<string> packageNames = new List<string>();
		IDictionary<string, string> dependencies = new Dictionary<string, string>();


		public IEnumerable<string> ParseInput(string[] input)
		{
			dependencies = GetPackageAndDependency(input);

			var noDependencyPackages = dependencies.Where(d => d.Value.Equals(string.Empty)).Select(p=>p.Key);

			return noDependencyPackages;

		}

		private IDictionary<string, string> GetPackageAndDependency(string[] input)
		{
			IDictionary<string, string> dependencies = new Dictionary<string, string>();

			foreach (var pckgDependency in input)
			{
				var dependencyArray = pckgDependency.Split(':');

				packageNames.Add(dependencyArray[0]);
				if (dependencyArray[1] != string.Empty)
				{
					packageNames.Add(dependencyArray[1]);
				}

				dependencies.Add(dependencyArray[0], dependencyArray[1]);
			}

			return dependencies;
		}
	}
}