using FluentAssertions;
using OctoPerf.PetStore.Automation.Framework.Base;
using OctoPerf.PetStore.Automation.Framework.Objects;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using static OctoPerf.PetStore.Automation.Framework.Data.Data;

namespace OctoPerf.PetStore.Automation.Framework.Page.Objects
{
    public class CheckoutPage : BasePage
    {
        private By _continue = By.Name("newOrder");
        private By _confirm = By.XPath("//a[text()='Confirm']");
        private By _orderConfirmationMessage = By.CssSelector("#Content .messages li");
        private By _orderNumber = By.CssSelector("#Catalog th[align='center']");

        private readonly string ORDER_DETAIL_FIELD = "//td[text()='{0}:']//following-sibling::td";

        public CheckoutPage(IWebDriver driver) : base(driver) { }

        public void HandleUserLoginAndContinue()
        {
            IsElementDisplayed(_continue).Should().BeTrue("Continue button should be displayed.");
            Click(_continue);
        }

        public void ConfirmOrder(bool validateOrder=true, User user=null)
        {
            IsElementDisplayed(_confirm).Should().BeTrue("Confirm button should be displayed.");
            Click(_confirm);

            IsElementDisplayed(_orderConfirmationMessage, TimeSpan.FromSeconds(Constants.WAIT_MEDIUM))
                .Should().BeTrue("Order Confirmation Message not displayed on page.");
            IsElementDisplayed(_orderNumber).Should().BeTrue("Order Number is not displayed on page.");
            LogInfo("Order placed successfully.");

            if (validateOrder) ValidateOrderDetails(user);
        }

        public string ValidateOrderDetails(User user)
        {
            string orderNumber = GetElement(_orderNumber).Text.Split(' ')[1];

            ValidateResult(user.FirstName, GetElement(By.XPath(string.Format(ORDER_DETAIL_FIELD, "First name"))).Text, 
                        "First Name is not matching");
            ValidateResult(user.LastName, GetElement(By.XPath(string.Format(ORDER_DETAIL_FIELD, "Last name"))).Text,
                        "Last Name is not matching");
            ValidateResult(user.Address1, GetElement(By.XPath(string.Format(ORDER_DETAIL_FIELD, "Address 1"))).Text,
                        "Address 1 is not matching");
            ValidateResult(user.Address2, GetElement(By.XPath(string.Format(ORDER_DETAIL_FIELD, "Address 2"))).Text,
                        "Address 2 is not matching");
            ValidateResult(user.City, GetElement(By.XPath(string.Format(ORDER_DETAIL_FIELD, "City"))).Text,
                        "City is not matching");
            ValidateResult(user.State, GetElement(By.XPath(string.Format(ORDER_DETAIL_FIELD, "State"))).Text,
                        "State is not matching");
            ValidateResult(user.Zip, GetElement(By.XPath(string.Format(ORDER_DETAIL_FIELD, "Zip"))).Text,
                        "Zip Code is not matching");
            ValidateResult(user.Country, GetElement(By.XPath(string.Format(ORDER_DETAIL_FIELD, "Country"))).Text,
                        "Country is not matching");
            Validate();
            LogInfo("Order validated successfully.");

            return orderNumber;
        }
    }
}
