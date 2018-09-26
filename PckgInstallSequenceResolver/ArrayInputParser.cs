using System;
using System.Collections.Generic;
using System.Linq;

namespace PckgInstallSequenceResolver
{
	class ArrayInputParser : IInputParser
	{


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
				if (!sequencedPackageNames.Contains(pkgDependency.Key, StringComparer.InvariantCultureIgnoreCase))
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
				AddPackageToStack(stackOfPackageAndDependency, packageName);

				while (dependency != string.Empty)
				{
					packageName = dependency;
					AddPackageToStack(stackOfPackageAndDependency, packageName);
					//dependency = packageAndDependencies.First(a => a.Key == packageName).Value;
					dependency = packageAndDependencies.First(a => string.Equals(a.Key,packageName,StringComparison.InvariantCultureIgnoreCase)).Value;

				}

				foreach (var pkg in stackOfPackageAndDependency)
				{
					dependencies.Add(pkg);
				}
			}

			return dependencies;

		}

		private void AddPackageToStack(Stack<string> stackOfPackageAndDependency, string packageName)
		{
			if (stackOfPackageAndDependency.Contains(packageName))
			{
				throw InputParserException.InvalidInputException(ErrorMessages.INPUT_CONTAINS_CYCLE);
			}
			else
			{
				stackOfPackageAndDependency.Push(packageName);
			}
		}

		private IDictionary<string, string> GetPackageAndDependency(string[] input)
		{
			IDictionary<string, string> dependencies = new Dictionary<string, string>();

			foreach (string pckgDependency in input)
			{
				if (!string.IsNullOrWhiteSpace(pckgDependency))
				{
					var dependencyArray = pckgDependency.Split(':');
					dependencies.Add(dependencyArray[0].Trim(), dependencyArray[1].Trim());
				}
			}

			return dependencies;
		}

		//case insensitive
		// missing :
		//move input validation logic to input parser
	}
}