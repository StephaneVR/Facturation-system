using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facturation.DAL.Entities;

namespace Facturation.DAL.Repository
{
    interface IRepository<T>
    {
        void Add(T t);
        T FinById(int? id);
        void Modify(T t);
        List<T> GetAll();
        
    }
}
