using System;
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
            return true;
        }


        public bool Insert(BroadcastItem item)
        {
            DateTime time = DateTime.Now;
            Broadcast broadcast = new Broadcast {
                Message = item.Message,
                EmergencyId = item.EmergencyId,
                PredefinedMessageID = item.PredefinedMessageID,
                BroadcastBuildings = new List<BroadcastBuilding>(),
                Time = time
            };
            context.Broadcasts.Add(broadcast);
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            int id = broadcast.ID;

            foreach (Building building in item.Buildings)
            {
                broadcast.BroadcastBuildings.Add(new BroadcastBuilding { BroadcastID = id, BuildingID = building.ID});
            }

            foreach (BroadcastBuilding BroadcastBuildingItem in broadcast.BroadcastBuildings)
            {
                context.BroadcastBuilding.Add(BroadcastBuildingItem);
            }

            context.Entry(broadcast).State = System.Data.Entity.EntityState.Modified;
            try
            {

                context.SaveChanges();
                this.pushNotification = new Pushmessage("New Broadcast", item.Message);
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
        public bool Delete(int id)
        {
            if (checkEntry(id) == false)
            {
                return false;
            }
            Broadcast bc = context.Broadcasts.Find(id);
            context.Broadcasts.Remove(bc);
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
            return context.Broadcasts.Count(e => e.ID == id) > 0;
        }


    }
}