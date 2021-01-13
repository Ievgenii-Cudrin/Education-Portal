using DataAccessLayer.DataContext;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class MaterialRepository : IRepository<Material>
    {
        private XmlSerializeContext context;
        public MaterialRepository(XmlSerializeContext context)
        {
            this.context = context;
        }
        public void Create(Material item)
        {
            context.Materials.Add(item);
        }

        public void Delete(int id)
        {
            context.Materials.Delete(id);
        }

        public Material Get(int id)
        {
            return context.Materials.Get(id);
        }

        public IEnumerable<Material> GetAll()
        {
            return context.Materials.GetAll();
        }

        public void Update(Material item)
        {
            context.Materials.UpdateObject(item);
        }
    }
}
