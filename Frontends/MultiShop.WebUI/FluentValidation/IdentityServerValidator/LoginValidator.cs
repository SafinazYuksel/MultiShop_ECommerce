using FluentValidation;
using MultiShop.DtoLayer.IdentityDtos.LoginDto;

namespace MultiShop.WebUI.FluentValidation.IdentityServerValidator
{
    public class LoginValidator : AbstractValidator<CreateLoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Kullanıcı adı boş geçilemez.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre kısmı boş geçilemez.");
        }
    }
}
