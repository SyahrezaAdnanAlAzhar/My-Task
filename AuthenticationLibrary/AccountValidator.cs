using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace AuthenticationLibrary
{
    public class AccountValidator : AbstractValidator<Account>
    {
        public AccountValidator()
        {
            RuleFor(account => account.userName).NotEmpty().WithMessage("Username harus diisi").Must(BeEndsWithDigit).WithMessage("Username harus diakhiri oleh angka");
            RuleFor(account => account.email).NotEmpty().WithMessage("Email harus diisi").Must(BeGmailAddress).WithMessage("Email harus diakhiri dengan @gmail.com");
            RuleFor(account => account.password).NotEmpty().WithMessage("Password harus diisi").MinimumLength(8).WithMessage("Password minimal 8 karakter")
                .Matches("[A-Z]").WithMessage("Password harus memiliki minimal 1 huruf kapital")
                .Matches("[0-9]").WithMessage("Password harus memiliki minimal 1 angka");
        }

        public ValidationResult Validate(Account newAccount, string ruleSet)
        {
            throw new NotImplementedException();
        }

        private bool BeEndsWithDigit(string username)
        {
            if (string.IsNullOrEmpty(username))
                return false;

            return char.IsDigit(username[^1]);
        }

        private bool BeGmailAddress(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            return email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase);
        }
    }

}
