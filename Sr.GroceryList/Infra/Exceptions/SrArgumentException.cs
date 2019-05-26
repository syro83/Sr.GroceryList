using System;
using System.Collections.Generic;
using System.Text;

namespace Sr.GroceryList.Infra.Exceptions
{
	public class SrArgumentException : SrException
	{
		private string _argumentName;

		public string ArgumentName
		{
			get => _argumentName;
		}

		public SrArgumentException(string message, string description, string argumentName) : base(message, description)
		{
			_argumentName = argumentName;
		}

	}
}
