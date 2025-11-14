using System;
using System.Linq;

namespace ConnectApp.Domain.Entities.Accounts
{
    public partial class Account
    {
        public static class AccountSetParams
        {
            private static readonly Guid SystemUserId = Guid.Parse("00000000-0000-0000-0000-000000000000");
            private const int AccountNameMinLength = 2;
            private const int AccountNameMaxLength = 100;
            private const int UserNameMaxLength = 100;
            private const int TemaMaxLength = 50;
            private const int UrlMaxLength = 500;
            private static readonly string[] TemasPermitidos = { "default", "dark", "light", "blue", "green" };

            public static Account Create(
                string accountName,
                Guid creationUserId,
                string? creationUserName,
                string? temaPadrao,
                string? urlLogo,
                string? urlIcone,
                string? urlImagemLogin,
                string? urlImagemDashboard)
            {
                ValidateAccountName(accountName);
                ValidateUserId(creationUserId, nameof(creationUserId));

                return new Account
                {
                    Id = GenerateValidAccountId(),
                    Name = accountName.Trim(),
                    Ativa = true,
                    TemaPadrao = ValidateAndFormatTemaPadrao(temaPadrao),
                    UrlLogo = ValidateAndFormatUrl(urlLogo),
                    UrlIcone = ValidateAndFormatUrl(urlIcone),
                    UrlImagemLogin = ValidateAndFormatUrl(urlImagemLogin),
                    UrlImagemDashboard = ValidateAndFormatUrl(urlImagemDashboard),
                    CreationDate = DateTime.UtcNow,
                    CreationUserId = creationUserId != Guid.Empty ? creationUserId : SystemUserId,
                    CreationUserName = FormatUserName(creationUserName),
                    RecordStatus = true
                };
            }

            public static void Update(
                Account account,
                string accountName,
                string? temaPadrao,
                string? urlLogo,
                string? urlIcone,
                string? urlImagemLogin,
                string? urlImagemDashboard,
                Guid changeUserId,
                string? changeUserName)
            {
                ValidateAccountName(accountName);
                ValidateUserId(changeUserId, nameof(changeUserId));

                account.Name = accountName.Trim();
                account.TemaPadrao = ValidateAndFormatTemaPadrao(temaPadrao);
                account.UrlLogo = ValidateAndFormatUrl(urlLogo);
                account.UrlIcone = ValidateAndFormatUrl(urlIcone);
                account.UrlImagemLogin = ValidateAndFormatUrl(urlImagemLogin);
                account.UrlImagemDashboard = ValidateAndFormatUrl(urlImagemDashboard);
                account.ChangeDate = DateTime.UtcNow;
                account.ChangeUserId = changeUserId;
                account.ChangeUserName = FormatUserName(changeUserName);
            }
            public static void Deactivate(Account account, Guid userId, string? userName)
            {
                ValidateUserId(userId, nameof(userId));

                account.Ativa = false;
                account.RecordStatus = false;
                account.ExclusionDate = DateTime.UtcNow;
                account.ExclusionUserId = userId;
                account.ExclusionUserName = FormatUserName(userName);
            }


            private static Guid GenerateValidAccountId()
            {
                var accountId = Guid.NewGuid();
                while (accountId == Guid.Empty)
                    accountId = Guid.NewGuid();
                return accountId;
            }

            internal static void ValidateAccountName(string accountName)
            {
                if (string.IsNullOrWhiteSpace(accountName))
                    throw new ArgumentException("O nome da conta é obrigatório.", nameof(accountName));

                var trimmedName = accountName.Trim();

                if (trimmedName.Length < AccountNameMinLength)
                    throw new ArgumentException($"O nome da conta deve ter pelo menos {AccountNameMinLength} caracteres.", nameof(accountName));

                if (trimmedName.Length > AccountNameMaxLength)
                    throw new ArgumentException($"O nome da conta não pode exceder {AccountNameMaxLength} caracteres.", nameof(accountName));
            }

            private static void ValidateUserId(Guid userId, string paramName)
            {
                if (userId == Guid.Empty)
                    throw new ArgumentException($"O ID do usuário ({paramName}) é obrigatório.", paramName);
            }

            private static string ValidateAndFormatTemaPadrao(string? temaPadrao)
            {
                if (string.IsNullOrWhiteSpace(temaPadrao))
                    return "default";

                var trimmedTema = temaPadrao.Trim();

                if (trimmedTema.Length > TemaMaxLength)
                    throw new ArgumentException($"O tema padrão não pode exceder {TemaMaxLength} caracteres.", nameof(temaPadrao));

                if (!TemasPermitidos.Contains(trimmedTema.ToLower()))
                    throw new ArgumentException($"Tema '{trimmedTema}' não é suportado.", nameof(temaPadrao));

                return trimmedTema;
            }

            private static string? ValidateAndFormatUrl(string? url)
            {
                if (string.IsNullOrWhiteSpace(url))
                    return null;

                var trimmedUrl = url.Trim();

                if (trimmedUrl.Length > UrlMaxLength)
                    throw new ArgumentException($"A URL não pode exceder {UrlMaxLength} caracteres.");

                if (!Uri.TryCreate(trimmedUrl, UriKind.Absolute, out var uriResult) ||
                    (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
                    throw new ArgumentException("A URL deve ser válida (http ou https).");

                return trimmedUrl;
            }

            private static string FormatUserName(string? userName)
            {
                return string.IsNullOrWhiteSpace(userName) ? "Sistema" : userName.Trim();
            }

           

            public static bool IsValidAccountName(string accountName)
            {
                try
                {
                    ValidateAccountName(accountName);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public static bool IsValidTema(string tema)
            {
                try
                {
                    ValidateAndFormatTemaPadrao(tema);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public static bool IsValidUrl(string url)
            {
                try
                {
                    ValidateAndFormatUrl(url);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public static string[] GetTemasPermitidos() => TemasPermitidos;

           
        }
    }
}