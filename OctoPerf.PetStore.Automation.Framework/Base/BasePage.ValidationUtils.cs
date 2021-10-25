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

        public string AssertMessage
        {
            get
            {
                return _assertMessage;
            }
            set
            {
                _assertMessage = value;
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
                        AssertMessage = AssertMessage + System.Environment.NewLine + message;
                    }
                    break;
                case ValidateConditions.NotEquals:
                    if (expected.Equals(actual))
                    {
                        AssertList.Add($"Expected: { expected}, Actual: {actual}, Operation: {validateConditions}, {message}");
                        AssertMessage = AssertMessage + System.Environment.NewLine + message;
                    }
                    break;
                case ValidateConditions.Contains:
                    if (!expected.Equals(actual))
                    {
                        AssertList.Add($"Expected: { expected}, Actual: {actual}, Operation: {validateConditions}, {message}");
                        AssertMessage = AssertMessage + System.Environment.NewLine + message;
                    }
                    break;
                case ValidateConditions.NotContains:
                    if (expected.Equals(actual))
                    {
                        AssertList.Add($"Expected: { expected}, Actual: {actual}, Operation: {validateConditions}, {message}");
                        AssertMessage = AssertMessage + System.Environment.NewLine + message;
                    }
                    break;
            }
            LogInfo($"Expected: { expected}, Actual: {actual}, Operation: {validateConditions}, {message}");
        }

        public void Validate()
        {
            AssertList.Count.Should().Be(0, AssertMessage);
        }
    }
}
