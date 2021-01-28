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
        private XmlSerializeContext _context;
        public MaterialRepository(XmlSerializeContext context)
        {
            this._context = context;
        }
        public void Create(Material item)
        {
            _context.Materials.Add(item);
        }

        public void Delete(int id)
        {
            _context.Materials.Delete(id);
        }

        public Material Get(int id)
        {
            return _context.Materials.Get(id);
        }

        public IEnumerable<Material> GetAll()
        {
            return _context.Materials.GetAll();
        }

        public void Update(Material item)
        {
            _context.Materials.UpdateObject(item);
        }
    }
}
