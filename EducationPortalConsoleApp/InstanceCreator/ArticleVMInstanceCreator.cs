using EducationPortal.PL.Models;
using EducationPortalConsoleApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.PL.InstanceCreator
{
    public static class ArticleVMInstanceCreator
    {
        public static ArticleViewModel CreateArticle()
        {
            ArticleViewModel article = new ArticleViewModel()
            {
                Name = GetDataHelper.GetNameFromUser(),
                PublicationDate = GetDataHelper.GetDateTimeFromUser(),
                Site = GetDataHelper.GetSiteAddressFromUser()
            };

            return article;
        }
    }
}
