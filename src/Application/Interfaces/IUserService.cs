using System;
using System.Collections.Generic;
using Application.Auth.Entities;
using System.Linq.Expressions;
using System.Linq;

namespace Application.Interfaces
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IQueryable<Domain.Models.User> GetAll();
        Domain.Models.User GetById(string id);
        bool Add(Domain.Models.User user);
        bool AddPartner(Domain.Models.User user);
        bool Update(string id, Domain.Models.User user);
        bool Delete(string id);
    }
}