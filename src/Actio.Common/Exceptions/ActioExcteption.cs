using System;
using System.Collections.Generic;
using System.Text;

namespace Actio.Common.Exceptions
{
    public class ActioExcteption : Exception
    {
        public string Code { get; }

        public ActioExcteption()
        {

        }

        public ActioExcteption(string code)
        {
            Code = code;
        }

        public ActioExcteption(string message, params object[] args) : this(string.Empty, message, args)
        {

        }

        public ActioExcteption(string code, string message, params object[] args) : this(null, code, message, args)
        {

        }

        public ActioExcteption(Exception innerException, string message, params object[] args) : this(innerException,
            string.Empty, message, args)
        {

        }

        public ActioExcteption(Exception innerException, string code, string messsage, params object[] args) : base(
            string.Format(messsage, args), innerException)
        {
            Code = code;
        }
    }
}
