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
    public class BuildingsRepository : IRepository<Building>
    {
        private ICEContext context = new ICEContext();
        public BuildingsRepository(ICEContext context)
        {
            this.context = context;
        }

        public IEnumerable<Building> GetAll()
        {
            return context.Buildings;
        }

        public Building Get(int id)
        {
            var result = (from r in context.Buildings where r.ID == id select r).FirstOrDefault();
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
        public bool Delete(Building item)
        {
            if (checkEntry(item.ID) == false)
            {
                return false;
            }

            context.Buildings.Remove(item);
            context.SaveChanges();

            return true;
        }

        private bool checkEntry(int id)
        {
            return context.Buildings.Count(e => e.ID == id) > 0;
        }


    }
}