namespace EducationPortal.DAL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using NLog;

    public interface IDalSqlLogger
    {
        Logger Logger { get; }
    }
}
