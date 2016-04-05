using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Concurrent;
using ICE_Server.Interfaces;
using ICE_Server.Models;
using ICE_Server.DAL;


namespace ICE_Server.Repository
{
    public class PredefinedMessageRepository : IRepository<PredefinedMessage>
    {
        ICEContext context = new ICEContext();

        public IEnumerable<PredefinedMessage> GetAll()
        {
            return context.PredefinedMessages;
        }

        public PredefinedMessage Get(int ids)
        {
            var result = (from r in context.PredefinedMessages where r.ID == ids select r).FirstOrDefault();
            return result;
        }

        public bool Insert(PredefinedMessage item)
        {
            context.PredefinedMessages.Add(item);
            context.SaveChanges();
            return true;
        }

        public bool Update(PredefinedMessage item, int[] ids)
        {
            context.Entry(item).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
            return true;
        }
        public bool Delete(PredefinedMessage item)
        {
            context.PredefinedMessages.Remove(item);
            context.SaveChanges();
            return true;
        }


    }
}