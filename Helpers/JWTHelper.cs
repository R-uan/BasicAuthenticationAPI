using System.Text;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace BasicAuthenticationAPI;

public class JWTHelper(IOptions<JWTSettings> jwtSettings) {
	private readonly JWTSettings _jwtSettings = jwtSettings.Value;
	public string Generate(EndUser user) {
		try {
			var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
			var handler = new JwtSecurityTokenHandler();
			var credentials = new SigningCredentials(
				new SymmetricSecurityKey(key),
				SecurityAlgorithms.HmacSha256Signature
			);
			var tokenDescriptor = new SecurityTokenDescriptor {
				Issuer = _jwtSettings.Issuer,
				Subject = GenerateClaims(user),
				SigningCredentials = credentials,
				Expires = DateTime.UtcNow.AddHours(2),
			};
			var token = handler.CreateToken(tokenDescriptor);
			return handler.WriteToken(token);
		} catch (System.Exception e) {
			return e.Message;
		}
	}

	private static ClaimsIdentity GenerateClaims(EndUser user) {
		var ci = new ClaimsIdentity();
		ci.AddClaim(new Claim(ClaimTypes.Name, user.Username));
		return ci;
	}
}
