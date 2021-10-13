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
            Assert.True(errors.Any(e => e.MemberNames.Contains(nameof(DataSet.Name)) && e.ErrorMessage == "��� ������ ���� ���������"));
        }

        [Test]
        public void NameMinLength()
        {
            var dataSet = new DataSet() { Name = "as" };
            var errors = dataSet.Validate(null);
            Assert.True(errors.Any(e => e.MemberNames.Contains(nameof(DataSet.Name)) && e.ErrorMessage == "����� ����������� ���� �� 4 �� 8 ��������"));
        }

        [Test]
        public void NameMaxLength()
        {
            var dataSet = new DataSet() { Name = "asdfghjkl" };
            var errors = dataSet.Validate(null);
            Assert.True(errors.Any(e => e.MemberNames.Contains(nameof(DataSet.Name)) && e.ErrorMessage == "����� ����������� ���� �� 4 �� 8 ��������"));
        }

        [Test]
        public void NameIsOnlyLatinCharsCyrillic()
        {
            var dataSet = new DataSet() { Name = "������" };
            var errors = dataSet.Validate(null);
            Assert.True(errors.Any(e => e.MemberNames.Contains(nameof(DataSet.Name)) && e.ErrorMessage == "��� ������ ��������� ������ ��������� �����"));
        }

        [Test]
        public void NameIsOnlyLatinCharsNumbers()
        {
            var dataSet = new DataSet() { Name = "asd122" };
            var errors = dataSet.Validate(null);
            Assert.True(errors.Any(e => e.MemberNames.Contains(nameof(DataSet.Name)) && e.ErrorMessage == "��� ������ ��������� ������ ��������� �����"));
        }

        [Test]
        public void NameIsNotCaptcha()
        {
            var dataSet = new DataSet() { Name = "captcha" };
            var errors = dataSet.Validate(null);
            Assert.True(errors.Any(e => e.MemberNames.Contains(nameof(DataSet.Name)) && e.ErrorMessage == "��� �� ����� ��������� ����� \"captcha\""));
        }

        [Test]
        public void IsLeastOne_IsLatin_IsCyrillic_IsNumbers()
        {
            var dataSet = new DataSet() { Name = "asdf" };
            var errors = dataSet.Validate(null);
            Assert.True(errors.Any(e => e.MemberNames.Contains(nameof(DataSet.IsLatin)) && e.ErrorMessage == "������ ���� ������� ��� ������� ���� ��: \"�������� ���������\", \"�������� ��������\", \"�������� �����\""));
        }

        [Test]
        public void ArchiveRequred()
        {
            var dataSet = new DataSet() { Name = "asdf" };
            var errors = dataSet.Validate(null);
            Assert.True(errors.Any(e => e.MemberNames.Contains(nameof(DataSet.Archive)) && e.ErrorMessage == "����������� ����� � �������������"));
        }
    }
}