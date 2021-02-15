namespace EducationPortal.PL.InstanceCreator
{
    using EducationPortal.PL.Models;
    using EducationPortalConsoleApp.Helpers;

    public static class ArticleVMInstanceCreator
    {
        public static ArticleViewModel CreateArticle()
        {
            ArticleViewModel article = new ArticleViewModel()
            {
                Name = GetDataHelper.GetNameFromUser(),
                PublicationDate = GetDataHelper.GetDateTimeFromUser(),
                Site = GetDataHelper.GetSiteAddressFromUser(),
            };

            return article;
        }
    }
}
