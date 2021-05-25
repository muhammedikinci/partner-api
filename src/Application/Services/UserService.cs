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
using Microsoft.AspNetCore.Http;
using Application.AppException.Exceptions;
using Application.AppException;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly AppSettings _appSettings;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserService(IOptions<AppSettings> appSettings, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _appSettings = appSettings.Value;
            this.userRepository = userRepository;
            this.httpContextAccessor = httpContextAccessor;
        }

        public User Authenticate(string username, string password)
        {
            MD5 md5Hash = MD5.Create();
            string hashedPass = GetMd5Hash(md5Hash, password);
            Domain.Models.User user = userRepository.Get(x => x.UserName == username && x.Password == hashedPass).FirstOrDefault();

            if (user == null)
                throw new UserNotValidException();
 
            User appUser = new User();
            appUser.Id = user.Id;
            appUser.UserName = user.UserName;
            appUser.Name = user.Name;
            appUser.Role = user.Role;
            appUser.PartnerId = user.PartnerId;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, appUser.Id.ToString()),
                    new Claim(ClaimTypes.Role, appUser.Role),
                    new Claim(ClaimTypes.UserData, appUser.PartnerId),
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
            Domain.Models.User user = userRepository.GetByIdAsync(id).Result;

            if (user == null)
                throw new NotFoundException();
            
            return user;
        }

        public Domain.Models.User GetMyData()
        {
            var user = GetUserDataFromToken();

            if (user == null)
                throw new UserNotValidException();

            Domain.Models.User _user = userRepository.Get(u => u.Id == user.Id).FirstOrDefault();

            if (_user == null)
                throw new NotFoundException();

            return _user;
        }

        public bool Add(Domain.Models.User user)
        {
            Domain.Models.User o = userRepository.AddAsync(user).Result;
            return o != null;
        }

        public bool AddPartner(Domain.Models.User user)
        {
            MD5 md5Hash = MD5.Create();
            string hashedPass = GetMd5Hash(md5Hash, user.Password);

            user.Role = Role.User;
            user.Password = hashedPass;

            Domain.Models.User o = userRepository.AddAsync(user).Result;
            return o != null;
        }

        public bool Update(string id, Domain.Models.User user)
        {
            var _user = userRepository.GetByIdAsync(id).Result;

            if (_user == null) 
                throw new UserNotValidException();

            if (user.Password != null) {
                MD5 md5Hash = MD5.Create();
                string hashedPass = GetMd5Hash(md5Hash, user.Password);
                _user.Password = hashedPass;
            }

            _user.Name = user.Name;
            _user.UserName = user.UserName;
            _user.PartnerId = user.PartnerId;
            _user.TradeRegistryTitle = user.TradeRegistryTitle;
            _user.RegistrationNumber = user.RegistrationNumber;
            _user.Address = user.Address;
            _user.OwnerName = user.OwnerName;
            _user.MobileNumber = user.MobileNumber;
            _user.CompanyPhoneNumber = user.CompanyPhoneNumber;
            _user.TaxNumber = user.TaxNumber;
            _user.Email = user.Email;

            Domain.Models.User o = userRepository.UpdateAsync(id, _user).Result;
            return o != null;
        }

        public bool UpdateMyData(Domain.Models.User user)
        {
            var _user = GetUserDataFromToken();

            if (_user == null)
                throw new UserNotValidException();

            if (user.Password != null) 
            {
                string trimmedPassword = user.Password.Trim();

                if (!String.IsNullOrEmpty(trimmedPassword))
                {
                    MD5 md5Hash = MD5.Create();
                    string hashedPass = GetMd5Hash(md5Hash, user.Password);
                    _user.Password = hashedPass;
                }
                else
                {
                    throw new PasswordException(ExceptionConstants.PASSWORD_WHITESPACES);
                }
            }

            _user.Name = user.Name;
            _user.UserName = user.UserName;
            _user.PartnerId = user.PartnerId;
            _user.TradeRegistryTitle = user.TradeRegistryTitle;
            _user.RegistrationNumber = user.RegistrationNumber;
            _user.Address = user.Address;
            _user.OwnerName = user.OwnerName;
            _user.MobileNumber = user.MobileNumber;
            _user.CompanyPhoneNumber = user.CompanyPhoneNumber;
            _user.TaxNumber = user.TaxNumber;
            _user.Email = user.Email;

            Domain.Models.User o = userRepository.UpdateAsync(_user.Id, _user).Result;
            return o != null;
        }

        public bool Delete(string id)
        {
            Domain.Models.User o = userRepository.DeleteAsync(id).Result;
            return o != null;
        }

        public Domain.Models.User GetUserDataFromToken()
        {
            ClaimsIdentity identity = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var idClaim = identity.Claims.First(c => c.Type == ClaimTypes.Name);

            return userRepository.GetByIdAsync(idClaim.Value).Result;
        }
    }
}