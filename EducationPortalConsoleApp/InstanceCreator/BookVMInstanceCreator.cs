namespace EducationPortal.PL.InstanceCreator
{
    using EducationPortal.PL.Models;
    using EducationPortalConsoleApp.Helpers;

    public static class BookVMInstanceCreator
    {
        public static BookViewModel CreateBook()
        {
            BookViewModel book = new BookViewModel()
            {
                Name = GetDataHelper.GetNameFromUser(),
                Author = GetDataHelper.GetAuthorNameFromUser(),
                CountOfPages = GetDataHelper.GetCountOfBookPages(),
            };

            return book;
        }
    }
}
