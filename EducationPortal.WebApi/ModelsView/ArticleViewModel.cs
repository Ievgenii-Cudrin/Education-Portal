using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPartal.WebApi.ModelsView
{
    public class ArticleViewModel : MaterialViewModel
    {
        public DateTime PublicationDate { get; set; }

        public string Site { get; set; }

        public override string ToString()
        {
            return $"Type: Article" +
                $"\nName: {this.Name}" +
                $"\nPublicationDate: {this.PublicationDate}" +
                $"\nSite: {this.Site}";
        }
    }
}
