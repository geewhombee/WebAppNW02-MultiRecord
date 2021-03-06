﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using DBSystem.DAL;
using DBSystem.ENTITIES;

namespace DBSystem.BLL
{
    public class Controller01 //Team
    {
        public List<Entity03> List()
        {
            using (var context = new Context())
            {
                return context.Entity03s.ToList();
            }
        }
        public Entity03 FindByID(int id)
        {
            using (var context = new Context())
            {
                return context.Entity03s.Find(id);
            }
        }
    }
}
