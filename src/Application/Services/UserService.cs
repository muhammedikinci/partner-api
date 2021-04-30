using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Application.Auth.Entities;
using Application.Auth.Helpers;
using Application.Interfaces;
using System.Security.Cryptography;
using Repository.Interfaces;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, IUserRepository userRepository)
        {
            _appSettings = appSettings.Value;
            this.userRepository = userRepository;
        }

        public User Authenticate(string username, string password)
        {
            MD5 md5Hash = MD5.Create();
            string hashedPass = GetMd5Hash(md5Hash, password);
            Domain.Models.User user = userRepository.Get(x => x.UserName == username && x.Password == hashedPass).FirstOrDefault();

            if (user == null)
                return null;
 
            User appUser = new User();
            appUser.Id = user.Id;
            appUser.UserName = user.UserName;
            appUser.Name = user.Name;
            appUser.Role = user.Role;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, appUser.Id.ToString()),
                    new Claim(ClaimTypes.Role, appUser.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            appUser.Token = tokenHandler.WriteToken(token);

            return appUser.WithoutPassword();
        }

        private string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public IQueryable<Domain.Models.User> GetAll()
        {
            return userRepository.Get();
        }

        public Domain.Models.User GetById(string id)
        {
            return userRepository.GetByIdAsync(id).Result;
        }

        public bool Add(Domain.Models.User user)
        {
            Domain.Models.User o = userRepository.AddAsync(user).Result;
            return o != null;
        }

        public bool Update(string id, Domain.Models.User user)
        {
            user.Id = id;
            Domain.Models.User o = userRepository.UpdateAsync(id, user).Result;
            return o != null;
        }

        public bool Delete(string id)
        {
            Domain.Models.User o = userRepository.DeleteAsync(id).Result;
            return o != null;
        }
    }
}