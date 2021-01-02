using DataAccessLayer.DataContext;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class SkillRepository : IRepository<Skill>
    {
        private XmlSerializeContext _context;
        public SkillRepository(XmlSerializeContext context)
        {
            this._context = context;
        }
        public void Create(Skill item)
        {
            _context.Skills.Add(item);
        }

        public void Delete(int id)
        {
            _context.Skills.Delete(id);
        }

        public Skill Get(int id)
        {
            return _context.Skills.Get(id);
        }

        public IEnumerable<Skill> GetAll()
        {
            return _context.Skills.GetAll();
        }

        public void Update(Skill item)
        {
            _context.Skills.UpdateObject(item);
        }
    }
}
