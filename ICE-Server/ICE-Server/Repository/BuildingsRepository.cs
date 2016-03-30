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
    public class BuildingsRepository : IRepository<Building>
    {
        ICEContext context = new ICEContext();

        public IEnumerable<Building> GetAll()
        {
            return context.Buildings;
        }

        public Building Get(int ids)
        {
            var result = (from r in context.Buildings where r.ID == ids select r).FirstOrDefault();
            return result;
        }

        public bool Insert(Building item)
        {
            context.Buildings.Add(item);
            context.SaveChanges();
            return true;
        }

        public bool Update(Building item, int[] ids)
        {
            context.Entry(item).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
            return true;
        }
        public bool Delete(Building item)
        {
            context.Buildings.Remove(item);
            context.SaveChanges();
            return true;
        }


    }
}