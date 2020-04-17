using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using DBSystem.DAL;
using DBSystem.ENTITIES;

namespace DBSystem.BLL
{
    public class Controller02 //Player
    {
        public List<Entity02> FindByID(int id)
        {
            using (var context = new Context())
            {
                IEnumerable<Entity02> results =
                    context.Database.SqlQuery<Entity02>("Player_GetByTeam @ID"
                        , new SqlParameter("ID", id));
                return results.ToList();
            }
        }
        public Entity02 FindByPLID(int id)
        {
            using (var context = new Context())
            {
                return context.Entity02s.Find(id);
            }
        }
        public List<Entity02> List()
        {
            using (var context = new Context())
            {
                return context.Entity02s.ToList();
            }
        }
        public int Add(Entity02 item)
        {
            using (var context = new Context())
            {
                context.Entity02s.Add(item);
                context.SaveChanges();
                return item.PlayerID;

            }
        }
        public int Update(Entity02 item)
        {
            using (var context = new Context())
            {
                context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                return context.SaveChanges();
            }
        }
        public int Delete(int productid)
        {
            using (var context = new Context())
            {
                var existing = context.Entity02s.Find(productid);
                if (existing == null)
                {
                    throw new Exception("Player has been removed from database");
                }
                context.Entity02s.Remove(existing);
                return context.SaveChanges();
            }
        }
    }
}
