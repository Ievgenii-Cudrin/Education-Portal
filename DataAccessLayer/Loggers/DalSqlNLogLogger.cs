namespace EducationPortal.DAL.Loggers
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using EducationPortal.DAL.Interfaces;
    using NLog;

    public class DalSqlNLogLogger : IDalSqlLogger
    {
        public Logger Logger
        {
            get
            {
                return LogManager.GetCurrentClassLogger();
            }
        }
    }
}
