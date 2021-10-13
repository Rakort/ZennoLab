using NUnit.Framework;
using System.Linq;
using ZennoLab.Api.Data;
using ZennoLab.Api.Model;

namespace ZennoLab.Api.Tests
{
    public class UserDataSetValidationTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void NameRequred()
        {
            var dataSet = new DataSet();
            var errors = dataSet.Validate(null);
            Assert.True(errors.Any(e => e.MemberNames.Contains(nameof(DataSet.Name)) && e.ErrorMessage == "Имя должно быть заполнено"));
        }

        [Test]
        public void NameMinLength()
        {
            var dataSet = new DataSet() { Name = "as" };
            var errors = dataSet.Validate(null);
            Assert.True(errors.Any(e => e.MemberNames.Contains(nameof(DataSet.Name)) && e.ErrorMessage == "Длина именидолжна быть от 4 до 8 символов"));
        }

        [Test]
        public void NameMaxLength()
        {
            var dataSet = new DataSet() { Name = "asdfghjkl" };
            var errors = dataSet.Validate(null);
            Assert.True(errors.Any(e => e.MemberNames.Contains(nameof(DataSet.Name)) && e.ErrorMessage == "Длина именидолжна быть от 4 до 8 символов"));
        }

        [Test]
        public void NameIsOnlyLatinCharsCyrillic()
        {
            var dataSet = new DataSet() { Name = "фывфыв" };
            var errors = dataSet.Validate(null);
            Assert.True(errors.Any(e => e.MemberNames.Contains(nameof(DataSet.Name)) && e.ErrorMessage == "Имя должно содержать только латинские буквы"));
        }

        [Test]
        public void NameIsOnlyLatinCharsNumbers()
        {
            var dataSet = new DataSet() { Name = "asd122" };
            var errors = dataSet.Validate(null);
            Assert.True(errors.Any(e => e.MemberNames.Contains(nameof(DataSet.Name)) && e.ErrorMessage == "Имя должно содержать только латинские буквы"));
        }

        [Test]
        public void NameIsNotCaptcha()
        {
            var dataSet = new DataSet() { Name = "captcha" };
            var errors = dataSet.Validate(null);
            Assert.True(errors.Any(e => e.MemberNames.Contains(nameof(DataSet.Name)) && e.ErrorMessage == "Имя не может содержать слово \"captcha\""));
        }

        [Test]
        public void IsLeastOne_IsLatin_IsCyrillic_IsNumbers()
        {
            var dataSet = new DataSet() { Name = "asdf" };
            var errors = dataSet.Validate(null);
            Assert.True(errors.Any(e => e.MemberNames.Contains(nameof(DataSet.IsLatin)) && e.ErrorMessage == "Должно быть выбрано как минимум одно из: \"Содержит кириллицу\", \"Содержит латиницу\", \"Содержит цифры\""));
        }

        [Test]
        public void ArchiveRequred()
        {
            var dataSet = new DataSet() { Name = "asdf" };
            var errors = dataSet.Validate(null);
            Assert.True(errors.Any(e => e.MemberNames.Contains(nameof(DataSet.Archive)) && e.ErrorMessage == "Отсутствует архив с изображениями"));
        }
    }
}