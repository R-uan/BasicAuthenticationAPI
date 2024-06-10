using Microsoft.AspNetCore.Mvc;

namespace BasicAuthenticationAPI.Application.Controller;

[ApiController]
[Route("enduser")]
public class EndUserController(IEndUserService service) : ControllerBase {
	private readonly IEndUserService _service = service;
	//
	//	Summary:
	//		Finds the entity associated with the username. Hides the password;
	//	Returns: 
	//		200 (Ok) Status Code with the associated entity;
	//		500 (Internal Server Error) Statuc Code with the error message;
	[HttpGet("{username}")]
	public async Task<ActionResult<EndUserDTO>> GetByUsername(string username) {
		try {
			var user = await _service.FindByUsername(username);
			return user == null ? NotFound() : Ok(user);
		} catch (Exception ex) {
			return StatusCode(500, ex.Message);
		}
	}
	//
	//	Summary:
	//		Lists all registered EndUsers. Hides the password;
	//	Returns:
	//		200 (Ok) Status Code with a List of EndUserDTO;
	//		500 (Internal Server Error) Statuc Code with the error message;
	[HttpGet]
	public async Task<ActionResult<List<EndUserDTO>>> GetAll() {
		try {
			var users = await _service.Find();
			return Ok(users);
		} catch (System.Exception ex) {
			return StatusCode(500, ex.Message);
			throw;
		}
	}
}
