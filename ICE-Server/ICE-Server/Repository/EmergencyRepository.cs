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
    public class EmergencyRepository : IRepository<Emergency>
    {
        private ICEContext context = new ICEContext();
        public EmergencyRepository(ICEContext context)
        {
            this.context = context;
        }

        public IEnumerable<Emergency> GetAll()
        {
            return context.Emergency;
        }

        public IEnumerable<EmergencyTranslated> GetAllTranslated()
        {
            return context.EmergencyTranslated;
        }

        public Emergency Get(int id)
        {
            var result = (from r in context.Emergency where r.ID == id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable<EmergencyTranslated> GetEmergencyTranslations(int id)
        {
            var result = context.EmergencyTranslated.Where(x => x.EmergencyID == id);
            return result;
        }

        public bool Insert(Emergency item)
        {
            return true;
        }

        public bool InsertTranslations(List<EmergencyTranslated> itemList)
        {
            Emergency emergency = new Emergency();
            context.Emergency.Add(emergency);
            context.SaveChanges();

            int id = emergency.ID;

            foreach (EmergencyTranslated translationItem in itemList)
            {
                translationItem.EmergencyID = id;
                context.EmergencyTranslated.Add(translationItem);
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

        

        public bool Update(Emergency item, int[] ids)
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
        public bool Delete(Emergency item)
        {
            if (checkEntry(item.ID) == false)
            {
                return false;
            }

            context.Emergency.Remove(item);
            context.EmergencyTranslated.RemoveRange(context.EmergencyTranslated.Where(x => x.EmergencyID == item.ID));
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
            return context.Emergency.Count(e => e.ID == id) > 0;
        }


    }
}