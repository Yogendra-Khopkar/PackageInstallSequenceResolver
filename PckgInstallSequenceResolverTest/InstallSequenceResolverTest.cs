using System.Collections.Generic;
using System.Linq;
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

		[TestMethod]
		public void TestInputWithCycles()
		{
			string[] input = new string[]
			{
				"KittenService: ",
				"Leetmeme: Cyberportal",
				"Cyberportal: Ice",
				"CamelCaser: KittenService",
				"Fraudstream: ",
				"Ice: Leetmeme"
			};

			InstallSequenceResolver resolver = new InstallSequenceResolver();
			string result = resolver.GetInstallSequence(input);
			Assert.AreEqual(ErrorMessages.INPUT_CONTAINS_CYCLE, result);
		}

		

		[TestMethod]
		public void TestInputContainsOneElementAllSpaces()
		{
			string[] input = new string[]{"                    "};

			InstallSequenceResolver resolver = new InstallSequenceResolver();
			string result = resolver.GetInstallSequence(input);
			Assert.AreEqual(ErrorMessages.INVALID_INPUT_MESSAGE, result);
		}

		[TestMethod]
		public void TestValidInputForCaseInsensitivity()
		{
			string[] input = new string[]
			{
				"KittenService: ",
				"Leetmeme: Cyberportal",
				"CYBERPORTAL: ICE",
				"CamelCaser: KittenService",
				"Fraudstream: ",
				"Ice: "
			};

			InstallSequenceResolver resolver = new InstallSequenceResolver();
			string result = resolver.GetInstallSequence(input);
			Assert.AreEqual("KittenService, ICE, Cyberportal, Leetmeme, CamelCaser, Fraudstream", result);
		}

		[TestMethod]
		public void TestInputWithInvalidFormat()
		{
			//The input contains elements without colon (:)
			string[] input = new string[]
			{
				"A: B",
				"XYZ",
				"P: Q"
			};
			InstallSequenceResolver resolver = new InstallSequenceResolver();
			string result = resolver.GetInstallSequence(input);
			Assert.AreEqual(ErrorMessages.INVALID_INPUT_MESSAGE, result);
		}

		[TestMethod]
		public void TestInputContainsNullElement()
		{
			string[] input = new string[] { null };

			InstallSequenceResolver resolver = new InstallSequenceResolver();
			string result = resolver.GetInstallSequence(input);
			Assert.AreEqual(ErrorMessages.INVALID_INPUT_MESSAGE, result);
		}

		[TestMethod]
		public void TestInputContainsEmptyStringAsPackageName()
		{
			string[] input = new string[]
			{
				"A: B",
				": A",
				"B: C",
				"C: ",
				"D: E",
				"E: "

			};

			InstallSequenceResolver resolver = new InstallSequenceResolver();
			string result = resolver.GetInstallSequence(input);
			Assert.AreEqual(ErrorMessages.PACKAGENAME_IS_EMPTY, result);
		}

		
	}
}
