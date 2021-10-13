using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZennoLab.Api.Model
{
    public class DataSetUploadOptions
    {
        public string PathFileFolder { get; set; }
        public int MinImageCount { get; set; }
        public int MaxImageCount { get; set; }
        public int AddedImageCount { get; set; }
        public string AnswerFileName { get; set; }
    }
}
