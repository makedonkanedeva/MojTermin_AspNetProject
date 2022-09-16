using Microsoft.EntityFrameworkCore;
using MojTermin.Domain.DomainModels;
using MojTermin.Repository.Interface;
using MojTermin.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MojTermin.Repository.Implementation
{
    public class EmailRepository : IEmailRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<EmailMessage> messages;

        public EmailRepository(ApplicationDbContext context)
        {
            this.context = context;
            messages = context.Set<EmailMessage>();
        }

        public void Delete(EmailMessage emailMessage)
        {
            if (emailMessage == null)
            {
                throw new ArgumentNullException("emailMessage");
            }
            messages.Remove(emailMessage);
            context.SaveChanges();
        }

        public EmailMessage Get(Guid? id)
        {
            return messages.SingleOrDefault(m => m.Id == id);
        }

        public IEnumerable<EmailMessage> GetAll()
        {
            return messages.AsEnumerable();
        }

        public void Insert(EmailMessage emailMessage)
        {
            if (emailMessage == null)
            {
                throw new ArgumentNullException("emailMessage");
            }
            messages.Add(emailMessage);
            context.SaveChanges();
        }

        public void Update(EmailMessage emailMessage)
        {
            if (emailMessage == null)
            {
                throw new ArgumentNullException("emailMessage");
            }
            messages.Update(emailMessage);
            context.SaveChanges();
        }
    }
}
