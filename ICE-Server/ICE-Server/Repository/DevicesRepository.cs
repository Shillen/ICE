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
    public class DevicesRepository : IRepository<Device>
    {
        ICEContext context = new ICEContext();
        
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
            context.Devices.Add(item);
            context.SaveChanges();
            return true;
        }

        public bool Update(Device item, int[] ids)
        {
            context.Entry(item).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
            return true;
        }
        public bool Delete(Device item)
        {
            context.Devices.Remove(item);
            context.SaveChanges();
            return true;
        }


    }
}