using DataAccessLayer.Entities;
using EducationPortalConsoleApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.InstanceCreator
{
    public static class BookInstanceCreator
    {
        public static Book BookCreator() => new Book()
        {
            Name = GetDataHelper.GetNameFromUser(),
            Author = "",
            CountOfPages = 0
        };
    }
}
