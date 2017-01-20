using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using ServiceST.DAL.EF;
using ServiceST.DAL.Entities;
using ServiceST.DAL.Interfaces;

namespace ServiceST.DAL.Repositories
{
    public class CarRepository : IRepository<Car>
    {
        private SSModel db;

        public CarRepository(SSModel context)
        {
            this.db = context;
        }

        public IEnumerable<Car> GetAll()
        {
            return db.Car.Include(c => c.Client);
        }

        public Car Get(int id)
        {
            return db.Car.Find(id);
        }

        public void Create(Car car)
        {
            db.Car.Add(car);
        }

        public void Update(Car car)
        {
            db.Entry(car).State = EntityState.Modified;
        }

        public IEnumerable<Car> Find(Func<Car, Boolean> predicate)
        {
            return db.Car.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Car car = db.Car.Find(id);
            if (car != null)
                db.Car.Remove(car);
        }
    }
}
