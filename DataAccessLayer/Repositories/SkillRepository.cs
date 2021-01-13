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
        private XmlSerializeContext context;
        public SkillRepository(XmlSerializeContext context)
        {
            this.context = context;
        }
        public void Create(Skill item)
        {
            context.Skills.Add(item);
        }

        public void Delete(int id)
        {
            context.Skills.Delete(id);
        }

        public Skill Get(int id)
        {
            return context.Skills.Get(id);
        }

        public IEnumerable<Skill> GetAll()
        {
            return context.Skills.GetAll();
        }

        public void Update(Skill item)
        {
            context.Skills.UpdateObject(item);
        }
    }
}
