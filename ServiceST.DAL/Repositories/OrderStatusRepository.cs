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
    public class OrderStatusRepository : IRepository<OrderStatus>
    {
        private SSModel db;

        public OrderStatusRepository(SSModel context)
        {
            this.db = context;
        }

        public IEnumerable<OrderStatus> GetAll()
        {
            return db.OrderStatus;
        }

        public OrderStatus Get(int id)
        {
            return db.OrderStatus.Find(id);
        }

        public void Create(OrderStatus orderStatus)
        {
            db.OrderStatus.Add(orderStatus);
        }

        public void Update(OrderStatus orderStatus)
        {
            db.Entry(orderStatus).State = EntityState.Modified;
        }

        public IEnumerable<OrderStatus> Find(Func<OrderStatus, Boolean> predicate)
        {
            return db.OrderStatus.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            OrderStatus orderStatus = db.OrderStatus.Find(id);
            if (orderStatus != null)
                db.OrderStatus.Remove(orderStatus);
        }
    }
}
