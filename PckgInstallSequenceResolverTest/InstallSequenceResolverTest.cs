using Microsoft.VisualStudio.TestTools.UnitTesting;
using PckgInstallSequenceResolver;

namespace PckgInstallSequenceResolverTest
{
	[TestClass]
	public class InstallSequenceResolverTest
	{
		[TestMethod]
		public void TestEmptyArrayInput()
		{
			//if the string input is empty the result is empty string

			InstallSequenceResolver resolver = new InstallSequenceResolver();
			string result = resolver.GetInstallSequence(new string[] { });
			Assert.AreEqual(string.Empty, result);
		}
	}
}
