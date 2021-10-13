using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZennoLab.Api.Model;

namespace ZennoLab.Api.Data
{
    public class DataSetEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsCyrillic { get; set; }
        public bool IsLatin { get; set; }
        public bool IsNumbers { get; set; }
        public bool IsSpecialChar { get; set; }
        public bool CaseSensitivity { get; set; }
        public string LocationAnswer { get; set; }
        public string ArchivePath { get; set; }        
    }
}
