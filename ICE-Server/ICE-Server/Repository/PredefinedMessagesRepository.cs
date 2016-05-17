using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Concurrent;
using ICE_Server.Interfaces;
using ICE_Server.Models;
using ICE_Server.DAL;
using System.Data.Entity.Infrastructure;

namespace ICE_Server.Repository
{
    public class PredefinedMessagesRepository : IRepository<PredefinedMessage>
    {
        private ICEContext context = new ICEContext();
        public PredefinedMessagesRepository(ICEContext context)
        {
            this.context = context;
        }

        public IEnumerable<PredefinedMessage> GetAll()
        {
            return context.PredefinedMessages;
        }
        public IEnumerable<PredefinedMessageTranslated> GetAllTranslated()
        {
            return context.PredefinedMessagesTranslated;
        }

        public PredefinedMessage Get(int id)
        {
            var result = (from r in context.PredefinedMessages where r.ID == id select r).FirstOrDefault();
            return result;
        }

        public PredefinedMessageTranslated GetPredefinedMessageTranslations(int id)
        {
            var result = (from r in context.PredefinedMessagesTranslated where r.PredefinedMessageID == id select r).FirstOrDefault();
            return result;
        }

        public bool Insert(PredefinedMessage item)
        {
            return true;
        }

        public bool InsertTranslations(List<PredefinedMessageTranslated> itemList)
        {
            PredefinedMessage predefinedMessage = new PredefinedMessage();
            context.PredefinedMessages.Add(predefinedMessage);
            context.SaveChanges();

            int id = predefinedMessage.ID;

            foreach (PredefinedMessageTranslated translationItem in itemList)
            {
                translationItem.PredefinedMessageID = id;
                context.PredefinedMessagesTranslated.Add(translationItem);
            }

            try
            {
                context.SaveChanges();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;

            }
        }

        public bool Update(PredefinedMessage item, int[] ids)
        {
            if (checkEntry(item.ID) == false)
            {
                return false;
            }

            context.Entry(item).State = System.Data.Entity.EntityState.Modified;

            try
            {
                context.SaveChanges();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;

            }

        }
        public bool Delete(PredefinedMessage item)
        {
            if (checkEntry(item.ID) == false)
            {
                return false;
            }

            context.PredefinedMessages.Remove(item);
            context.PredefinedMessagesTranslated.RemoveRange(context.PredefinedMessagesTranslated.Where(x => x.PredefinedMessageID == item.ID));
            try
            {
                context.SaveChanges();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;

            }
        }

        private bool checkEntry(int id)
        {
            return context.PredefinedMessages.Count(e => e.ID == id) > 0;
        }


    }
}