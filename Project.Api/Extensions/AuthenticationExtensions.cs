
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Project.Api.Extensions
{
    public static class AuthenticationExtensions
    {
        // Define the RegisterAuthentication method to register authentication in the WebApplicationBuilder
        public static WebApplicationBuilder RegisterAuthentication(this WebApplicationBuilder builder)
        {
            //builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings:"));

            // Add JWT authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = builder.Configuration.GetValue<string>("TokenSettings:Issuer"),
                        ValidateIssuer = true,
                        ValidAudience = builder.Configuration.GetValue<string>("TokenSettings:Audience"),
                        ValidateAudience = true,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("TokenSettings:SecurityKey"))),
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero
                    };


                });


            // Add authorization and HttpContextAccessor
            builder.Services.AddAuthorization();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            // Return the updated builder
            return builder;
        }
    }
}
