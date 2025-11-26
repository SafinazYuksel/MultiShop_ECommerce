using FluentValidation;
using MultiShop.DtoLayer.IdentityDtos.RegisterDto;

namespace MultiShop.WebUI.FluentValidation.IdentityServerValidator
{
    public class RegisterValidator : AbstractValidator<CreateRegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Kullanıcı adı boş geçilemez.")
                .MinimumLength(5).MaximumLength(25).WithMessage("Kullanıcı adı en az 5 en fazla 25 karakter olmalıdır.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ad kısmı boş geçilemez.");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Soyad kısmı boş geçilemez.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email adresi gereklidir.")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");

            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifre boş olamaz.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,}$")
                .WithMessage("Şifre en az 1 büyük harf, 1 küçük harf, 1 rakam ve 1 özel karakter içermelidir.");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Şifrelerin uyuştuğundan emin olun.");
        }
    }
}
