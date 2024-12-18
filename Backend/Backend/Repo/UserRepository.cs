﻿using Backend.Data;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace Backend.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext db;

        public UserRepository(DataContext Db)
        {
            db = Db;
        }
        public async Task<User> Authenticate(string userName, string passwordText)
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.Username == userName);

            if (user == null || user.PasswordKey == null)
                return null;

            if (!MatchPasswordHash(passwordText, user.Password, user.PasswordKey))
                return null;

            return user;
        }

        private bool MatchPasswordHash(string passwordText, byte[] password, byte[] passwordKey)
        {
            using (var hmac = new HMACSHA512(passwordKey))
            {
                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordText));

                for (int i = 0; i < passwordHash.Length; i++)
                {
                    if (passwordHash[i] != password[i])
                        return false;
                }

                return true;
            }
        }

        public void Register(string userName, string password)
        {
            byte[] passwordHash, passwordKey;

            using (var hmac = new HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }

            User user = new User();
            user.Username = userName;
            user.Password = passwordHash;
            user.PasswordKey = passwordKey;

            db.Users.Add(user);
        }

        public async Task<bool> UserAlreadyExists(string userName)
        {
            return await db.Users.AnyAsync(x => x.Username == userName);
        }
    }
}
