using System;
using System.Collections.Generic;
using System.Text;

namespace Actio.Common.Auth
{
    public class JwtOptions
    {
        public string SecretKey { get; set; }
        /// <summary>
        /// How long the token will be valid
        /// </summary>
        public int ExpiryMinutes { get; set; }
        /// <summary>
        /// State which service (URL or endpoint) create the token
        /// </summary>
        public string Issuer { get; set; }
    }
}
