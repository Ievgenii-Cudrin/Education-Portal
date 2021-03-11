using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.BLL.Interfaces
{
    public interface IOperationResult
    {
        string Message { get; set; }

        bool IsSucceed { get; set; }
    }
}
