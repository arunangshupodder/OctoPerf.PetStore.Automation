using FluentAssertions;
using OctoPerf.PetStore.Automation.Framework.Base;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;
using static OctoPerf.PetStore.Automation.Framework.Data.Data;

namespace OctoPerf.PetStore.Automation.Framework.Page.Objects
{
    public class HomePage : BasePage
    {
        private By _signInLink = By.XPath("//a[text()='Sign In']");
        //private By _viewCartLink = By.XPath("//a[contains(@href, 'viewCart')]");
        private By _viewCartLink = By.CssSelector("a[href*='viewCart']");
        private By _returnToMainMenuLink = By.XPath("//a[text()='Return to Main Menu']");
        private By _subTotal = By.XPath("//input[@name='updateCartQuantities']//parent::td");
        private By _proceedToCheckoutButton = By.CssSelector("a[href*='newOrderForm']");

        private readonly string ITEM = "//a[contains(@href, '{0}={1}')]";
        private readonly string LIST_PRICE = "//input[@name='{0}']//parent::td//following-sibling::td[1]";
        private readonly string TOTAL_COST = "//input[@name='{0}']//parent::td//following-sibling::td[2]";
        private readonly string URL = "/actions/Catalog.action";
        
        public HomePage(IWebDriver driver) : base(driver) { }

        public void VerifyNavigationToHomePage()
        {
            (DoesURLContainPath(URL) && IsElementDisplayed(_signInLink))
                .Should().BeTrue("User should have been navigated to the Home Page.");
            LogInfo("User is successfully navigated to application home page.");
        }

        public void ClickOnSignIn()
        {
            IsElementDisplayed(_signInLink).Should().BeTrue("Sign In link should be displayed.");
            GetElement(_signInLink).Click();
        }

        public void SelectPetCategory(string categoryId)
        {
            By categoryIdLink = By.XPath(string.Format(ITEM, "categoryId", categoryId));
            IsElementDisplayed(categoryIdLink).Should().BeTrue("Pet Category " + categoryId + " is not displayed.");
            Click(categoryIdLink);
        }

        public void SelectPetProduct(string productId)
        {
            By productIdLink = By.XPath(string.Format(ITEM, "productId", productId));
            IsElementDisplayed(productIdLink).Should().BeTrue("Pet Product " + productId + " is not displayed.");
            Click(productIdLink);
        }

        public void AddItemToCart(string itemId, bool proceedToCheckout=false)
        {
            By itemIdLink = By.XPath(string.Format(ITEM, "workingItemId", itemId));
            IsElementDisplayed(itemIdLink).Should().BeTrue("Pet Item " + itemId + " is not displayed.");
            Click(itemIdLink);

            IsElementDisplayed(By.XPath(string.Format(ITEM, "itemId", itemId)))
                .Should().BeTrue("Item " + itemId + " is not added to shopping cart.");
            LogInfo("Item " + itemId + " is added to shopping cart.");

            if (proceedToCheckout) ProceedToCheckout();
            else ClickReturnToMainMenu();
        }

        public void ClickReturnToMainMenu()
        {
            IsElementClickable(_returnToMainMenuLink).Should().BeTrue("Return To Main Menu link is not displayed.");
            Click(_returnToMainMenuLink);
        }

        public void ProceedToCheckout()
        {
            IsElementClickable(_proceedToCheckoutButton).Should().BeTrue("Proceed to Checkout button should be visible.");
            Click(_proceedToCheckoutButton);
        }

        public void ClickViewCart(bool isVerify = false, Table dataTable = null)
        {
            IsElementClickable(_viewCartLink).Should().BeTrue("View Cart link is not displayed.");
            Click(_viewCartLink);

            if (isVerify)
            {
                double subTotal = 0.00;
                foreach(var rowItem in dataTable.Rows)
                {
                    By itemId = By.XPath(string.Format(ITEM, "itemId", rowItem[2]));
                    ValidateResult(true, IsElementDisplayed(itemId), "Item is not displayed in the Shopping Cart.");

                    string itemQty = GetElement(By.Name(rowItem[2])).GetAttribute("value");
                    string listPrice = Regex.Replace(
                        GetElement(By.XPath(string.Format(LIST_PRICE, rowItem[2]))).Text, Constants.CURRENCY_REGEX, Constants.EMPTY);
                    string totalPrice = Regex.Replace(
                        GetElement(By.XPath(string.Format(TOTAL_COST, rowItem[2]))).Text, Constants.CURRENCY_REGEX, Constants.EMPTY);
                    ValidateResult(Convert.ToInt32(itemQty) * Convert.ToDouble(listPrice), Convert.ToDouble(totalPrice), "Item total price does not match.");

                    subTotal = subTotal + Convert.ToDouble(totalPrice);
                }

                ValidateResult(subTotal, Convert.ToDouble(Regex.Replace(GetElement(_subTotal).Text, Constants.CURRENCY_REGEX, Constants.EMPTY)), 
                    "Sub-Total price does not match.");
            }

            Validate();
        }
    }
}
