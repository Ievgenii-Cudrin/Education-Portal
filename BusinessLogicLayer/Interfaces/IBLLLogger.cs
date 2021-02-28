namespace EducationPortal.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using NLog;

    public interface IBLLLogger
    {
        Logger Logger { get; }
    }
}
