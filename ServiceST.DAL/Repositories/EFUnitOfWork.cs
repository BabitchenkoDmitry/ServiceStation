using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceST.DAL.EF;
using ServiceST.DAL.Entities;
using ServiceST.DAL.Interfaces;
using ServiceST.DAL.Repositories;

namespace ServiceST.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private SSModel db;
        private CarRepository carRepository;
        private ClientRepository clientRepository;
        private OrderRepository orderRepository;
        private OrderStatusRepository orderstatusRepository;


        public EFUnitOfWork(string connectionString)
        {
            db = new SSModel(connectionString);
        }

        public IRepository<Car> Cars
        {
            get
            {
                if (carRepository == null)
                    carRepository = new CarRepository(db);
                return carRepository;
            }
        }

        public IRepository<Client> Clients
        {
            get
            {
                if (clientRepository == null)
                    clientRepository = new ClientRepository(db);
                return clientRepository;
            }
        }

        public IRepository<Order> Orders
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepository(db);
                return orderRepository;
            }
        }

        public IRepository<OrderStatus> OrderStatuses
        {
            get
            {
                if (orderstatusRepository == null)
                    orderstatusRepository = new OrderStatusRepository(db);
                return orderstatusRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
