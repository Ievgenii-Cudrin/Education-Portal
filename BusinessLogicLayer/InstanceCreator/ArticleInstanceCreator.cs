using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.InstanceCreator
{
    public static class ArticleInstanceCreator
    {
        public static Article CreateArticle(string name, string site, DateTime publicationDate)
        {
            Article article = null;

            if (name != null && site != null)
            {
                article = new Article()
                {
                    Name = name,
                    Site = site,
                    PublicationDate = publicationDate
                };
            }

            return article == null ? null : article;
        }
    }
}
