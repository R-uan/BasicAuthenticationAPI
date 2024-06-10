using FluentValidation;

namespace BasicAuthenticationAPI.Helpers.Validator;

public class EndUserValidator : AbstractValidator<EndUserDTO> {
	public EndUserValidator() {
		RuleFor(enduser => enduser.Username).NotEmpty().WithMessage("Username is necessary");
		RuleFor(enduser => enduser.FirstName).NotEmpty().WithMessage("FirstName is necessary");
		RuleFor(enduser => enduser.LastName).NotEmpty().WithMessage("LastName is necessary");
		RuleFor(enduser => enduser.Password).NotEmpty().WithMessage("Password is necessary");
	}
}
