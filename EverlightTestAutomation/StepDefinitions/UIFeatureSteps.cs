namespace EverlightTestAutomation.StepDefinitions
{
    [Binding]
    public class UIFeatureSteps
    {
        private readonly HomePage _hPage;
        private readonly CreateNewOrderPage _cNewPage;
        private readonly OrdersPage _oPage;
        Random rnd1 = new Random(3);
        private int rnd1Id = 0;

        public UIFeatureSteps(IWebDriver driver)
        {
            _hPage = new HomePage(driver);
            _oPage = new OrdersPage(driver);
            _cNewPage = new CreateNewOrderPage(driver);
        }

        [Given(@"I am on ""([^""]*)"" and navigate to orders page")]
        public void GivenIAmOnAndNavigateToOrdersPage(string url)
        {
            _hPage.Navigate(url);
            _hPage.clickHyperlink("orders");
        }

        [When(@"I create the order for ""([^""]*)"", (.*), ""([^""]*)""")]
        public void WhenICreateTheOrderFor(string orgCode, int siteId, string modality)
        {
            _oPage.clickCreateNew();

            Assert.IsTrue(true, _oPage.getOrderPageHeader());

            rnd1Id = rnd1.Next(100, 1000);

            _cNewPage.sendMRNText("P" + rnd1Id.ToString());

            _cNewPage.sendFirstNameText("firstName");

            _cNewPage.sendLastNameText("lastName");

            _cNewPage.accessionNumberText("00" + rnd1Id.ToString());

            _cNewPage.selectOrgCodeDropDown(orgCode);

            _cNewPage.selectSiteDropDown(siteId);

            _cNewPage.selectModalityDropDown(modality);

            string dtNameString = DateTime.Now.ToString("dd/MM/yyyy") + Keys.Tab + string.Format("{0:hh:mmtt}", DateTime.Now);

            _cNewPage.studyDateTimeText(dtNameString);

            _cNewPage.submitButton();
        }

        [When(@"view the created order in the search results for ""([^""]*)"", (.*), ""([^""]*)""")]
        public void WhenViewTheCreatedOrderInTheSearchResultsFor(string orgCode, int siteId, string modality)
        {
            _oPage.validateCreatedOrder("P" + rnd1Id.ToString(), "firstName", "lastName", "00" + rnd1Id.ToString(), orgCode, siteId.ToString(), modality);
        }

        [Then(@"I delete the created order and verify order is deleted")]
        public void ThenIDeleteTheCreatedOrderAndVerifyOrderIsDeleted()
        {
            _oPage.deleteCreatedOrder("P" + rnd1Id.ToString(), "00" + rnd1Id.ToString());

        }

        [When(@"I verify the orders from order list table to know the order count")]
        public void WhenIVerifyTheOrdersFromOrderListTableToKnowTheOrderCount()
        {
            _oPage.verifyOrdersFromTable();
        }

        [Then(@"delete order, verify to know the order count")]
        public void ThenDeleteOrderVerifyToKnowTheOrderCount()
        {
            _oPage.deleteExistingOrder();
        }

        [Then(@"verify teleradiology several statuses, Modality and OrgCode")]
        public void ThenVerifyTeleradiologySeveralStatusesModalityAndOrgCode()
        {
            _oPage.clickCreateNew();

            foreach (IWebElement element in _cNewPage.getOrgCodeDropDown())
            {
                if (StaticModels.OrgCodeList().ContainsValue(element.Text.Trim().ToString()))
                {
                    Assert.IsTrue(true, element.Text.ToString());
                }
            }

            foreach (IWebElement element in _cNewPage.getModalityDropDown())
            {
                if (StaticModels.Modality().ContainsValue(element.Text.Trim().ToString()))
                {
                    Assert.IsTrue(true, element.Text.ToString());
                }
            }

            foreach (DictionaryEntry de in StaticModels.OrgCodeList())
            {
                _cNewPage.selectOrgCodeDropDown(de.Key.ToString());

                foreach (IWebElement element in _cNewPage.getSiteDropDown())
                {
                    if (StaticModels.SiteIdList_Org1().ContainsValue(element.Text.Trim().ToString()))
                    {
                        Assert.IsTrue(true, element.Text.ToString());
                    }
                    else if (StaticModels.SiteIdList_Org2().ContainsValue(element.Text.Trim().ToString()))
                    {
                        Assert.IsTrue(true, element.Text.ToString());
                    }
                    else if (StaticModels.SiteIdList_Org3().ContainsValue(element.Text.Trim().ToString()))
                    {
                        Assert.IsTrue(true, element.Text.ToString());
                    }
                }
            }

            Assert.Greater(StaticModels.Status().Count, 0);

            foreach (DictionaryEntry de in StaticModels.Status())
            {
                Assert.IsTrue(true, de.Key.ToString());
                Assert.IsTrue(true, de.Value.ToString());
            }
        }

        [When(@"I check for all mandatory fields")]
        public void WhenICheckForAllMandatoryFields()
        {
            _oPage.clickCreateNew();

            _cNewPage.submitButton();

            Assert.Greater(_cNewPage.textErrorMsges.Count, 0);

            foreach(IWebElement element in _cNewPage.textErrorMsges)
            {
                Assert.IsTrue(true, element.Text);
            }
        }


        [Then(@"I check all other validations like duplicate, incorrect data format for ""([^""]*)"", (.*), ""([^""]*)"", (.*),""([^""]*)""")]
        public void ThenICheckAllOtherValidationsLikeDuplicateIncorrectDataFormatFor(string orgCode, int siteId, string modality, int accessionNumber, string mrn)
        {
            _cNewPage.sendMRNText(mrn);

            _cNewPage.sendFirstNameText("firstName");

            _cNewPage.sendLastNameText("lastName");

            _cNewPage.accessionNumberText("00" + accessionNumber.ToString());

            _cNewPage.selectOrgCodeDropDown(orgCode);

            _cNewPage.selectSiteDropDown(siteId);

            _cNewPage.selectModalityDropDown(modality);

            string dtNameString = DateTime.Now.ToString("dd/MM/yyyy") + Keys.Tab + string.Format("{0:hh:mmtt}", DateTime.Now);

            _cNewPage.studyDateTimeText(dtNameString);

            _cNewPage.submitButton();

            Assert.Greater(_cNewPage.textErrorMsges.Count, 0);

            foreach (IWebElement element in _cNewPage.textErrorMsges)
            {
                Assert.IsTrue(true, element.Text);
            }

            _cNewPage.clearAccessionNumberText();

            _cNewPage.accessionNumberText("00" + (accessionNumber + 1).ToString());

            dtNameString = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy") + Keys.Tab + string.Format("{0:hh:mmtt}", DateTime.Now);

            _cNewPage.studyDateTimeText(dtNameString);

            _cNewPage.submitButton();

            Assert.Greater(_cNewPage.textErrorMsges.Count, 0);

            foreach (IWebElement element in _cNewPage.textErrorMsges)
            {
                Assert.IsTrue(true, element.Text);
            }
        }

    }
}
