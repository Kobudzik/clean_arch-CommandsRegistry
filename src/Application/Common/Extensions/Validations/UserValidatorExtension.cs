﻿using FluentValidation;

namespace CommandsRegistry.Application.Common.Extensions.Validations
{
    internal static class UserValidatorExtension
    {
        private const string WrongUserNameMessage =
            "Username should be alphanumeric text having a length of 3 to 16";

        private const string UserNameRegex = "^[A-Za-z0-9_-]{3,16}$";

        private const string SpecialCharacters = "!\"#$%&'()*+,\\-.\\/:;<=>?@\\[\\]\\^_`{|}~";

        private const string PasswordRegex =
            "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[A-Za-z\\d" + SpecialCharacters + "]{6,25}$";

        private const string InvalidFormatMessage = "is in invalid format";

        #region Custom rules
        public static IRuleBuilderOptions<T, string> Username<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Matches(UserNameRegex).WithMessage(WrongUserNameMessage);
        }

        public static IRuleBuilderOptions<T, string> MustBeCorrectPasswordFormat<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Matches(PasswordRegex).WithMessage($"Password {InvalidFormatMessage}");
        }

        public static IRuleBuilderOptions<T, string> MustNotFakeAdmin<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Must(x => !x.Contains("admin")).WithMessage("Not valid format");
        }
        #endregion
    }
}