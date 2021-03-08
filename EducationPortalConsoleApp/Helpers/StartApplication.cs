using EducationPortal.PL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.PL.Helpers
{
    public class StartApplication
    {
        private IApplication application;

        public StartApplication(IApplication application)
        {
            this.application = application;
        }

        public async Task Run()
        {
            await this.application.StartApplication();
        }
    }
}
