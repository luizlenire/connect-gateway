using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Api.AppCore.API_Common.SeveralFunctions
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class Validation : ValidationAttribute
    {
        #region --> Public properties. <--

        public enum ValidationType
        {
            Cpf = 1,
            Email = 2,
            CEP = 3,
            UF = 4,
            InitialDate = 5,
            ExpirationDate = 6
        }

        #endregion -> Public properties. <--

        #region --> Private properties. <--

        private string DisapprovalMessage { get; set; }

        private ValidationType _ValidationType { get; set; }

        #endregion --> Private properties. <--

        #region --> Constructors. <--

        public Validation() { }

        public Validation(ValidationType validationType, string mensagem)
         : base(mensagem)
        {
            _ValidationType = validationType;
            DisapprovalMessage = mensagem;
        }

        #endregion --> Constructors. <--

        #region --> Protected methods. <--

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (_ValidationType == ValidationType.Cpf && IsCpf(value.ToString())) return ValidationResult.Success;
            else if (_ValidationType == ValidationType.Email && IsEmail(value.ToString())) return ValidationResult.Success;
            else if (_ValidationType == ValidationType.CEP && IsCEP(value.ToString())) return ValidationResult.Success;
            else if (_ValidationType == ValidationType.UF && IsUF(value.ToString())) return ValidationResult.Success;
            else if (_ValidationType == ValidationType.InitialDate && IsAllowedInitialDate((DateTime)value)) return ValidationResult.Success;
            else return new ValidationResult(DisapprovalMessage);
        }

        #endregion --> Protected methods. <--

        #region --> Public static methods. <--  

        public static bool IsCnpj(string value)
        {
            try
            {
                value = Masking.UndoCnpj(value);

                int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int soma;
                int resto;
                string digito;
                string tempCnpj;

                if (value.Length != 14) return false;

                tempCnpj = value.Substring(0, 12);
                soma = 0;

                for (int i = 0; i < 12; i++) soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

                resto = soma % 11;

                if (resto < 2) resto = 0;
                else resto = 11 - resto;

                digito = resto.ToString();
                tempCnpj += digito;
                soma = 0;

                for (int i = 0; i < 13; i++) soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

                resto = soma % 11;

                if (resto < 2) resto = 0;
                else resto = 11 - resto;

                digito += resto.ToString();

                return value.EndsWith(digito);
            }
            catch { }

            return default;
        }

        public static bool IsCpf(string value)
        {
            try
            {
                value = Masking.UndoCpf(value);

                int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                string tempCpf;
                string digito;
                int soma;
                int resto;

                if (value.Length != 11) return false;

                tempCpf = value.Substring(0, 9);
                soma = 0;

                for (int i = 0; i < 9; i++) soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

                resto = soma % 11;

                if (resto < 2) resto = 0;
                else resto = 11 - resto;

                digito = resto.ToString();
                tempCpf += digito;
                soma = 0;

                for (int i = 0; i < 10; i++) soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

                resto = soma % 11;

                if (resto < 2) resto = 0;
                else resto = 11 - resto;

                digito += resto.ToString();

                return value.EndsWith(digito);
            }
            catch { }

            return default;
        }

        public static bool IsEmail(string value)
        {
            if (value != default) value = value.ToUpper();

            try
            {
                MailAddress mailAddress = new(value);
                return mailAddress.Address == value;
            }
            catch { return default; }
        }

        public static bool IsCEP(string value)
        {
            Regex regex = new(@"^\d{5}-\d{3}$"); // 09660-006

            if (!regex.IsMatch(value))
            {
                regex = new(@"^\d{5}\d{3}$"); // 09660006

                if (!regex.IsMatch(value)) return default;
                else return true;
            }
            else return true;
        }

        public static bool IsUF(string value)
        {
            if (value != default) value = value.ToUpper();

            if (!string.IsNullOrEmpty(value))
            {
                return new string[]
                {
                "AC", "AL", "AP", "AM", "BA",
                "CE", "DF", "ES", "GO", "MA",
                "MT", "MS", "MG", "PA", "PB",
                "PR", "PE", "PI", "RJ", "RN",
                "RS", "RO", "RR", "SC", "SP",
                "SE", "TO"
                }.Contains(value.ToUpper());
            }
            return true;
        }

        public static bool IsAllowedInitialDate(DateTime value)
        {
            if (value <= Tools.GetDateTimeNow().Date) return true;

            return default;
        }

        #endregion --> Public static methods. <--
    }
}