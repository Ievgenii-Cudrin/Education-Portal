using System.Threading.Tasks;

namespace EducationPortal.BLL.Interfaces
{
    public interface ILogInService
    {
        Task<bool> LogIn(string email, string password);

        bool LogOut();
    }
}
