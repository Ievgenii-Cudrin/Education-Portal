using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.InstanceCreator
{
    public static class BookInstanceCreator
    {
        public static Book CreateBook(string name, string author, int countOfPages)
        {
            Book book = null;

            if (name != null && author != null && countOfPages != 0)
            {
                book = new Book()
                {
                    Name = name,
                    Author = author,
                    CountOfPages = countOfPages
                };
            }

            return book == null ? null : book;
        }
    }
}
