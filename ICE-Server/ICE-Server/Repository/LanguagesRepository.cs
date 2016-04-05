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
    public class LanguagesRepository : IRepository<Language>
    {
        private ICEContext context = new ICEContext();
        public LanguagesRepository(ICEContext context)
        {
            this.context = context;
        }

        public IEnumerable<Language> GetAll()
        {
            return context.Languages;
        }

        public Language Get(int id)
        {
            var result = (from r in context.Languages where r.ID == id select r).FirstOrDefault();
            return result;
        }

        public bool Insert(Language item)
        {
            context.Languages.Add(item);
            context.SaveChanges();
            return true;
        }

        public bool Update(Language item, int[] ids)
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
        public bool Delete(Language item)
        {
            if (checkEntry(item.ID) == false)
            {
                return false;
            }

            context.Languages.Remove(item);
            context.SaveChanges();

            return true;
        }

        private bool checkEntry (int id)
        {
            return context.Languages.Count(e => e.ID == id) > 0;
        }


    }
}