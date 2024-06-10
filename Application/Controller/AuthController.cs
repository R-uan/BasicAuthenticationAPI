using System.Text;
using BasicAuthenticationAPI.Helpers.Validator;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace BasicAuthenticationAPI;

[Route("auth")]
[ApiController]
public class AuthController(IAuthService service, IValidator<EndUserDTO> validator) : ControllerBase {
	private readonly IValidator<EndUserDTO> _validate = validator;

	private readonly IAuthService _service = service;
	//
	//	Summary:
	//		Tries to autheticate a user based on the Basic Authorization header;
	//	Returns:
	//		200 (Ok) with a JWT token if authentication is sucessful;
	//		400 (Bad Request) if no authorization header is found;
	//		500 (Internal Server Error) with error message in case of a unexpected error;
	[HttpGet]
	public async Task<ActionResult<string>> Login() {
		try {
			if (Request.Headers.ContainsKey("Authorization")) {
				var authorization = Request.Headers.Authorization.ToString();
				var auth_token = authorization.Substring("Basic ".Length).Trim();
				var credentialBytes = Convert.FromBase64String(auth_token);
				var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
				var username = credentials[0];
				var password = credentials[1];
				var token = await _service.Authenticate(username, password);
				return token == null ? Unauthorized() : Ok(token);
			} else return BadRequest("No credentials provided");
		} catch (System.Exception ex) {
			return StatusCode(500, ex.Message);
			throw;
		}
	}
	//
	//	Summary:
	//		Endpoint to test a valid JWT token;
	//	Returns:
	//		200 (OK) with "Authenticated" if the authentication is sucessful;
	//		401 (Unauthorized) if the token is not valid;
	[Authorize]
	[HttpGet("verify")]
	public ActionResult<string> VerifyJwtAuthentication() {
		return Ok("Authenticated");
	}
	//
	//	Summary:
	//		Validates the data received from the body.
	//		Attempts to register a EndUser in the datbase.
	//	Returns:
	//		200 (OK) with the saved EndUser entity if successful;
	//		500 (Internal Server Error) if unable to register;
	[HttpPost]
	public async Task<ActionResult<EndUserDTO>> Register([FromBody] EndUserDTO endUser) {
		try {
			var validation = _validate.Validate(endUser);
			if (!validation.IsValid) return BadRequest(validation.Errors);
			return await _service.Register(endUser);
		} catch (System.Exception ex) {
			return StatusCode(500, ex.Message);
			throw;
		}
	}
}
