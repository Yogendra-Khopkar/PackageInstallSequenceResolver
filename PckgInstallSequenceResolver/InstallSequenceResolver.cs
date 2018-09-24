﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PckgInstallSequenceResolver
{
	public class InstallSequenceResolver
	{
		public string GetInstallSequence(string[] input)
		{
			string result = String.Empty;
			if (input == null)
			{
				return ErrorMessages.NULL_INPUT_MESSAGE;
			}
			if (!input.Any())
			{
				return result;
			}

			IInputParser parser = new ArrayInputParser();
			IEnumerable<string> sequencedPackages = parser.ParseInput(input);

			result = string.Join(",", sequencedPackages);

			return result;
		}

		
	}
}
