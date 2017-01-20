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
    public class OrderRepository : IRepository<Order>
    {
        private SSModel db;

        public OrderRepository(SSModel context)
        {
            this.db = context;
        }

        public IEnumerable<Order> GetAll()
        {
            return db.Order.Include(o => o.Car).Include(o => o.Client).Include(o => o.OrderStatus);
        }

        public Order Get(int id)
        {
            return db.Order.Find(id);
        }

        public void Create(Order order)
        {
            db.Order.Add(order);
        }

        public void Update(Order order)
        {
            db.Entry(order).State = EntityState.Modified;
        }

        public IEnumerable<Order> Find(Func<Order, Boolean> predicate)
        {
            return db.Order.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Order order = db.Order.Find(id);
            if (order != null)
                db.Order.Remove(order);
        }
    }
}
