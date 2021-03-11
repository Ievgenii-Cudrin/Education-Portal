using EducationPortal.BLL.Interfaces;

namespace EducationPortal.BLL.Results
{
    public class OperationResult : IOperationResult
    {
        public string Message { get; set; }

        public bool IsSucceed { get; set; }
    }
}
