using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICE_Server.Interfaces
{
    public interface IRepository<T> where T : class
    {
       IEnumerable<T> GetAll();

        T Get(int ids);

        bool Insert(T item);

        bool Update(T item, params int[] ids);

        bool Delete(T item);
    }
}
