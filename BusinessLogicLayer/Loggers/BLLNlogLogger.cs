namespace EducationPortal.BLL.Loggers
{
    using EducationPortal.BLL.Interfaces;
    using NLog;

    public class BLLNlogLogger : IBLLLogger
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
