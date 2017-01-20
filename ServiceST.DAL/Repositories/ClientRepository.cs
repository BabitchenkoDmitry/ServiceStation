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
    public class ClientRepository : IRepository<Client>
    {
        private SSModel db;

        public ClientRepository(SSModel context)
        {
            this.db = context;
        }

        public IEnumerable<Client> GetAll()
        {
            return db.Client;
        }

        public Client Get(int id)
        {
            return db.Client.Find(id);
        }

        public void Create(Client client)
        {
            db.Client.Add(client);
        }

        public void Update(Client client)
        {
            db.Entry(client).State = EntityState.Modified;
        }

        public IEnumerable<Client> Find(Func<Client, Boolean> predicate)
        {
            return db.Client.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Client client = db.Client.Find(id);
            if (client != null)
                db.Client.Remove(client);
        }
    }
}
