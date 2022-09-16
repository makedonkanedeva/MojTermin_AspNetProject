using MojTermin.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MojTermin.Repository.Interface
{
    public interface IEmailRepository
    {
        IEnumerable<EmailMessage> GetAll();
        EmailMessage Get(Guid? id);
        void Insert(EmailMessage emailMessage);
        void Update(EmailMessage emailMessage);
        void Delete(EmailMessage emailMessage);
    }
}
