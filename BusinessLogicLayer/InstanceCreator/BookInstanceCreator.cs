using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.InstanceCreator
{
    public static class BookInstanceCreator
    {
        public static Book BookCreator() => new Book()
        {
            //Name = GetDataHelper.GetNameFromUser(),
            //Author = GetDataHelper.GetAuthorNameFromUser(),
            //CountOfPages = GetDataHelper.GetCountOfBookPages()
        };
    }
}
