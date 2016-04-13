﻿using System;
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

        public PredefinedMessage Get(int id)
        {
            var result = (from r in context.PredefinedMessages where r.ID == id select r).FirstOrDefault();
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
            context.SaveChanges();

            return true;
        }

        private bool checkEntry(int id)
        {
            return context.PredefinedMessages.Count(e => e.ID == id) > 0;
        }


    }
}