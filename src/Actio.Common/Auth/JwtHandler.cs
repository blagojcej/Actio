using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Actio.Common.Auth
{
    public class JwtHandler : IJWTHandler
    {
        private readonly JwtOptions _options;
        private readonly JwtSecurityTokenHandler _tokenHandler=new JwtSecurityTokenHandler();
        private readonly SecurityKey _issuerSignInKey;
        private readonly SigningCredentials _signingCredentials;
        private readonly JwtHeader _jwtHeader;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public JwtHandler(IOptions<JwtOptions> options)
        {
            _options = options.Value;
            _issuerSignInKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            _signingCredentials = new SigningCredentials(_issuerSignInKey, SecurityAlgorithms.HmacSha256);
            _jwtHeader=new JwtHeader(_signingCredentials);
            _tokenValidationParameters=new TokenValidationParameters()
            {
                //We don;t care which end client would be authenticated
                ValidateAudience = false,
                //Service responsible for creating tokens
                ValidIssuer = _options.Issuer,
                IssuerSigningKey = _issuerSignInKey
            };
        }

        public JsonWebToken Create(Guid userId)
        {
            var utcNow = DateTime.UtcNow;
            var expires = utcNow.AddMinutes(_options.ExpiryMinutes);
            //Define century begin to utc
            var centuryBegin=new DateTime(1970,1,1).ToUniversalTime();
            var exp = (long) (new TimeSpan(expires.Ticks - centuryBegin.Ticks).TotalSeconds);
            var now = (long) (new TimeSpan(utcNow.Ticks - centuryBegin.Ticks).TotalSeconds);

            var payload = new JwtPayload()
            {
                {"sub", userId},
                {"iss", _options.Issuer},
                //issue at
                {"iat", now},
                //expiration date
                {"exp", exp},
                {"unique_name", userId}
            };

            var jwt=new JwtSecurityToken(_jwtHeader, payload);
            var token = _tokenHandler.WriteToken(jwt);

            return new JsonWebToken()
            {
                Token = token,
                Expires = exp
            };
        }
    }
}
