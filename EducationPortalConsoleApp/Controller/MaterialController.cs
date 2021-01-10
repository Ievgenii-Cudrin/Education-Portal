using EducationPortalConsoleApp.Branch;
using EducationPortalConsoleApp.Helpers;
using EducationPortalConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortalConsoleApp.Controller
{
    public class MaterialController
    {
        MaterialService materialService;
        ProgrammBranch programmBranch;

        public MaterialController()
        {
            materialService = new MaterialService();
            programmBranch = new ProgrammBranch();
        }

        public void CreateNewMaterial()
        {
            MaterialConsoleMessageHelper.ShowTextForChoiceKindOfMaterial();

            string kindOfMaterial = Console.ReadLine().ToLower();

            switch (kindOfMaterial)
            {
                case "video":
                    break;
                case "book":
                    break;
                case "article":
                    break;
                default:
                    CreateNewMaterial();
                    break;

            }


        }
    }
}
