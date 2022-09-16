using MojTermin.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MojTermin.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<MojTerminUser> GetAll();
        MojTerminUser Get(string id);
        void Insert(MojTerminUser entity);
        void Update(MojTerminUser entity);
        void Delete(MojTerminUser entity);
    }
}
