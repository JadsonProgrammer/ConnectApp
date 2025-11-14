namespace ConnectApp.Domain.Entities.Users
{
    public static class UserMapper
    {
        public static User MapForInsert(User user, Func<string, string> hashPasswordFunc)
        {
            var now = DateTime.UtcNow;
            user.Id = user.Id == Guid.Empty ? Guid.NewGuid() : user.Id;
            user.Code = user.Code == 0 ? new Random().Next(1000, 9999) : user.Code;

            user.CPF = user.CPF?.Replace(".", "").Replace("-", "") ?? string.Empty;
            user.Password = hashPasswordFunc(user.Password);

            user.TypeCode = user.TypeCode == 0 ? 1 : user.TypeCode;
            user.TypeName ??= "Default";
            user.ProfileCode = user.ProfileCode == 0 ? 1 : user.ProfileCode;
            user.ProfileName ??= "Padrão";
            user.StatusCode = user.StatusCode == 0 ? 1 : user.StatusCode;
            user.StatusName ??= "Ativo";

            user.LastAccess = user.LastAccess == default ? now : user.LastAccess;
            user.AccessCount = user.AccessCount < 0 ? 0 : user.AccessCount;

            user.Avatar ??= "";
            user.Note ??= "";

            user.BrokerId = user.BrokerId == Guid.Empty ? Guid.NewGuid() : user.BrokerId;
            user.AccountId = user.AccountId == Guid.Empty ? Guid.NewGuid() : user.AccountId;

            user.CreationDate = now;
            user.CreationUserId = user.CreationUserId == Guid.Empty ? Guid.NewGuid() : user.CreationUserId;
            user.CreationUserName ??= "sistema";

            user.ChangeDate = now;
            user.ChangeUserId = user.ChangeUserId == Guid.Empty ? Guid.NewGuid() : user.ChangeUserId;
            user.ChangeUserName ??= "sistema";

            user.ExclusionDate = default;
            user.ExclusionUserId = Guid.Empty;
            user.ExclusionUserName ??= "sistema";

            user.RecordStatus = true;

            user.IsActive = true;



            return user;
        }

        public static User MapToSearch(User user)
        {
            return new User
            {
                Id = user.Id,
                Code = user.Code,
                CPF = user.CPF!,
                Name = user.Name,
                AccessKey = user.AccessKey,
                //Emails = user.Emails!,
                //Phones = user.Phones!,

                TypeCode = user.TypeCode ?? 0,
                TypeName = user.TypeName!,
                ProfileCode = user.ProfileCode ?? 0,
                ProfileName = user.ProfileName!,
                StatusCode = user.StatusCode ?? 0,
                StatusName = user.StatusName!,

                LastAccess = user.LastAccess ?? DateTime.MinValue,
                AccessCount = user.AccessCount ?? 0,

                Avatar = user.Avatar ?? string.Empty,
                Note = user.Note ?? string.Empty,

                BrokerId = user.BrokerId ?? Guid.Empty,
                AccountId = user.AccountId ?? Guid.Empty,

                CreationDate = user.CreationDate ?? DateTime.MinValue,
                CreationUserId = user.CreationUserId ?? Guid.Empty,
                CreationUserName = user.CreationUserName!,

                ChangeDate = user.ChangeDate ?? DateTime.MinValue,
                ChangeUserId = user.ChangeUserId ?? Guid.Empty,
                ChangeUserName = user.ChangeUserName!
            };
        }

        public static List<User> MapToList(List<User> users)
        {
            return [.. users.Select(MapToSearch)];
        }
    }
}