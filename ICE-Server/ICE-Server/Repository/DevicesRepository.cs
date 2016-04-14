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
    public class DevicesRepository : IRepository<Device>
    {
        private ICEContext context = new ICEContext();
        public DevicesRepository(ICEContext context)
        {
            this.context = context;
        }

        public IEnumerable<Device> GetAll()
        {
            return context.Devices;
        }

        public Device Get(int id)
        {
            var result = (from r in context.Devices where r.ID == id select r).FirstOrDefault();
            return result;
        }

        public bool Insert(Device item)
        {
            if (context.Devices.Count(e => e.DeviceID == item.DeviceID) > 0)
            {
                return false;
            }

            context.Devices.Add(item);
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

        public bool Update(Device item, int[] ids)
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
        public bool Delete(Device item)
        {
            if (checkEntry(item.ID) == false)
            {
                return false;
            }

            context.Devices.Remove(item);
            context.SaveChanges();

            return true;
        }

        private bool checkEntry(int id)
        {
            return context.Devices.Count(e => e.ID == id) > 0;
        }
    }
}