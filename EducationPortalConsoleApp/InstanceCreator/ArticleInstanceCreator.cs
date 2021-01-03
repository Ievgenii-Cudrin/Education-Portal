using DataAccessLayer.Entities;
using EducationPortalConsoleApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.InstanceCreator
{
    public static class ArticleInstanceCreator
    {
        public static Article ArticleCreator() => new Article()
        {
            Name = GetDataHelper.GetNameFromUser(),
            PublicationDate = GetDataHelper.GetDateTimeFromUser(),
            Site = GetDataHelper.GetSiteAdressFromUser()
        };
    }
}
