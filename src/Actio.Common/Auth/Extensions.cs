using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Actio.Common.Auth
{
    public static class Extensions
    {
        public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = new JwtOptions();
            var section = configuration.GetSection("jwt");
            section.Bind(jwtOptions);

            services.Configure<JwtOptions>(section);
            services.AddSingleton<IJWTHandler, JwtHandler>();
            services.AddAuthentication()
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        //We don;t care which end client would be authenticated
                        ValidateAudience = false,
                        //Service responsible for creating tokens
                        ValidIssuer = jwtOptions.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                    };
                });
        }
    }
}
