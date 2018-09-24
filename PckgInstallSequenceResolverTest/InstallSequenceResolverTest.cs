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

		[TestMethod]
		public void TestNullInput()
		{
			//if the input is null, an error message is returned
			InstallSequenceResolver resolver = new InstallSequenceResolver();
			string result = resolver.GetInstallSequence(null);
			Assert.AreEqual(ErrorMessages.NULL_INPUT_MESSAGE, result);
		}
	}
}
