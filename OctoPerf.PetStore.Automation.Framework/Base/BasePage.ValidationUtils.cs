using FluentAssertions;
using OctoPerf.PetStore.Automation.Framework.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OctoPerf.PetStore.Automation.Framework.Base
{
    public partial class BasePage
    {
        public List<string> AssertList
        {
            get
            {
                if (_assertList == null) _assertList = new List<string>();
                return _assertList;
            }
            set
            {
                _assertList = value;
            }
        }

        public void ValidateResult(object expected, object actual, string message, ValidateConditions validateConditions = ValidateConditions.Equals)
        {
            switch (validateConditions)
            {
                case ValidateConditions.Equals:
                    if (!expected.Equals(actual))
                    {
                        AssertList.Add($"Expected: { expected}, Actual: {actual}, Operation: {validateConditions}, {message}");
                    }
                    break;
                case ValidateConditions.NotEquals:
                    if (expected.Equals(actual))
                    {
                        AssertList.Add($"Expected: { expected}, Actual: {actual}, Operation: {validateConditions}, {message}");
                    }
                    break;
                case ValidateConditions.Contains:
                    if (!expected.Equals(actual))
                    {
                        AssertList.Add($"Expected: { expected}, Actual: {actual}, Operation: {validateConditions}, {message}");
                    }
                    break;
                case ValidateConditions.NotContains:
                    if (expected.Equals(actual))
                    {
                        AssertList.Add($"Expected: { expected}, Actual: {actual}, Operation: {validateConditions}, {message}");
                    }
                    break;
            }
        }

        public void Validate(string successMessage = "")
        {
            AssertList.Count.Should().Be(0, string.Join("\n", AssertList));
            if (!string.IsNullOrEmpty(successMessage)) LogInfo(successMessage);
        }

        public void AssertTrue(bool validationObject, string failureMessage, string successMessage = "")
        {
            validationObject.Should().BeTrue(failureMessage);
            if (!string.IsNullOrEmpty(successMessage)) LogInfo(successMessage);
        }

        public void AssertFalse(bool validationObject, string failureMessage, string successMessage = "")
        {
            validationObject.Should().BeFalse(failureMessage);
            if (!string.IsNullOrEmpty(successMessage)) LogInfo(successMessage);
        }
    }
}
