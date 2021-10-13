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

namespace ZennoLab.Api.Model
{
    public class DataSet : IValidatableObject
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsCyrillic { get; set; }
        public bool IsLatin { get; set; }
        public bool IsNumbers { get; set; }
        public bool IsSpecialChar { get; set; }
        public bool CaseSensitivity { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public LocationAnswer LocationAnswer { get; set; }
        public IFormFile Archive { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            var option = validationContext.GetService<DataSetUploadOptions>();

            if (string.IsNullOrEmpty(Name))
            {
                errors.Add(new ValidationResult("Имя должно быть заполнено", new[] { nameof(Name) }));
            }
            else
            {
                if (Name.Length < 4 || Name.Length > 8)
                    errors.Add(new ValidationResult("Длина именидолжна быть от 4 до 8 символов", new[] { nameof(Name) }));
                if (!Regex.IsMatch(Name, "^[a-zA-Z]+$"))
                    errors.Add(new ValidationResult("Имя должно содержать только латинские буквы", new[] { nameof(Name) }));
                if (Name.Equals("captcha"))                
                    errors.Add(new ValidationResult("Имя не может содержать слово \"captcha\"", new[] { nameof(Name) }));
                
            }
            if (!IsLatin && !IsCyrillic && !IsNumbers)
            {
                errors.Add(new ValidationResult("Должно быть выбрано как минимум одно из: \"Содержит кириллицу\", \"Содержит латиницу\", \"Содержит цифры\"", new[] { nameof(IsLatin), nameof(IsCyrillic), nameof(IsNumbers) }));
            }
            if (Archive == null)
                errors.Add(new ValidationResult("Отсутствует архив с изображениями", new[] { nameof(Archive) }));
            else
            {
                using Stream archiveSteam = Archive.OpenReadStream();
                var zip = new ZipArchive(archiveSteam);
                ZipArchiveEntry answerFile = zip.GetEntry(option.AnswerFileName);
                var countImage = zip.Entries.Count() - (answerFile == null ? 0 : 1);

                var addedRange = new[] { IsCyrillic, IsLatin, IsNumbers, IsSpecialChar, CaseSensitivity }.Where(x => x).Count() * option.AddedImageCount;
                if (countImage < option.MinImageCount + addedRange || countImage > option.MaxImageCount + addedRange)
                    errors.Add(new ValidationResult("Количество картинок не попадает в диапазон ограничений", new[] { nameof(Archive) }));

                if (LocationAnswer == LocationAnswer.SeparateFile)
                {
                    if (answerFile == null)
                        errors.Add(new ValidationResult("Отсутствует файл с ответами", new[] { nameof(Archive) }));
                    else
                    {
                        using var steam = new StreamReader(answerFile.Open());
                        var text = steam.ReadToEnd();
                        var lines = text.Split("\n", StringSplitOptions.RemoveEmptyEntries);
                        if (countImage != lines.Length)
                            errors.Add(new ValidationResult("Количество ответов не совпадает с количеством картинок", new[] { nameof(Archive) }));
                    }
                }
            }
            return errors;
        }
    }
}
