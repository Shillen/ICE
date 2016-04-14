﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Concurrent;
using ICE_Server.Interfaces;
using ICE_Server.Models;
using ICE_Server.DAL;
using System.Data.Entity.Infrastructure;
using ICE_Server.PushNotifications;

namespace ICE_Server.Repository
{
    public class BroadcastsRepository : IRepository<Broadcast>
    {
        private ICEContext context = new ICEContext();
        private Pushmessage pushNotification;

        public BroadcastsRepository(ICEContext context)
        {
            this.context = context;
        }

        public IEnumerable<Broadcast> GetAll()
        {
            return context.Broadcasts;
        }

        public Broadcast Get(int id)
        {
            var result = (from r in context.Broadcasts where r.ID == id select r).FirstOrDefault();
            return result;
        }

        public bool Insert(Broadcast item)
        {
            context.Broadcasts.Add(item);
            try
            {

                context.SaveChanges();
                this.pushNotification = new Pushmessage("Swampmonster", item.Message);
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;

            }
        }

        public bool Update(Broadcast item, int[] ids)
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
        public bool Delete(Broadcast item)
        {
            if (checkEntry(item.ID) == false)
            {
                return false;
            }

            context.Broadcasts.Remove(item);
            context.SaveChanges();

            return true;
        }

        private bool checkEntry(int id)
        {
            return context.Broadcasts.Count(e => e.ID == id) > 0;
        }


    }
}