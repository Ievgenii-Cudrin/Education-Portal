﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.PL.Models
{
    public class BookViewModel : MaterialViewModel
    {
        public int CountOfPages { get; set; }

        public string Author { get; set; }

        public override string ToString()
        {
            return $"Type: Book" +
                $"\nName: {this.Name}" +
                $"\nCountOfPages: {this.CountOfPages}" +
                $"\nAuthor: {this.Author}";
        }
    }
}
