using System;
using System.Collections.Generic;
using Entities;

namespace BLL.Services.Interfaces
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        User GetUserById(int id);
        User GetUserByUsername(string username);
        
        void AddUser(User user, string password);
        void UpdateUser(User user);
        void UpdateUserPassword(int userId, string newPassword);
        void DeleteUser(int id);
        
        bool IsUsernameUnique(string username, int? excludeUserId = null);
        bool AuthenticateUser(string username, string password, out User user);
        string HashPassword(string password);
    }
} 