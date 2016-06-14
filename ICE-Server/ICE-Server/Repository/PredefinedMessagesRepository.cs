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

        public IEnumerable<PredefinedMessageTranslated> GetPredefinedMessageTranslations(int id)
        {
            var result = context.PredefinedMessagesTranslated.Where(x => x.PredefinedMessageID == id);
            return result;
        }

        public bool Insert(PredefinedMessage item)
        {
            return true;
        }

        public bool Insert(PredefinedMessageItem item)
        {
            PredefinedMessage predefMessage = new PredefinedMessage();
            predefMessage.Name = item.Name;
            predefMessage.EmergencyID = item.EmergencyId;
            context.PredefinedMessages.Add(predefMessage);
            context.SaveChanges();

            int id = predefMessage.ID;

            foreach (var translation in item.Translations)
            {
                translation.PredefinedMessageID = id;
                context.PredefinedMessagesTranslated.Add(translation);
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
            return true;

        }

        public bool Update(PredefinedMessageItem item, int[] ids)
        {
            if (checkEntry(ids[0]) == false)
            {
                return false;
            }
            PredefinedMessage predefMessage = new PredefinedMessage();
            predefMessage.ID = ids[0];
            predefMessage.Name = item.Name;
            try
            {
                context.Entry(predefMessage).State = System.Data.Entity.EntityState.Modified;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            foreach (PredefinedMessageTranslated translation in item.Translations)
            {
                try
                {
                    context.Entry(translation).State = System.Data.Entity.EntityState.Modified;
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return true;

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