﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Concurrent;
using ICE_Server.Interfaces;
using ICE_Server.Models;
using ICE_Server.DAL;


namespace ICE_Server.Repository
{
    public class BroadcastsRepository : IRepository<Broadcast>
    {
        ICEContext context = new ICEContext();
        
        public IEnumerable<Broadcast> GetAll()
        {
            return context.Broadcasts;
        }

        public Broadcast Get(int ids)
        {
            var result = (from r in context.Broadcasts where r.ID == ids select r).FirstOrDefault();
            return result;
        }

        public bool Insert(Broadcast item)
        {
            context.Broadcasts.Add(item);
            context.SaveChanges();
            return true;
        }

        public bool Update(Broadcast item, int[] ids)
        {
            context.Entry(item).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
            return true;
        }
        public bool Delete(Broadcast item)
        {
            context.Broadcasts.Remove(item);
            context.SaveChanges();
            return true;
        }


    }
}