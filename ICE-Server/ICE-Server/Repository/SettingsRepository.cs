using ICE_Server.DAL;
using ICE_Server.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace ICE_Server.Repository
{
    public class SettingsRepository
    {
        private ICEContext context = new ICEContext();
        public SettingsRepository(ICEContext context)
        {
            this.context = context;
        }

        public IEnumerable<Settings> GetAll()
        {
            return context.Settings;
        }

        public bool Update(List<Settings> settings)
        {
            if (settings != null)
            {
                foreach (Settings item in settings)
                {
                    context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                }
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
    }
}