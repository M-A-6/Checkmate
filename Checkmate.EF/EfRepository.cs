using System;
using System.Collections.Generic;

using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkmate.EF
{
    public class EfRepository<T>  where T : class
    {
        private EfDbContext _context = null;
        private DbSet<T> table = null;

        public EfRepository()
        {
            this._context = new EfDbContext();
            table = _context.Set<T>();
        }
        public T GetById(int id)
        {
            return table.Find(id);            
        }

        public IQueryable<T> GetAll()
        {
            return table;
        }

        public T Insert(T obj)
        {
            table.Add(obj);
            Save();
            return obj;
        }

        public T Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            Save();
            return obj;
        }

        public bool Delete(object id)
        {
            bool retVal = false;
            T existingRecord = table.Find(id);
            if (existingRecord != null)
            {
                table.Remove(existingRecord);
                retVal = true;
            }
            return retVal;         
        }


        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
