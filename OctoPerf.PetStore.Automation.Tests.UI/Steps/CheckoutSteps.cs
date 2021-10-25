using OctoPerf.PetStore.Automation.Framework.Objects;
using OctoPerf.PetStore.Automation.Framework.Page.Objects;
using OctoPerf.PetStore.Automation.Framework.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace OctoPerf.PetStore.Automation.Tests.UI.Steps
{
    [Binding]
    public sealed class CheckoutSteps : CommonSteps
    {
        public CheckoutSteps(FeatureContext featureContext, ScenarioContext scenarioContext)
            : base(featureContext, scenarioContext) { }

        [When("I add pet to cart")]
        public void WhenIAddPetToCart(Table dataTable)
        {
            foreach (var row in dataTable.Rows)
            {
                _homePage.SelectPetCategory(row[0]);
                _homePage.SelectPetProduct(row[1]);
                _homePage.AddItemToCart(row[2]);
            }
            _homePage.ClickViewCart(isVerify:true, dataTable:dataTable);
        }

        [Then("I should be able to perform checkout with given cart content")]
        public void ThenIShouldPerformCheckout()
        {
            _homePage.ProceedToCheckout();
            _checkoutPage.HandleUserLoginAndContinue();
            _checkoutPage.ConfirmOrder(user:(User)_scenarioContext["user"]);
        }
    }
}
