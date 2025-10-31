﻿using ConnectApp.Domain.Entities.Users;

namespace ConnectApp.Domain.Interfaces.Auths
{

    public interface IAuthRepository
    {
        Task<bool> GetByUsernameAsync(string login);
        Task<User?> GetByUserAsync(string login, string password);
    }
}