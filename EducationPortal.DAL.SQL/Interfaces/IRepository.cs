﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.DAL.SQL.Interfaces
{
    public interface IRepository<T>
        where T : class
    {
        public List<T> GetAll();

        public T Get(int id);

        public void Create(T item);

        public void Update(T item);

        public void Delete(int id);
    }
}
