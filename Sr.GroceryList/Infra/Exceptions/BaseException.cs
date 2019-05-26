using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Sr.GroceryList.Infra.Exceptions
{
    public class SrException : Exception
    {
        private string _description;

        public string InternalDescription
        {
            get => _description;
        }

        public SrException(string message) :
            base(message)
        {
            _description = message;
        }

        public SrException(string message, params object[] args) :
            base(message)
        {
            _description = String.Format(CultureInfo.CurrentCulture, message, args);
        }

        public SrException(string message, string internalDescription) : 
            base(message)
        {
            _description = internalDescription;
        }

        public SrException(string message, string internalDescription, params object[] args) :
            base(message)
        {
            _description = String.Format(CultureInfo.CurrentCulture, internalDescription, args);
        }
    }
}
