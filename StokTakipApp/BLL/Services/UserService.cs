using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using BLL.Services.Interfaces;
using DAL.Repositories;
using Entities;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAll().ToList();
        }

        public User GetUserById(int id)
        {
            return _userRepository.GetById(id);
        }

        public User GetUserByUsername(string username)
        {
            return _userRepository.Find(u => u.Username == username).FirstOrDefault();
        }

        public void AddUser(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Parola boş olamaz");

            if (!IsUsernameUnique(user.Username))
                throw new ArgumentException("Bu kullanıcı adı zaten kullanılıyor");

            user.PasswordHash = HashPassword(password);
            user.CreatedDate = DateTime.Now;
            
            _userRepository.Add(user);
            _userRepository.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            var existingUser = _userRepository.GetById(user.Id);
            if (existingUser == null)
                throw new Exception("Kullanıcı bulunamadı");

            // Parola hashini korumak için user nesnesinden kopyalamıyoruz
            existingUser.FullName = user.FullName;
            existingUser.Email = user.Email;
            existingUser.Role = user.Role;
            existingUser.IsActive = user.IsActive;

            _userRepository.Update(existingUser);
            _userRepository.SaveChanges();
        }

        public void UpdateUserPassword(int userId, string newPassword)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı");

            user.PasswordHash = HashPassword(newPassword);
            
            _userRepository.Update(user);
            _userRepository.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var user = _userRepository.GetById(id);
            if (user != null)
            {
                _userRepository.Remove(user);
                _userRepository.SaveChanges();
            }
        }

        public bool IsUsernameUnique(string username, int? excludeUserId = null)
        {
            if (excludeUserId.HasValue)
                return !_userRepository.Find(u => u.Username == username && u.Id != excludeUserId.Value).Any();
            
            return !_userRepository.Find(u => u.Username == username).Any();
        }

        public bool AuthenticateUser(string username, string password, out User user)
        {
            user = null;
            
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return false;

            var existingUser = _userRepository.Find(u => u.Username == username && u.IsActive).FirstOrDefault();
            if (existingUser == null)
                return false;

            string hashedPassword = HashPassword(password);
            if (existingUser.PasswordHash != hashedPassword)
                return false;

            // Giriş başarılı, kullanıcıyı güncelle
            existingUser.LastLoginDate = DateTime.Now;
            _userRepository.Update(existingUser);
            _userRepository.SaveChanges();

            user = existingUser;
            return true;
        }

        public string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
} 