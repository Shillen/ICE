using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICE_Server.Models;

namespace ICE_Server.Interfaces
{

    public interface IBroadcastsRepository
    {
        void Add(Broadcasts item);
        IEnumerable<Broadcasts> GetAll();
        Broadcasts Find(string key);
        Broadcasts Remove(string key);
        void Update(Broadcasts item);
    }
}