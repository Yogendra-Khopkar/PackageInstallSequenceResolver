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

		[TestMethod]
		public void TestInputContainsSinglePackageWithNoDependency()
		{
			//if the input contains only 1 package which has no other dependency, 
			//the result contains only the package name. 

			InstallSequenceResolver resolver = new InstallSequenceResolver();
			string[]input = new string[]{"KittenService:"};
			string result = resolver.GetInstallSequence(input);
			Assert.AreEqual("KittenService", result);
		}

		[TestMethod]
		public void TestInputContainsTwoPackagesWithNoDependency()
		{
			//if the input contains 2 package names which have no other dependency, 
			//the result contains the package names. 

			InstallSequenceResolver resolver = new InstallSequenceResolver();
			string[] input = new string[] { "KittenService:","CamelCaser:" };
			string result = resolver.GetInstallSequence(input);
			Assert.AreEqual("KittenService, CamelCaser", result);
		}

		[TestMethod]
		public void TestPackageWithSingleDependency()
		{
			//if the input contains 1 package  depending on another, 
			//the result contains the package names with the dependency listed first

			//input ["KittenService:CamelCaser","CamelCaser:"];
			//result = "CamelCaser, KittenService"

			InstallSequenceResolver resolver = new InstallSequenceResolver();
			string[] input = new string[] { "KittenService:CamelCaser", "CamelCaser:" };
			string result = resolver.GetInstallSequence(input);
			Assert.AreEqual("CamelCaser, KittenService", result);
		}


		[TestMethod]
		public void TestValidInput()
		{
			string[] input = new string[]
			{
				"KittenService: ",
				"Leetmeme: Cyberportal",
				"Cyberportal: Ice",
				"CamelCaser: KittenService",
				"Fraudstream: ",
				"Ice: "
			};

			InstallSequenceResolver resolver = new InstallSequenceResolver();
			string result = resolver.GetInstallSequence(input);
			Assert.AreEqual("KittenService, Ice, Cyberportal, Leetmeme, CamelCaser, Fraudstream",result);
		}
	}
}
