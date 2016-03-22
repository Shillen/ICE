using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Concurrent;
using ICE_Server.Interfaces;
using ICE_Server.Models;

namespace ICE_Server.Repository
{
    public class BroadcastsRepository : IBroadcastsRepository
    {
        static ConcurrentDictionary<string, Broadcasts> _broadcasts = new ConcurrentDictionary<string, Broadcasts>();

        public BroadcastsRepository()
        {
            Add(new Broadcasts { Message = "Item1" });
        }

        public IEnumerable<Broadcasts> GetAll()
        {
            return _broadcasts.Values;
        }

        public void Add(Broadcasts item)
        {
            item.Message = Guid.NewGuid().ToString();
            _broadcasts[item.Message] = item;
        }

        public Broadcasts Find(string key)
        {
            Broadcasts item;
            _broadcasts.TryGetValue(key, out item);
            return item;
        }

        public Broadcasts Remove(string key)
        {
            Broadcasts item;
            _broadcasts.TryGetValue(key, out item);
            _broadcasts.TryRemove(key, out item);
            return item;
        }

        public void Update(Broadcasts item)
        {
            _broadcasts[item.Message] = item;
        }
    }
}