using System.Collections.Generic;
using System.Linq;

namespace PckgInstallSequenceResolver
{
	class ArrayInputParser : IInputParser
	{
		List<string> sequencedPackageNames1 = new List<string>();
		IDictionary<string, string> dependencies1 = new Dictionary<string, string>();


		public IEnumerable<string> ParseInput(string[] input)
		{
			IDictionary<string, string> dependencies = GetPackageAndDependency(input);
			List<string> sequencedPackageNames = new List<string>();


			foreach (var pkgDependency in dependencies)
			{
				string packageName = pkgDependency.Key;

				if (string.IsNullOrWhiteSpace(packageName))
				{
					throw InputParserException.InvalidInputException(ErrorMessages.PACKAGENAME_IS_EMPTY);
				}
				if (!sequencedPackageNames.Contains(pkgDependency.Key))
				{
					IEnumerable<string> resolvedDependencies = ResolveDependenciesForPackage(pkgDependency, dependencies).ToList();
					sequencedPackageNames.AddRange(resolvedDependencies);
				}
			}
			return sequencedPackageNames.Distinct();

		}

		private IEnumerable<string> ResolveDependenciesForPackage(KeyValuePair<string, string> pkgDependency, IDictionary<string,string> packageAndDependencies)
		{
			string packageName = pkgDependency.Key;
			string dependency = pkgDependency.Value;


			List<string> dependencies = new List<string>();
			if (dependency == string.Empty)
			{
				dependencies.Add(packageName);
			}
			else
			{
				Stack<string> stackOfPackageAndDependency = new Stack<string>();
				stackOfPackageAndDependency.Push(packageName);

				while (dependency != string.Empty)
				{
					packageName = dependency;
					stackOfPackageAndDependency.Push(packageName);
					dependency = packageAndDependencies.First(a => a.Key == packageName).Value;
				}

				foreach (var pkg in stackOfPackageAndDependency)
				{
					dependencies.Add(pkg);
				}
			}

			return dependencies;

		}

		private IDictionary<string, string> GetPackageAndDependency(string[] input)
		{
			IDictionary<string, string> dependencies = new Dictionary<string, string>();

			foreach (var pckgDependency in input)
			{
				var dependencyArray = pckgDependency.Split(':');
				dependencies.Add(dependencyArray[0].Trim(), dependencyArray[1].Trim());
			}

			return dependencies;
		}
	}
}